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
    class PasswordError : State
    {
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            if (update.CallbackQuery.Data == "ErrorAdmin_State")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Помощь с выбором препарата?", callbackData: "help_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Сформировать заказ", callbackData: "order_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Вы Админ?", callbackData: "admin_state")
                    });

                // меняем интерфейс
                await botClient.SendTextMessageAsync(user.Id,
                    $"Вы снова вернулись к нам {user.UserName} что вам нужно???",
                    ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup);

                user.State.SetState(new MainMenuState());
            }

            else if (update.CallbackQuery.Data == "ResetPassword_State")
            {
                await botClient.SendTextMessageAsync(user.Id, "Ваш пароль?");
                user.State.SetState(new AdminState());
            }
        }
    }
}
