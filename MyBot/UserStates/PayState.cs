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
    class PayState : State
    {
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            if (update.CallbackQuery.Data == null)
                return;
            else if (update.CallbackQuery.Data == "CardPay_state")
            {
                Console.WriteLine(await botClient.SendTextMessageAsync(
                     chatId: user.Id,
                     text: $"К оплате {user.Order.Drug.Cost} денег, картой!"));
            }

            else if (update.CallbackQuery.Data == "MoneyPay_state")
            {
                Console.WriteLine(await botClient.SendTextMessageAsync(
                     chatId: user.Id,
                     text: $"К оплате {user.Order.Drug.Cost} денег, наличными средствами!"));
            }

        }
    }
}
