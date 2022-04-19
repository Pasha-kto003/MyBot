﻿using MyBot.db;
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
    class MainMenuState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        public List<User> Users = new List<User>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "main_state1")
            {
                //Get();
                foreach (Drug drug in Drugs)
                {
                    InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "3", callbackData: "main_state3"),
                        InlineKeyboardButton.WithCallbackData(text: "4", callbackData: "main_state4"),
                    });
                    await botClient.SendTextMessageAsync(user.Id,
                    "Вот наш каталог товаров",
                    ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup);
                }

                user.State.SetState(new NewState());
            }
            else if (update.CallbackQuery.Data == "main_state2")
            {
                await botClient.SendTextMessageAsync(user.Id, "Данная аптека работает на Telegram боте. Если вам нужно узнать список всех препаратов, следует нажать Препараты. Если вам нужны наши контакты нажмите на кнопку Контакты.");
                user.State.SetState(new DefaultState());
            }

            await Task.CompletedTask;
        }

        //public IEnumerable<Drug> Get()
        //{
        //    return dBContext.Drugs.ToList();
        //}
    }
}
