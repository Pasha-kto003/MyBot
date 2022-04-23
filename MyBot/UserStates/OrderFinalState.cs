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
    public class OrderFinalState : State
    {
        private static MyBot_DBContext _dbContext;
        public List<Drug> drugs = new List<Drug>();
        public List<User> users = new List<User>();
        //private List<Order> Orders = new List<Order>();
        private static List<Order> orders = new List<Order>(_dbContext.Orders);



        public override async Task UpdateHandler(User user, ITelegramBotClient botClient, Update update)
        {
            
            var connection = DbInstance.Get();
            drugs = new List<Drug>(connection.Drugs.ToList());
            //Orders = new List<Order>(connection.Orders.ToList());

            if (update.CallbackQuery == null)
                return;

            if (update.CallbackQuery.Data == "YFinal_State")
            {
                Orders.AddOrder(user.Order.Id, user.Order.NumberOfOrder, user.Order.DateOrder, user.Order.Drug.Id);

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                        text: $"Ваш заказ: {user.Order.NumberOfOrder}\n с препаратом{user.Order.Drug.Title},\nцена заказа{user.Order.Drug.Cost} денег"));

                await Task.CompletedTask;
            } 
        }
    }
}
