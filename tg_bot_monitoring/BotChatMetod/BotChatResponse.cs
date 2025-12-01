using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChatBot.Response
{
    public class ChatBotResponse
    {
        public static async Task ResponseKeyBoard(Message msg, UpdateType type, Telegram.Bot.TelegramBotClient? _bot)
        {
            if (_bot != null)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[] // ряд кнопок
                    {
                        new KeyboardButton("Добавить сервер"),
                    },
                    new[] // еще один ряд
                    {
                        new KeyboardButton("Мои серверы")
                    },
                    new[] // еще один ряд
                    {
                        new KeyboardButton("Редактировать серверы")
                    }
                })
                {
                    ResizeKeyboard = true,  // автоматическая подгонка размера клавиатуры
                    OneTimeKeyboard = false  // клавиатура остается до ручного закрытия
                };
                // Отправляем сообщение с клавиатурой
                await _bot.SendMessage(msg.Chat, "Выберите действие", replyMarkup: replyKeyboard);
            }
            else
            {
                Log.Error("Problem connecting to the bot");
            }
        }
    }
}



