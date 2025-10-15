using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Telegram.Bot;
using Configurator.Serilog;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

SerilogConfig.ConfigureLogger();
Log.Information("start bot");

var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // путь к каталогу с appsettings.json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();
string? token = config["TelegramBotToken"];

using var cts = new CancellationTokenSource();

var bot = new TelegramBotClient(token, cancellationToken: cts.Token);

var me = await bot.GetMe();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;	// we only handle Text messages here
    Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
    // let's echo back received text in the chat
    await bot.SendMessage(msg.Chat.Id, $" said: {msg.Text}");
}











 