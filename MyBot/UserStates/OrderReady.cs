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
            }

            else if (update.CallbackQuery.Data == "YStatusSipleOrder_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Обычное лекарство");
                user.DrugId = user.Drug.Id;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());
            }

            else if (update.CallbackQuery.Data == "YStatusStrongOrder_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Крепкое лекарство");
                user.DrugId = user.Drug.Id;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());
            }

            else if (update.CallbackQuery.Data == "YStatusSuperOrder_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Отличное лекарство");
                user.DrugId = user.Drug.Id;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());
            }

            else if (update.CallbackQuery.Data == "NStatusOrder_state")
            {

                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Drug drug in Drugs)
                {
                    Order order = new Order();
                    order.DateOrder = DateTime.Now;
                    user.Order = order;

                    if (drug.Title == "Обычное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "SimpleOrder_state") });
                    }
                    if (drug.Title == "Аспирин")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "AspirinOrder_state") });
                    }
                    if (drug.Title == "Крепкое лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "StrongOrder_state") });
                    }

                    if (drug.Title == "Отличное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "SuperOrder_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                chatId: user.Id,
                text: "Выбирайте лекарство",
                replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new OrderState());
            }

            await Task.CompletedTask;
        }
    }
}
