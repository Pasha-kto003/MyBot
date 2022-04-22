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
    public class NewState : State
    {
        public string TitleProduct { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public List<Drug> Drugs = new List<Drug>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "Aspirin_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;

                InlineKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatus_state")
                   });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new ChangeStatus());
            }

            if (update.CallbackQuery.Data == "SimpleDrug_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Обычное лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;

                InlineKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatus_state")
                   });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new ChangeStatus());
            }

            if (update.CallbackQuery.Data == "StrongDrug_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Крепкое лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;

                InlineKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatus_state")
                   });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new ChangeStatus());
            }

            if (update.CallbackQuery.Data == "SuperDrug_state")
            {
                user.Drug = Drugs.FirstOrDefault(s => s.Title == "Отличное лекарство");
                user.DrugId = user.Drug.Id;
                TitleProduct = user.Drug.Title;
                Cost = user.Drug.Cost;
                Count = user.Drug.Count;
                Description = user.Drug.Description;

                InlineKeyboardMarkup replyKeyboardMarkup = new(
                   new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatus_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatus_state")
                   });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new ChangeStatus());
            }

            await Task.CompletedTask;
        }
    }
}
