﻿using MyBot.db;
using MyBot.UserStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyBot
{
    public class StateMachine
    {
        private readonly User user;
        private readonly Drug drug;
        State state;

        public StateMachine(User user, State defaultState)
        {
            this.user = user;
            SetState(defaultState);
        }

        public void SetState(State newState) => state = newState;

        internal void UpdateHandler(ITelegramBotClient botClient, Update update)
        {// передаем команду текущему стейту. Если он ее не сможет обработать, то не судьба
            state.UpdateHandler(user, botClient, update);
        }
    }
}
