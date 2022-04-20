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
    public class NewState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "main_state3")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Id == user.DrugId);
                await botClient.SendTextMessageAsync(user.DrugId, $"{user.Drug.Title}");
            }
            //else if (update.CallbackQuery.Data == "main_state4")
            //{
            //    await botClient.SendTextMessageAsync(user.Id, "hi");
            //}

            await Task.CompletedTask;
        }

        //public override async Task Update(Drug drug, User user, ITelegramBotClient botClient, Update update)
        //{
        //    if (update.CallbackQuery == null)
        //        return;

        //    if (update.CallbackQuery.Data == "main_state3")
        //    {
        //        await botClient.SendTextMessageAsync(user.Id, $"{drug.Title}");
        //    }
        //    else if (update.CallbackQuery.Data == "main_state4")
        //    {
        //        await botClient.SendTextMessageAsync(user.Id, $"{drug.Title}");
        //    }

        //    await Task.CompletedTask;
        //}
    }
}
