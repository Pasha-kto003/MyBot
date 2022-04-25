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
    class DoctorFin : State
    {
        private List<Doctor> Doctors = new List<Doctor>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();
            Doctors = new List<Doctor>(connection.Doctors.ToList());
            if (update.CallbackQuery.Data == "Doctor1Order_state")
            {
                user.Doctor = Doctors.FirstOrDefault(s => s.FirstName == "Алексей");
                user.Order.DoctorId = user.Doctor.Id;
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                           new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatusOrder_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatusOrder_state")
                           });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный специалист: {user.Doctor.FirstName}",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new OrderDoctorReady());
            }
            
            else if (update.CallbackQuery.Data == "Doctor2Order_state")
            {
                user.Doctor = Doctors.FirstOrDefault(s => s.FirstName == "Даниил");
                user.Order.DoctorId = user.Doctor.Id;

                InlineKeyboardMarkup replyKeyboardMarkup = new(
                           new[]{
                            InlineKeyboardButton.WithCallbackData(text: "Да!", callbackData: "YStatus1Order_state"),
                            InlineKeyboardButton.WithCallbackData(text: "Нет!", callbackData: "NStatus1Order_state")
                           });

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: $"Ваш выбранный специалист: {user.Doctor.FirstName}",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new OrderDoctorReady());
            }
        }
    }
}
