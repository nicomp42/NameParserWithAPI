/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 * my API Key: See nameParser.myAPIKey
 * https://api.parser.name/?api_key=GETYOUROWNAPIKEY&endpoint=parse&name=Bill%20Nicholson
 */

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;

namespace NameParserWithAPI
{
    class APIs
    {
        public static void Main(string[] args)
        {
            String apiKey = APIKey.read();
            ParseName("Bill Nicholson", apiKey);
        }
        public static void ParseName(String name, String apiKey)
        {
            var client = new WebClient();
            // You can also open the URL in a browser to see the JSON data interpreted by the browser. 
            var jsonString = client.DownloadString("https://api.parser.name/?api_key=" + apiKey + "&endpoint=parse&name="+ name);
            JObject json = JObject.Parse(jsonString);      // NewtonSoft !
            Console.WriteLine(json);                        // All the JSON. 

            JToken dataToken = json.GetValue("data");       // There's a thing called "data" in the JSON data

          var myDictionary = deserializeToDictionary(jsonString);
            /*            Console.WriteLine("Dumping the JSON:");
                        foreach (KeyValuePair<string, object> entry in myDictionary)
                        {
                            Console.WriteLine(entry.Value.GetType()); 
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        } */
            Console.WriteLine(dataToken[0]);                // The first element in "data". Everything inside the outer {} delimiters of the "data" item because there's only one item
            Console.WriteLine("First Name = " + dataToken[0]["name"]["firstname"]["name"]);   // There's a thing called "firstname" in the JSON data
            Console.WriteLine("Last Name = " +  dataToken[0]["name"]["lastname"]["name"]);   // There's a thing called "firstname" in the JSON data
        }
        private static Dictionary<string, object> deserializeToDictionary(string jsonString)
        {
            // https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            var values2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> d in values)
            {
                if (d.Value is JObject)
                {
                    values2.Add(d.Key, deserializeToDictionary(d.Value.ToString()));
                }
                else
                {
                    values2.Add(d.Key, d.Value);
                }
            }
            return values2;
        }
    }
}
