﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyBot
{
    public class Handlers
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long userId = -1;
            if (update.Type == UpdateType.Message)
                userId = update.Message.From.Id;
            else if (update.Type == UpdateType.CallbackQuery)
                userId = update.CallbackQuery.From.Id;

            if (Users.HasUser(userId))
                Users.GetUser(userId)
                    .State
                    .UpdateHandler(botClient, update);
            else
                Users.AddUser(userId, update.Message?.From.Username)
                    .State
                    .UpdateHandler(botClient, update);

            if(update.Type == UpdateType.Message)
            {
                if(update.Message.Type == MessageType.Text)
                {
                    var text = update.Message.Text;
                    var id = update.Message.Chat.Id;
                    string userName = update.Message.Chat.Username;
                    Console.WriteLine($"{userName} | {id} | {text}");
                }
            }
        }
    }
}
