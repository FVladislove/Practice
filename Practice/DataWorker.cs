using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Practice
{
    public class DataWorker
    {
        public static void DeleteFilesFromFolder(string folderPath)
        {
            string[] folderFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + folderPath);
            foreach (string file in folderFiles)
            {
                File.Delete(file);
            }
        }
        public static List<string> Deserialise(string filePath)
        {
            filePath = Directory.GetCurrentDirectory() + filePath;

            Dish[] products = JsonConvert.DeserializeObject<Dish[]>(File.ReadAllText(filePath));

            List<string> dishes = new List<string>();

            foreach (Dish product in products)
            {
                dishes.Append(product.ToString());
            }
            return dishes;
        }

        public static List<Dish> DeserialiseToDishes(string filePath)
        {
            filePath = Directory.GetCurrentDirectory() + filePath;

            Dish[] products = JsonConvert.DeserializeObject<Dish[]>(File.ReadAllText(filePath));

            List<Dish> dishes = new List<Dish>();

            foreach (Dish product in products)
            {
                dishes.Append(product);
            }
            return dishes;
        }
    }
}
