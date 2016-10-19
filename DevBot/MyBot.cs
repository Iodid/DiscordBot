using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace DevBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random randMemes;
        Random randAnswers;

        string[] allthememes;
        string[] randomAnswers;

        public MyBot()
        {
            randMemes = new Random();
            randAnswers = new Random();    

            allthememes = new string[]
            {
                ///Hier alle memes eintragen
                "pictures/memes/datboi.gif",
                "pictures/memes/dickbutt.png",
                "pictures/memes/pepe_sad.jpg"
            };

            randomAnswers = new string[]  
            {
                ///Hier Random Antworten eintragen
                "Hue",
                "Heh",
                "Ahaha",
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '#';   ///Kommando Präfix (Was ich vor nem Kommando schreiben muss)
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();   ///Hiernach alle Befehle registrieren

            RegisterDatboiCommand();
            RegisterMemeCommand();
            RegisterRandomAnswerCommand();
            RegisterKillCommand();   ///Needs certain rights

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjM4MjE5OTE4OTc2MzUyMjU4.CujDHw.EJlQ172xzhef16ExEMfCXZis_Ok",TokenType.Bot);   ///Bot Log-In Token
            });
        }

        private void RegisterDatboiCommand()   ///Datboi
        {
            commands.CreateCommand("meme datboi")
                .Do(async (e) =>
                {
                    await e.Channel.SendFile("pictures/memes/datboi.gif");
                });
        }

        private void RegisterMemeCommand()   ///Random Meme incoming!
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {
                    int randomMemeIndex = randMemes.Next(allthememes.Length);
                    string memeToPost = allthememes[randomMemeIndex];
                    await e.Channel.SendFile(memeToPost);
                });
        }

        private void RegisterRandomAnswerCommand()   ///Random Answers incoming!
        {
            commands.CreateCommand("Antworte!")
                .Do(async (e) =>
                {
                    int randomAnswerIndex = randAnswers.Next(randomAnswers.Length);
                    string answerToPost = randomAnswers[randomAnswerIndex];
                    await e.Channel.SendMessage(answerToPost);
                });
        }

        private void RegisterKillCommand()   ///Delete all tha messages!
        {
            commands.CreateCommand("kill")
                .Do(async (e) =>
                {
                    Message[] messagesToKill;
                    messagesToKill = await e.Channel.DownloadMessages(100);
                    await e.Channel.DeleteMessages(messagesToKill);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
