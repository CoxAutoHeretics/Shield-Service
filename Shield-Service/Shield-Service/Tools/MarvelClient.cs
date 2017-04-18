using ShieldService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shield_Service.Tools
{
    public static class MarvelClient
    {
        // CONSTANCE ==================================================================================================

        public static readonly string BASE_URI = "https://gateway.marvel.com:443";

        public static readonly string PATH_CHARACTERS = "/v1/public/characters";

        private static readonly string AUTH_TS = DateTime.Now.ToString();
        private static readonly string AUTH_PUBLIC_KEY = "47e0edb6b1636db561b6aa03933fbfde";
        private static readonly string AUTH_PRIVATE_KEY = "2c8088150ab06a5cc12eb11328541620170a3951";

        // STATE ======================================================================================================

        private static readonly HttpClient _client;

        private static readonly UTF8Encoding _utf8Encoder = new UTF8Encoding();
        private static readonly HashAlgorithm _hashAlgorithm = (HashAlgorithm)CryptoConfig.CreateFromName("MD5");

        // CONSTRUCTION ===============================================================================================

        static MarvelClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BASE_URI);
            //_client.MaxResponseContentBufferSize = 524288;    // bytes
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            AppDomain.CurrentDomain.ProcessExit += Destructor;
        }

        static void Destructor(object sender, EventArgs e)
        {
            _client.Dispose();
        }

        // CONTROL ====================================================================================================

        public static List<Character> GetAllCharacters()
        {
            string relativeRequest = PATH_CHARACTERS + "?" + CreateAuthText();

            var response = GetAsync(relativeRequest).Result;

            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response);
            var results = obj.data.results;

            List<Character> characters = new List<Character>();
            foreach (var result in results) {
                Console.WriteLine(result);
            }

            //JObject jo = JObject.Parse(response);
            //jo.

            return null;
        }

        public static string GetAllCharactersJson()
        {
            string relativeRequest = PATH_CHARACTERS + "?" + CreateAuthText();
            var response = GetAsync(relativeRequest).Result;

            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response);
            var results = obj.data.results;

            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            foreach (var result in results)
            {
                
                Console.WriteLine(result);
            }

            //JObject jo = JObject.Parse(response);
            //jo.

            return null;
        }

        public static async Task<string> GetAsync(string request)
        {
            HttpResponseMessage response = await _client.GetAsync(request).ConfigureAwait(continueOnCapturedContext:false);
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return body;
        }

        public static HttpResponseMessage GetSync(string request)
        {
            HttpResponseMessage response = Task.Run(() => _client.GetAsync(request)).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        // UTILITY ====================================================================================================

        private static string CreateHash(string text)
        {
            byte[] utf8 = _utf8Encoder.GetBytes(text);
            byte[] hashBytes = _hashAlgorithm.ComputeHash(utf8);
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hash;
        }

        private static string CreateAuthText()
        {
            string ts = DateTime.Now.ToString();
            string hash = CreateHash(ts + AUTH_PRIVATE_KEY + AUTH_PUBLIC_KEY);
            return $"ts={ts}&apikey={AUTH_PUBLIC_KEY}&hash={hash}";
        }

    }
}