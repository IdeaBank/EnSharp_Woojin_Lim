using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Library.Utility
{
    public class RestfulApiConnector
    {
        private static RestfulApiConnector _instance;

        public static RestfulApiConnector getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestfulApiConnector();
                }

                return _instance;
            }
        }
        
        private RestfulApiConnector()
        {
        }

        public JObject GetResponseAsJObject(string url, string query, params string[] headers)
        {
            JObject jObject = null;
            
            string encodedQuery = System.Web.HttpUtility.UrlEncode(query);
            url = url + encodedQuery;

            Console.WriteLine(url);
            
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            for (int i = 0; i < headers.Length / 2; ++i)
            {
                httpRequest.Headers[headers[i * 2]] = headers[i * 2 + 1];
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();

                jObject = JObject.Parse(result);
            }

            return jObject;
        }
    }
}