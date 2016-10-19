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

        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '#';                                                                                 ///Kommando Präfix (Was ich vor nem Kommando schreiben muss)
                x.AllowMentionPrefix = true;
            });

            var commands = discord.GetService<CommandService>();                                                    ///Kommandos

            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi");
                });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjM4MjE5OTE4OTc2MzUyMjU4.CujDHw.EJlQ172xzhef16ExEMfCXZis_Ok",TokenType.Bot);   ///Bot Log-In Token
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
