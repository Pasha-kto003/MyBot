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
    class DefaultState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public List<Order> Orders = new List<Order>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            Orders = new List<Order>(connection.Orders.ToList());

            if (update.Message == null)
                return;
            if (update.Message.Text == "/start")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Помощь с выбором препарата?", callbackData: "help_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Сформировать заказ", callbackData: "order_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Вы Админ?", callbackData: "admin_state")
                    });

                // меняем интерфейс
                await botClient.SendTextMessageAsync(update.Message.Chat.Id,
                    $"Добро пожаловать в нашу аптеку {update.Message.Chat.Username}: что вам нужно???",
                    ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup);

                user.State.SetState(new MainMenuState()); // тут указываем класс-обработчик новых команд, таких классов может быть дофига

                
            }
        }
    }
}
