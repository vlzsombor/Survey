using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static List<CardModel> CardList = new List<CardModel>() {
            new CardModel(0,"Title1", "text",4),
            new CardModel(1, "hideg finom fozelek", "juniorok megeszik",4),
            new CardModel(2, "kis lepes az embernek", "nagy lepes az emberisegnek",5),
            new CardModel(3,"Title1", "text",4),
        };

    }
}
