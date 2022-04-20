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
            var drugsMiddle = Drugs.Where(s => s.Cost >= 100 && s.Cost < 200);
            var drugsCritical = Drugs.Where(s => s.Cost > 200);

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "goodStatus_state")
            {
                foreach(Drug drug in drugsGood)
                {
                    keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "help_state2") });
                }
                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Если у вас вполне стабильное количество здоровья вам подойдет данный препарат",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new SelectedStatus());
            }

            if(update.CallbackQuery.Data == "middleStatus_state")
            {
                foreach (Drug drug in drugsMiddle)
                {
                    if(drug.Title == "Крепкое лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "help_state3") });
                    }
                    else if(drug.Title == "Обычное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "help_state5") });
                    }
                }
                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Если у вас среднее количество здоровья",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new SelectedStatus());
            }

            if(update.CallbackQuery.Data == "criticalStatus_state")
            {
                foreach(Drug drug in drugsCritical)
                {
                    keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "help_state4") });
                }
                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Данные препараты подходят если у вас критический уровень здоровья",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new SelectedStatus());
            }

        }
    }
}
