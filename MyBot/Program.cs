using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace MyBot
{
    class Program
    {
        //private readonly MyBot_DBContext dBContext;
        public static List<Drug> Drugs = new List<Drug>();
        public static void Main(string[] args)
        {
            var bot = new TelegramBotClient("5384587265:AAEhBiArssgTTHHDrf_iljwBFF7cm-8tJLc");

            var me = bot.GetMeAsync();
            Console.WriteLine($"Hello, World! I am user {me.Id}.");

            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());

            using var cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };

            bot.StartReceiving(Handlers.HandleUpdateAsync, Handlers.HandleErrorAsync, receiverOptions, cts.Token);

            Console.WriteLine($"Start listening.");
            foreach (Drug drug in Drugs)
            {
                Console.WriteLine($"{drug.Title}");
            }
            Console.ReadKey();

            cts.Cancel();
        }
        //public IEnumerable<Drug> Get()
        //{
        //    return dBContext.Drugs.ToList();
        //}
    }
}
