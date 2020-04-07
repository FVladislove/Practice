using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KFCScraper scraper = new KFCScraper();
            scraper.MyScrape("/ScrapersData/KFC/");

            var scraperDataKFC = Directory.GetFiles(Directory.GetCurrentDirectory() + "/ScrapersData/KFC/");
            DishesPool dishesPool = DishesPool.GetInstance();
            foreach(var scrapedFile in scraperDataKFC)
            {
                dishesPool.AddDishes(DataWorker.DeserialiseToDishes(scrapedFile));
            }
            

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
