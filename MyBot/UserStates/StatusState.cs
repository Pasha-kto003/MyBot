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
    public class StatusState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            var keyBoard = new List<List<InlineKeyboardButton>>();

            var drugsGood = Drugs.Where(s=> s.Cost < 100);
            var drugsMiddle = Drugs.Where(s => s.Cost < 200);
            var drugsCritical = Drugs.Where(s => s.Cost <= 300);

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "goodStatus_state")
            {
                foreach(Drug drug in drugsGood)
                {
                    keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "main_state3") });
                }
                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Если у вас вполне стабильное количество здоровья вам подойдет данный препарат",
                    replyMarkup: replyKeyBoardMarkup));
            }

        }
    }
}
