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
    class AdminMenu : State
    {
        private List<Doctor> Doctors = new List<Doctor>();
        private List<Order> Orders = new List<Order>();
        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            var connection = DbInstance.Get();

            Orders = new List<Order>(connection.Orders.ToList());
            Doctors = new List<Doctor>(connection.Doctors.ToList());

            if (update.CallbackQuery.Data == null)
                return;
            else if (update.CallbackQuery.Data == "YAdmin_State")
            {
                var keyBoard = new List<List<InlineKeyboardButton>>();
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Узнать список заказов", callbackData: "OrderList_State"),
                        InlineKeyboardButton.WithCallbackData(text: "Создать новый препарат", callbackData: "AddDrug_state")
                    });

                // меняем интерфейс
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Какие действия хотите совершить?",
                    replyMarkup: replyKeyboardMarkup));
                //user.State.SetState(new StatusState());
            }

            else if (update.CallbackQuery.Data == "OrderList_State")
            {
                foreach (Order order in Orders)
                {
                    user.Order = order;
                    user.Order.Doctor = Doctors.FirstOrDefault(s => s.Id == user.Order.DoctorId);
                    Console.WriteLine(await botClient.SendTextMessageAsync(
                        chatId: user.Id,
                        text: $"Заказ: {user.Order.NumberOfOrder}\n" +
                        $"Препарат: {user.Order.Drug.Title}\n" +
                        $"Врач: {user.Order.Doctor.LastName}\n" +
                        $"Время приема: {user.Order.DateOrder}"));
                }
            }
        }
    }
}
