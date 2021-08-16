using System;
using System.Collections.Generic;
using System.IO;

namespace NameParserWithAPI
{
    class APIKey
    {
        public static String apiKeyFile = "nameParser.myAPIKey";
        public static String read()
        {
            String apiKey = "";
            try
            {
                /// OMG. https://stackoverflow.com/questions/837488/how-can-i-get-the-applications-path-in-a-net-console-application
                /// This works differently for WinForm and Console apps. Yikes.
                /// apiKey = System.IO.File.ReadAllText(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), apiKeyFile));
                apiKey = System.IO.File.ReadAllText("..\\..\\..\\"+ apiKeyFile);
            } catch (Exception ex) {
                Console.WriteLine("APIKey.read() : " + ex.Message);
                //apiKey = "Unable to read API Key from " + apiKeyFile;
            }
            return apiKey;
        }
    }
}
