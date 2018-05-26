using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SimpleHttpServer.Models;

namespace mota_js_server
{
    class MyRoute
    {
        public static HttpResponse route(HttpRequest request)
        {
            // Console.WriteLine(request.Content);
            string[] strings = request.Content.Split('&');
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string s in strings)
            {
                // string[] keyvalue = s.Split('=');
                int index = s.IndexOf("=");
                if (index > 0)
                {
                    dictionary.Add(s.Substring(0, index), s.Substring(index+1));
                }
                // dictionary.Add(keyvalue[0], System.Web.HttpUtility.UrlDecode(keyvalue[1], Encoding.UTF8));
            }

            // Console.WriteLine(request.Path);

            if (request.Path.StartsWith("readFile"))
                return readFileHandler(dictionary);

            if (request.Path.StartsWith("writeFile"))
                return writeFileHandler(dictionary);

            if (request.Path.StartsWith("listFile"))
                return listFileHandler(dictionary);
            
            return new HttpResponse()
            {
                ContentAsUTF8 = "Request Not found.",
                ReasonPhrase = "Not Found",
                StatusCode = "404",
            };
            
        }

        private static HttpResponse readFileHandler(Dictionary<String,String> dictionary)
        {
            
            string type = dictionary["type"];
            if (type == null || !type.Equals("base64")) type = "utf8";
            string filename = dictionary["name"];
            if (filename == null || !File.Exists(filename))
            {
                return new HttpResponse()
                {
                    ContentAsUTF8 = "File Not Exists!",
                    StatusCode = "404",
                    ReasonPhrase = "Not found"
                };
            }
            byte[] bytes = File.ReadAllBytes(filename);
            return new HttpResponse()
            {
                ContentAsUTF8 = type=="base64"?Convert.ToBase64String(bytes):Encoding.UTF8.GetString(bytes),
                StatusCode = "200",
                ReasonPhrase = "OK"
            };
        }

        private static HttpResponse writeFileHandler(Dictionary<String,String> dictionary)
        {
            string type = dictionary["type"];
            if (type == null || !type.Equals("base64")) type = "utf8";
            string filename = dictionary["name"], content = dictionary["value"];
            byte[] bytes;
            if (type == "base64")
                bytes = Convert.FromBase64String(content);
            else
                bytes = Encoding.UTF8.GetBytes(content);
            File.WriteAllBytes(filename, bytes);
            return new HttpResponse()
            {
                ContentAsUTF8 = Convert.ToString(bytes.Length),
                StatusCode = "200",
                ReasonPhrase = "OK"
            };
        }

        private static HttpResponse listFileHandler(Dictionary<string, string> dictionary)
        {
            string name = dictionary["name"];
            if (name == null || !Directory.Exists(name))
            {
                return new HttpResponse()
                {
                    ContentAsUTF8 = "Directory Not Exists!",
                    StatusCode = "404",
                    ReasonPhrase = "Not found"
                };
            }
            string[] filenames = Directory.GetFiles(name);
            for (int i = 0; i < filenames.Length; i++) filenames[i] = "\"" + Path.GetFileName(filenames[i]) + "\"";
            string content = "[" + string.Join(", ", filenames) + "]";
            //Console.WriteLine(content);
            return new HttpResponse()
            {
                ContentAsUTF8 = content,
                StatusCode = "200",
                ReasonPhrase = "OK"
            };
        }
    }
}
