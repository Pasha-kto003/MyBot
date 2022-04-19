using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot
{
    public abstract class State
    {
        public abstract Task UpdateHandler(User user, ITelegramBotClient botClient, Update update);
    }
}
