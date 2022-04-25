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
    class CancelStatus : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            if (update.CallbackQuery.Data == "YCancel_State")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Drug drug in Drugs)
                {
                    Order order = new Order();
                    order.DateOrder = DateTime.Now;
                    user.Order = order;

                    if (drug.Title == "Обычное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title) });
                    }
                    if (drug.Title == "Аспирин")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title) });
                    }
                    if (drug.Title == "Крепкое лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title) });
                    }

                    if (drug.Title == "Отличное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title) });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Выбирайте лекарство",
                    replyMarkup: replyKeyBoardMarkup));
            }

            await Task.CompletedTask;
        }
    }
}
