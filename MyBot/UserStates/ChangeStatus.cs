﻿using MyBot.db;
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
    public class ChangeStatus : State
    {
        Drug drug;
        public List<Drug> Drugs = new List<Drug>();
        public string TitleProduct { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            
            if (update.CallbackQuery == null)
                return;

            if(update.CallbackQuery.Data == "YStatus_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                user.DrugId = user.Drug.Id;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "My_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "No_state")
                  });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Препарат {user.Drug.Title} приобретен на сумму {user.Drug.Cost} денег, хотите выбрать еще один препарат?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new FinalState());
            }

            else if (update.CallbackQuery.Data == "NStatus_state")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Окей, возвращаемся назад!!!");
                user.State.SetState(new MainMenuState());
            }

            else if(update.CallbackQuery.Data == "YStatusMiddle_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Крепкое лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "My_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "No_state")
                  });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Препарат {user.Drug.Title} приобретен на сумму {user.Drug.Cost} денег, хотите выбрать еще один препарат?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new FinalState());
            }

            else if(update.CallbackQuery.Data == "NStatusMiddle_state")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Окей, возвращаемся назад!!!");
                user.State.SetState(new MainMenuState());
            }

            else if (update.CallbackQuery.Data == "YStatusMiddleCopy_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Обычное лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "My_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "No_state")
                  });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Препарат {user.Drug.Title} приобретен на сумму {user.Drug.Cost} денег, хотите выбрать еще один препарат?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new FinalState());
            }

            else if (update.CallbackQuery.Data == "NStatusMiddleCopy_state")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Окей, возвращаемся назад!!!");
                user.State.SetState(new MainMenuState());
            }

            else if (update.CallbackQuery.Data == "YStatusCritical_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Отличное лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                  new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "My_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "No_state")
                  });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Препарат {user.Drug.Title} приобретен на сумму {user.Drug.Cost} денег, хотите выбрать еще один препарат?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new FinalState());
            }

            else if (update.CallbackQuery.Data == "NStatusCritical_state")
            {
                await botClient.SendTextMessageAsync(user.Id, $"Окей, возвращаемся назад!!!");
                user.State.SetState(new MainMenuState());
            }
        }
    }
}
