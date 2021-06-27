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
                "/start - вывести перечень команд\r\n" +
                "/sticker - отправить стикер\r\n" +
                "/nativePolls - Пройти опрос");
            }
            else if (e.Message.Text == "/photo")
            {
                await botClient.SendPhotoAsync(
                e.Message.Chat,
                photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                caption: "<b>Попугайчик</b>.\r\nSource: <a href=\"https://pixabay.com\">Pixabay</a>", parseMode: ParseMode.Html);
            }
            else if (e.Message.Text == "/sticker")
            {
                var stickerIds = new[]
                {
                    "CAACAgIAAxkBAAEBc71gyxEQ1Eu7_JhQ4N8bEUysMg5YEgACjwADAfXbLp0fgFonCqORHwQ",
                    "CAACAgIAAxkBAAEBdIhgy6wbEwvOIiZ5vnKQR2qjP0seVQACRQAD3U7yFTgZqaF0Gf8pHwQ",
                    "CAACAgIAAxkBAAEBdItgy6wocLnnchUI95J1sIjbvjyVmgACHwADobYRCL2v7UTV4wIUHwQ",
                    "CAACAgIAAxkBAAEBdI5gy6w5H2s6rSOv8fDwsqfLQrb1FgACQAAD3U7yFRIdRgRNaU3xHwQ",
                    "CAACAgIAAxkBAAEBdJFgy6xepOwG64TUnpiJW4UjY-EndwAC3AEAAlrjihdqHsd1V_QIvh8E"
                };

                await botClient.SendStickerAsync(
                e.Message.Chat,
                sticker: stickerIds[random.Next(stickerIds.Length)]);
            }
            else if (e.Message.Text == "/nativePolls")
            {
                await botClient.SendPollAsync(
                e.Message.Chat,
                question: "Вы любите Костеньку?",
                options: new[]
                {
                    "Очень!",
                    "Конечно!!"
                });
            }
        }
    }
}
