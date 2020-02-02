using LichEngine.ContentExtensions.Maps;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.JsonConverter
{
    class Program
    {
        static string keyResult;
        static void Main(string[] args)
        {
            WelcomeText();



            void WelcomeText()
            {
                Console.WriteLine("Compile Maps: M" + " | " + "Compile TileSets : S");
                keyResult = Console.ReadLine();
                if(keyResult == "M" || keyResult == "m")
                {
                    ReadJson();
                    Console.WriteLine("All map files have been coverted to BSON");

                }
                else 
                if(keyResult == "X" || keyResult == "x")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Not a key");
                    WelcomeText();
                }
                Console.ReadLine();
                WelcomeText();
            }

            void ReadJson()
            {

                string[] files = Directory.GetFiles(Utils.SAVE_DATA + @"TiledMaps\Maps\", "*.json");
                foreach (var item in files)
                {
                    string text = File.ReadAllText(item);
                    var map = JsonConvert.DeserializeObject<TiledMap>(text);
                    using (MemoryStream ms = new MemoryStream())
                    using (BsonDataWriter dataWriter = new BsonDataWriter(ms))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(dataWriter, map);
                        var file = Path.GetFileNameWithoutExtension(item);
                        FileStream stream = new FileStream(Utils.SAVE_DATA + @"TiledMaps\Maps\" + file + ".bson", FileMode.Create, FileAccess.Write);
                        var data = ms.ToArray();
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                        Console.WriteLine("Converted " + file + " To BSON");
                    }

                }
            }
        }
    }
}
