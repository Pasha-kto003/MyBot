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
        public List<Worker> Workers = new List<Worker>();
        public List<Order> Orders = new List<Order>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            Workers = new List<Worker>(connection.Workers.ToList());
            Orders = new List<Order>(connection.Orders.ToList());

            Order order = new Order { NumberOfOrder = "123" };

            if (update.CallbackQuery == null)
                return;

            if(update.CallbackQuery.Data == "YStatusOrder_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                user.DrugId = user.Drug.Id;

                order.Drug = user.Drug;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "My_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "No_state")
                  });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш заказ {order.NumberOfOrder} . Препарат {user.Drug.Title} приобретен на сумму {user.Drug.Cost} денег. Вернуться назад?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new FinalState());
            }

        }
    }
}
