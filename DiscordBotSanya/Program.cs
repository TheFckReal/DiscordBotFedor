using System;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;

namespace DiscordBotSanya
{
    class Program
    {
        // flex
        string f = "sfsd";

        DiscordSocketClient client = new DiscordSocketClient();
        static void Main(string[] args)
           => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            DiscordSocketConfig config = new DiscordSocketConfig()
            {
                UseInteractionSnowflakeDate = false,
                GatewayIntents = GatewayIntents.All
                
            };

            client = new DiscordSocketClient(config);
            client.MessageReceived += CommandsHandler;
            client.Log += Log;

            client.ButtonExecuted += Client_ButtonExecuted;
            var guild = client.Guilds;

            client.UserLeft += Client_UserLeft;

            var token = File.ReadAllText("token.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            Console.ReadLine();
        }
        CharacterSettings chatsats = new CharacterSettings();
        private async Task Client_ButtonExecuted(SocketMessageComponent arg)
        {
            EmbedCreateClass embedCreate = new EmbedCreateClass();

            
            chatsats.FillingStats(arg);
            try
            {
                await arg.DeferAsync(true);
            }
            catch (Exception e)
            {
                arg.Channel.SendMessageAsync(e.Message);
            }


        }

        private Task Client_UserLeft(SocketGuild arg1, SocketUser arg2)
        {

            
            arg1.GetChannel(863849668826628116);
            var channel = arg1.GetChannel(863762608930160653) as SocketTextChannel;

            channel.SendMessageAsync("Hui");

            return Task.CompletedTask;

        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }



        Commands com = new Commands();

        private Task CommandsHandler(SocketMessage msg)
        {
            if (!msg.Author.IsBot)
            {
                if (msg.Author.IsBot)
                {
                    return Task.CompletedTask;
                }
                com.FedyaAnswer(msg, client);
                com.diceroll(msg);
                if (msg.Content.StartsWith("!изменить"))
                {
                    com.RemoteStatChange(msg);
                }
                com.Umoritelno(msg);
                com.Stats(msg);
                com.AdminCheckStat(msg);
                com.TotalWriter(msg);
                //com.TestMessage(msg, client);
            }
            return Task.CompletedTask;
        }


    }
}
