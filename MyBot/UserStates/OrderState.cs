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
    class OrderState : State
    {
        public List<Drug> Drugs = new List<Drug>();
        
        public List<Order> Orders = new List<Order>();

        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Drugs = new List<Drug>(connection.Drugs.ToList());
            
            Orders = new List<Order>(connection.Orders.ToList());

                if (update.CallbackQuery == null)
                    return;

                if (update.CallbackQuery.Data == "AspirinOrder_state")
                {
                    //user.Order.Drug = connection.Drugs.ToList()[(int.Parse(update.CallbackQuery.Data)) - 1];
                    user.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                    user.DrugId = user.Drug.Id;
                    user.Order.Drug = Drugs.FirstOrDefault(s => s.Title == "Аспирин");
                    user.Order.DrugId = user.DrugId;
                    InlineKeyboardMarkup replyKeyboardMarkup = new(
                       new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatusOrder_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatusOrder_state")
                       });

                    Console.WriteLine(await botClient.SendTextMessageAsync(
                        chatId: user.Id,
                        text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                        replyMarkup: replyKeyboardMarkup));
                    user.State.SetState(new OrderDoctorState());
                }

                if (update.CallbackQuery.Data == "SimpleOrder_state")
                {
                    //user.Order.Drug = connection.Drugs.ToList()[(int.Parse(update.CallbackQuery.Data)) - 1];
                    user.Drug = Drugs.FirstOrDefault(s => s.Title == "Обычное лекарство");
                    user.DrugId = user.Drug.Id;
                    user.Order.Drug = Drugs.FirstOrDefault(s => s.Title == "Обычное лекарство");
                    user.Order.DrugId = user.DrugId;
                    InlineKeyboardMarkup replyKeyboardMarkup = new(
                       new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatusSipleOrder_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatusOrder_state")
                       });

                    Console.WriteLine(await botClient.SendTextMessageAsync(
                        chatId: user.Id,
                        text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                        replyMarkup: replyKeyboardMarkup));
                    user.State.SetState(new OrderDoctorState());
                }

                if (update.CallbackQuery.Data == "StrongOrder_state")
                {
                    //user.Order.Drug = connection.Drugs.ToList()[(int.Parse(update.CallbackQuery.Data)) - 1];
                    user.Drug = Drugs.FirstOrDefault(s => s.Title == "Крепкое лекарство");
                    user.DrugId = user.Drug.Id;
                    user.Order.Drug = Drugs.FirstOrDefault(s => s.Title == "Крепкое лекарство");
                    user.Order.DrugId = user.DrugId;
                    InlineKeyboardMarkup replyKeyboardMarkup = new(
                       new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatusStrongOrder_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatusOrder_state")
                       });

                    Console.WriteLine(await botClient.SendTextMessageAsync(
                        chatId: user.Id,
                        text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                        replyMarkup: replyKeyboardMarkup));
                    user.State.SetState(new OrderDoctorState());
                }

                if (update.CallbackQuery.Data == "SuperOrder_state")
                {
                    //user.Order.Drug = connection.Drugs.ToList()[(int.Parse(update.CallbackQuery.Data)) - 1];
                    user.Drug = Drugs.FirstOrDefault(s => s.Title == "Отличное лекарство");
                    user.DrugId = user.Drug.Id;
                    user.Order.Drug = Drugs.FirstOrDefault(s => s.Title == "Отличное лекарство");
                    user.Order.DrugId = user.DrugId;
                    InlineKeyboardMarkup replyKeyboardMarkup = new(
                       new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatusSuperOrder_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatusOrder_state")
                       });

                    Console.WriteLine(await botClient.SendTextMessageAsync(
                        chatId: user.Id,
                        text: $"Ваш выбранный препарат:\n{ user.Drug.Title },\nДанный препарат востанавливает {user.Drug.Cost} единиц здоровья,\nВоздействие на организм: {user.Drug.Description} \n Будете приобретать?",
                        replyMarkup: replyKeyboardMarkup));
                    user.State.SetState(new OrderDoctorState());
                }

                

                await Task.CompletedTask;
        }
    }
}
