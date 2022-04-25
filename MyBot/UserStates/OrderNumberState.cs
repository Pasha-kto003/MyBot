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
    class OrderNumberState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public List<Order> Orders = new List<Order>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            Orders = new List<Order>(connection.Orders.ToList());

            user.Order.NumberOfOrder = update.Message.Text;

            InlineKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YFinal_State"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NFinal_state")
                   });

            Console.WriteLine(await botClient.SendTextMessageAsync(
                chatId: user.Id,
                text: $"Подтвердите ваш заказ: Номер заказа: {user.Order.NumberOfOrder}\nПрепарат для покупки {user.Order.Drug.Title}\nДата заказа: {user.Order.DateOrder}\nСтоимость вашего заказа: {user.Order.Drug.Cost}",
                replyMarkup: replyKeyboardMarkup));

            user.State.SetState(new OrderFinalState());
            await Task.CompletedTask;
        }
    }
}
