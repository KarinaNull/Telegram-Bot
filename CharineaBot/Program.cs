#pragma warning disable CS0618

using System;
using System.Configuration;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Awesome
{
    class Program
    {
        static ITelegramBotClient botClient;
        static readonly Random random = new Random();

        static void Main()
        {
            botClient = new TelegramBotClient(ConfigurationManager.AppSettings["token"]);
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat,
                    "Привет, вот что я умею:\r\n" +
                    "/photo - отправить фото\r\n" +
                    "/start - вывести перечень команд\r\n");
            }
            else if (e.Message.Text == "/photo")
            {
                await botClient.SendPhotoAsync(
                    e.Message.Chat,
                    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    caption: "<b>Попугайчик</b>.\r\nSource: <a href=\"https://pixabay.com\">Pixabay</a>", parseMode: ParseMode.Html);
            }
        }
    }
}
