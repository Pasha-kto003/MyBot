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
        //public override Task Update(Drug drug, User user, ITelegramBotClient botClient, Update update)
        //{
        //    throw new NotImplementedException();
        //}

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            if (update.Message == null)
                return;
            if (update.Message.Text == "/start")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Выбрать препарат", callbackData: "main_state1"),
                        InlineKeyboardButton.WithCallbackData(text: "Подсказки", callbackData: "main_state2"),
                        InlineKeyboardButton.WithCallbackData(text: "Контакты автора", callbackData: "contact_state")
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
