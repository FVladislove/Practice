using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IronWebScraper;
using System.IO;
using Newtonsoft.Json;
namespace Practice
{
    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageLink { get; set; }

    }
    class ProductsEntry
    {
        public List<Product> products = new List<Product>();
        public int counter = 0;
    }
    class KFCScraper : WebScraper
    {
        public static Dictionary<string, ProductsEntry> map = new Dictionary<string, ProductsEntry>();
        public override void Init()
        {
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://www.kfc-ukraine.com", Parse);
        }
        public override void Parse(Response response)
        {
            var menuNavBar = response.Css("ul.main-nav li.menu ul.sub-nav")[0];
            foreach (var specialMenu in menuNavBar.ChildNodes)
            {
                if (specialMenu.InnerHtml != null)
                {
                    string specialMenuName = specialMenu.TextContentClean;

                    map.Add(specialMenuName, new ProductsEntry());

                    string specialMenuLink;
                    specialMenuLink = specialMenu.ChildNodes[1].Attributes["href"];

                    this.Request(specialMenuLink, ParseSpecialMenu, new MetaData() { { "specialMenuName", specialMenuName } });
                }
            }
        }
        public void ParseSpecialMenu(Response response)
        {
            var productsUL = response.Css("ul.products-detail-list")[0];

            string specialMenuName = response.MetaData.Get<string>("specialMenuName");
            string filePath = Directory.GetCurrentDirectory()
                        + "/ScrapersData/KFC/" + specialMenuName
                        + ".json";

            foreach (var productLI in productsUL.ChildNodes)
            {
                if (productLI.InnerHtml != null)
                {
                    
                    map[specialMenuName].counter++;

                    string productPageLink = productLI.ChildNodes[1].Attributes["href"];

                    this.Request(productPageLink, ParseProduct, new MetaData() {
                        { "specialMenuName", specialMenuName },
                        { "filePath", filePath }
                    });
                }
            }

        }

        public void ParseProduct(Response response)
        {
            

            Product product = new Product();

            var productInfoElement = response.Css("div.product-info-wrp")[0];

            product.ImageLink = response.Css("div.product-photo-wrp")[0].ChildNodes[1].Attributes["src"];
            product.Name = productInfoElement.ChildNodes[1].InnerTextClean;
            product.Description = productInfoElement.ChildNodes[3].InnerTextClean;
            product.Price = float.Parse(productInfoElement.ChildNodes[7].ChildNodes[0].TextContentClean);

            string productStr = JsonConvert.SerializeObject(product);
            
            //Scrape(productStr, response.MetaData.Get<string>("filePath"));
            if (--map[response.MetaData.Get<string>("specialMenuName")].counter == 0)
            {
                Scrape(map[response.MetaData.Get<string>("specialMenuName")].products, response.MetaData.Get<string>("filePath"));
            }
            else {
                map[response.MetaData.Get<string>("specialMenuName")].products.Add(product);
            }
        }
    }
    public class DataWorker
    {
        public static void Deserialise(string fileName)
        {
            fileName = Directory.GetCurrentDirectory() + fileName;
            Product[] products = JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(fileName));
            foreach(Product product in products)
            {
                Console.WriteLine(product);
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            /*DeleteFilesFromFolder("/ScrapersData/KFC/");
            var scraper = new KFCScraper();
            var scraperTask = scraper.StartAsync();

            
            while (true) {
                if (scraperTask.IsCompleted)
                    break;
            }*/
            DataWorker.Deserialise("/ScrapersData/KFC/Cалати.json");
            CreateHostBuilder(args).Build().Run();
        }

        static void DeleteFilesFromFolder(string folderPath)
        {
            string[] folderFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + folderPath);
            foreach (string file in folderFiles)
            {
                File.Delete(file);
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
