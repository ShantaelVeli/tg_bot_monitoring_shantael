using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using DataBase.Context;
using ChatBot.Response;
using DataBase.DataAccess;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChatBot
{

    public class ChatBotServices
    {
        private readonly Telegram.Bot.TelegramBotClient? _bot;
        private readonly DataAccess dataAccess;
        public ChatBotServices(Telegram.Bot.TelegramBotClient? bot, ApplicationContext context)
        {
            _bot = bot;
            dataAccess = new DataAccess(context);
        }


        public void ChatBotMetods()
        {

            if (_bot != null)
            {
                _bot.OnError += OnError;
                _bot.OnMessage += OnMessage;
                _bot.OnUpdate += OnUpdate;
            }
        }




        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Log.Error(exception.Message);// just dump the exception to the console
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {

            if (_bot != null && msg.ReplyToMessage == null)
            {
                switch (msg.Text)
                {
                    case "/start":
                        Log.Information("Start chat chatid =" + msg.Chat.Id + " username=@" + msg.Chat.Username);
                        await _bot.SendMessage(msg.Chat, "Здравствуйте, данный бот позволит вам мониторить ваши серверы");
                        await ChatBotResponse.ResponseKeyBoard(msg, type, _bot);
                        await dataAccess.PushDataChat(msg.Chat.Id);
                        break;
                    case "Добавить сервер":
                        await _bot.SendMessage(msg.Chat.Id, "Введите ключ (ответом на это сообщение):", replyMarkup: new ForceReplyMarkup { Selective = true });
                        break;
                    case "/keybord":
                        await ChatBotResponse.ResponseKeyBoard(msg, type, _bot);
                        break;
                    default:
                        await _bot.SendMessage(msg.Chat, "такой команды нет");
                        break;
                }
            }

            if (_bot != null && msg.ReplyToMessage != null)
            {
                if (msg.ReplyToMessage.Text == "Введите ключ (ответом на это сообщение):")
                {
                    await dataAccess.PushDataSSh(msg);
                }
            }



        }

        async Task OnUpdate(Update update)
        {
            if ((update is { CallbackQuery: { } query }) && _bot != null)
            {
                var chatId = query.Message!.Chat.Id;
                await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendLocation(chatId, latitude: 33.747252f, longitude: -112.633853f);
            }
        }
    }
}



