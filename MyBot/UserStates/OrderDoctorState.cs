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
    class OrderDoctorState : State
    {
        private List<Doctor> Doctors = new List<Doctor>();
        private List<Drug> Drugs = new List<Drug>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Doctors = new List<Doctor>(connection.Doctors.ToList());
            Drugs = new List<Drug>(connection.Drugs.ToList());
            if (update.CallbackQuery.Data == "YStatusOrder_state")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Doctor doctor in Doctors)
                {
                    user.Order.Doctor = doctor;
                    user.Order.DoctorId = doctor.Id;
                    if (doctor.FirstName == "Алексей")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor1Order_state") });
                    }
                    if (doctor.FirstName == "Даниил")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor2Order_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Выбирайте врача",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new DoctorFin());
            }

            if (update.CallbackQuery.Data == "YStatusSipleOrder_state")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Doctor doctor in Doctors)
                {
                    user.Order.Doctor = doctor;
                    user.Order.DoctorId = doctor.Id;
                    if (doctor.FirstName == "Алексей")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor1Order_state") });
                    }
                    if (doctor.FirstName == "Даниил")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor2Order_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Выбирайте врача",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new DoctorFin());
            }

            if (update.CallbackQuery.Data == "YStatusStrongOrder_state")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Doctor doctor in Doctors)
                {
                    user.Order.Doctor = doctor;
                    user.Order.DoctorId = doctor.Id;
                    if (doctor.FirstName == "Алексей")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor1Order_state") });
                    }
                    if (doctor.FirstName == "Даниил")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor2Order_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Выбирайте врача",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new DoctorFin());
            }

            if (update.CallbackQuery.Data == "YStatusSuperOrder_state")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Doctor doctor in Doctors)
                {
                    user.Order.Doctor = doctor;
                    user.Order.DoctorId = doctor.Id;
                    if (doctor.FirstName == "Алексей")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor1Order_state") });
                    }
                    if (doctor.FirstName == "Даниил")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(doctor.FirstName, callbackData: "Doctor2Order_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Выбирайте врача",
                    replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new DoctorFin());
            }

            else if (update.CallbackQuery.Data == "NStatusOrder_state")
            {

                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Drug drug in Drugs)
                {
                    Order order = new Order();
                    order.DateOrder = DateTime.Now;
                    user.Order = order;

                    if (drug.Title == "Обычное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "SimpleOrder_state") });
                    }
                    if (drug.Title == "Аспирин")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "AspirinOrder_state") });
                    }
                    if (drug.Title == "Крепкое лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "StrongOrder_state") });
                    }

                    if (drug.Title == "Отличное лекарство")
                    {
                        keyBoard.Add(new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(drug.Title, callbackData: "SuperOrder_state") });
                    }
                }

                var replyKeyBoardMarkup = new InlineKeyboardMarkup(keyBoard);
                Console.WriteLine(await botClient.SendTextMessageAsync(
                chatId: user.Id,
                text: "Выбирайте лекарство",
                replyMarkup: replyKeyBoardMarkup));
                user.State.SetState(new OrderState());
            }

            await Task.CompletedTask;
        }
    }
}
