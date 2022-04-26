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
    public class OrderFinalState : State
    {
        private static readonly MyBot_DBContext _dbContext;
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
                Orders.AddOrder(user.Order.Id, user.Order.NumberOfOrder, user.Order.DateOrder, user.Order.Drug.Id, user.Order.Doctor.Id);

                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                        text: $"Ваш заказ был обработан, Спасибо за покупку.\nВаш заказ: Номер заказа: { user.Order.NumberOfOrder}\nПрепарат для покупки { user.Order.Drug.Title}\nСпециалист: {user.Order.Doctor.FirstName}\nДата заказа: { user.Order.DateOrder}\nСтоимость вашего заказа: { user.Order.Drug.Cost}"));

                await Task.CompletedTask;

                var keyBoard = new List<List<InlineKeyboardButton>>();
                InlineKeyboardMarkup replyKeyboardMarkup = new(
                    new[]{
                        InlineKeyboardButton.WithCallbackData(text: "Картой", callbackData: "CardPay_state"),
                        InlineKeyboardButton.WithCallbackData(text: "Наличными средствами", callbackData: "MoneyPay_state"),
                        
                    }); 

                // меняем интерфейс
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Какой способ оплаты",
                    replyMarkup: replyKeyboardMarkup));
                user.State.SetState(new PayState());
            }
            else if (update.CallbackQuery.Data == "NFinal_state")
            {
                Console.WriteLine(await botClient.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Тогда вы можете выбрать другой препарат"));
                await Task.CompletedTask;


                var keyBoard = new List<List<InlineKeyboardButton>>();

                foreach (Drug drug in drugs)
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
                await Task.CompletedTask;
            }
        }
    }
}
