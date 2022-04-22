using MyBot.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot.UserStates
{
    class FinalState : State
    {
        public List<Drug> Drugs = new List<Drug>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());

            if (update.CallbackQuery == null)
                return;

            else if (update.CallbackQuery.Data == "My_state")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Окей!!!");
                user.State.SetState(new MainMenuState());
            }

            else if (update.CallbackQuery.Data == "No_State")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Хорошо!!!");
                user.State.SetState(new ChangeStatus());
            }
        }
    }
}
