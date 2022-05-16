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
    class PayState : State
    {
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            if (update.CallbackQuery.Data == null)
                return;
            else if (update.CallbackQuery.Data == "CardPay_state")
            {
                //Console.WriteLine(await botClient.SendTextMessageAsync(
                //     chatId: user.Id,
                //     text: $"К оплате {user.Order.Drug.Cost} денег, картой!"));
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да", callbackData: "back_state"),
                    });

                // меняем интерфейс
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"К оплате {user.Order.Drug.Cost} денег, картой! Вернуться назад?",
                    replyMarkup: replyKeyboardMarkup));
            }

            else if (update.CallbackQuery.Data == "MoneyPay_state")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да", callbackData: "back_state"),
                    });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                     chatId: user.Id,
                     text: $"К оплате {user.Order.Drug.Cost} денег, наличными средствами!",
                     replyMarkup: replyKeyboardMarkup));
            }

            else if (update.CallbackQuery.Data == "back_state")
            {
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Помощь с выбором препарата?", callbackData: "help_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Сформировать заказ", callbackData: "order_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Вы Админ?", callbackData: "admin_state")
                    });

                // меняем интерфейс
                await botClient.SendTextMessageAsync(user.Id,
                    $"Добро пожаловать в нашу аптеку {user.UserName}: что вам нужно???",
                    ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup);

                user.State.SetState(new MainMenuState());
            }
        }
    }
}
