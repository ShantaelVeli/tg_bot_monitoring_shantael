using Microsoft.Extensions.Configuration;
using Serilog;
using Telegram.Bot;
using Configurator.Serilog;
using Telegram.Bot.Types;
using DataBase.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChatBot;
using System.IO;
using Cryptography;

SerilogConfig.ConfigureLogger();

if (!File.Exists("./aes/aes-key.txt"))
{


    Log.Information("start bot");

    var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // путь к каталогу с appsettings.json
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

    var servCollect = new ServiceCollection();

    servCollect.AddDbContext<ApplicationContext>(opt =>
    {
        opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

    var ServProv = servCollect.BuildServiceProvider();

    var context = ServProv.GetRequiredService<ApplicationContext>();

    string? token = configuration["TelegramBotToken"];

    using var cts = new CancellationTokenSource();

    Telegram.Bot.TelegramBotClient? bot = new TelegramBotClient(token, cancellationToken: cts.Token);
    var me = await bot.GetMe();

    var commands = new[]
    {
    new BotCommand { Command = "start", Description = "Начать работу" },
    new BotCommand { Command = "keybord", Description = "Отоброзить клавиатуру" },
    new BotCommand { Command = "settings", Description = "Настройки" }
    };

    // Установите команды как меню
    // await bot.SendRequest(new SetMyCommandsRequest(commands));
    await bot.SetMyCommands(commands);


    var ChatBot = new ChatBotServices(bot, context);
    ChatBot.ChatBotMetods();

    Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
    Console.ReadLine();
    cts.Cancel(); // stop the bot
}
else
{
    Log.Error("Encryption key file not found. Please run create-aes-key.sh and restart the application");
    Aes.Encrypt("sajdkasj");
}