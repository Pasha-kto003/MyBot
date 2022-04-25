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
    class OrderDoctorReady : State
    {
        private List<Doctor> Doctors = new List<Doctor>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Doctors = new List<Doctor>(connection.Doctors.ToList());

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "YStatusOrder_state")
            {
                user.Order.Doctor = Doctors.FirstOrDefault(s => s.Id == 1);
                user.Order.DoctorId = user.DoctorId;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());
            }

            else if (update.CallbackQuery.Data == "YStatus1Order_state")
            {
                user.Order.Doctor = Doctors.FirstOrDefault(s => s.Id == 2);
                user.Order.DoctorId = user.DoctorId;

                await botClient.SendTextMessageAsync(user.Id, "Введите номер заказа");
                user.State.SetState(new OrderNumberState());
            }

                else if (update.CallbackQuery.Data == "NStatusOrder_state")
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
                        text: "Выбирайте врача снова",
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
                        text: "Выбирайте врача снова",
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
                        text: "Выбирайте врача снова",
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
                        text: "Выбирайте врача снова",
                        replyMarkup: replyKeyBoardMarkup));
                    user.State.SetState(new DoctorFin());
                }
            
        }
    }
}
