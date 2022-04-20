using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot.UserStates
{
    public class SelectedStatus : State
    {
        Drug drug;
        public List<Drug> Drugs = new List<Drug>();
        public string TitleProduct { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
            TitleProduct = user.Drug.Title;
            Cost = user.Drug.Cost;
            Count = user.Drug.Count;
            Description = user.Drug.Description;

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "help_state2")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Продукт {user.Drug.Title}");
            }
        }
    }
}
