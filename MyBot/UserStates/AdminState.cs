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

            if(update.Message.Text == "admin123")
            {
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

            else if (update.Message.Text != "admin123")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "ErrorAdmin_State"),
                        InlineKeyboardButton.WithCallbackData(text: "Повтор", callbackData: "ResetPassword_State")
                  });
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Пароль неверный, вернуться в главное меню?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new PasswordError());
            }

        }
    }
}
