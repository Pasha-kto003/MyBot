using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot.UserStates
{
    class MainMenuState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public List<User> Users = new List<User>();
        public List<Order> Orders = new List<Order>();
        //public override Task Update(Drug drug, User user, ITelegramBotClient botClient, Update update)
        //{
        //    throw new NotImplementedException();
        //}

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            Orders = new List<Order>(connection.Orders.ToList());

            if (update.CallbackQuery == null)
                return;

            else if(update.CallbackQuery.Data == "help_state")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Стабильный", callbackData: "goodStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Средний", callbackData: "middleStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Малый запас здоровья", callbackData: "criticalStatus_state"),     
                    });

                // меняем интерфейс
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Какой у вас запас здоровья",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new StatusState());
            }

            if (update.CallbackQuery.Data == "order_state")
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

            else if (update.CallbackQuery.Data == "admin_state")
            {
                await botClient.SendTextMessageAsync(user.Id, "Ваш пароль?");
                user.State.SetState(new AdminState());
            }

            await Task.CompletedTask;
        }
    }
}
