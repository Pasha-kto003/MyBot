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
        }
    }
}
