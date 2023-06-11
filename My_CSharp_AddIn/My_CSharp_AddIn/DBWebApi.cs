using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace My_CSharp_AddIn
{
    class DBWebApi
    {
        public static async Task<List<string>> GetConnect(string Login, string Password)
        {
            List<string> res = new List<string>(2);
            Login = Login + "\"";
            Password =  Password + "\"";
            var entre = "{\n  \"name\": \"" + Login + ",\n  \"password\": \""+ Password+"\n}";

            string responseText;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:8080/auth/login"))
                    {
                        request.Headers.TryAddWithoutValidation("accept", "application/json");

                        request.Content = new StringContent(entre);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                        var response = await httpClient.SendAsync(request);

                        responseText = await response.Content.ReadAsStringAsync();
                        res.Add(response.StatusCode.ToString());

                        Console.WriteLine(response.StatusCode);
                    }
                }

                res.Add(responseText);

                return res;
            }
            catch (Exception)
            {
                res.Add("Not con");
                res.Add("");

                return res;
            }
 
        }
        public static async Task<List<string>> PostFile(string userkey, string filepath)
        {
            List<string> res = new List<string>();

            string responseText;
            string name = Path.GetFileName(filepath);
            string pattern = "[^a-zA-Z]";
            string result = Regex.Replace(name, pattern, "");
            string fileNameZip = result.Substring(0, result.Length - 3) + ".zip";

            string description = "result";

            try
            {
                using (var httpClient2 = new HttpClient())
                {
                    using (var request1 = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:8080/container/" + name + "/" + description))
                    {
                        request1.Headers.TryAddWithoutValidation("accept", "*/*");
                        request1.Headers.TryAddWithoutValidation("Authorization", "Bearer " + userkey);

                        var multipartContent = new MultipartFormDataContent();
                        var file1 = new ByteArrayContent(File.ReadAllBytes(filepath));
                        file1.Headers.Add("Content-Type", "application/x-zip-compressed");
                        multipartContent.Add(file1, "container", Path.GetFileName(fileNameZip));
                        request1.Content = multipartContent;

                        var response1 = await httpClient2.SendAsync(request1);

                        res.Add(response1.StatusCode.ToString());
                    }
                }

                return res;
            }
            catch (Exception)
            {

                res.Add("Not con");

                return res;
            }
 
        }
    }
}
