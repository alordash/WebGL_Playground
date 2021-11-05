using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebGL_Playground_Site {
    public static class Misc {
        public static async Task<String> GetFileContents(string sourcePath, HttpClient http) {
            string source;
            try {
                source = await http.GetStringAsync(sourcePath);
                return source;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}