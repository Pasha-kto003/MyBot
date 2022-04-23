using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.UserStates
{
    class OrderReady : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public List<Order> Orders = new List<Order>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            Orders = new List<Order>(connection.Orders.ToList());

            

            if (update.CallbackQuery == null)
                return;

            if(update.CallbackQuery.Data == "YStatusOrder_state")
            {

                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                user.DrugId = user.Drug.Id;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());


                await Task.CompletedTask;
            }

        }
    }
}
