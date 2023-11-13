using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TextEditor
{
    public class Figure
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("VVEDITE PUTb FAILA:");
            string filePath = Console.ReadLine(); // выбор файла по его пути

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File ne naiden");
                return;
            }

            // загрузка информации из файла
            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.F1)
                {
                    // сохранение файла
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".txt")
                    {
                        File.WriteAllLines(filePath, lines);
                    }
                    else if (fileExtension == ".json")
                    {
                        // преобразование модели в JSON и сохранение
                        string jsonData = JsonConvert.SerializeObject(lines);
                        File.WriteAllText(filePath, jsonData);
                    }
                    else if (fileExtension == ".xml")
                    {
                        // преобразование модели в XML и сохранение
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(string[]));
                        using (TextWriter writer = new StreamWriter(filePath))
                        {
                            xmlSerializer.Serialize(writer, lines);
                        }
                    }

                    Console.WriteLine("File sohranen");
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}