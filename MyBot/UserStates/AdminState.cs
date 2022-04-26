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
    class AdminState : State
    {
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            user.Password = update.Message.Text;

            InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YAdmin_State"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NFinal_state")
                  });

            Console.WriteLine(await botClient.SendTextMessageAsync(
                chatId: user.Id,
                text: $"Подтвердите ваш пароль {user.UserName}, пароль: {user.Password}",
                replyMarkup: replyKeyboardMarkup));
            user.State.SetState(new AdminMenu());
        }
    }
}
