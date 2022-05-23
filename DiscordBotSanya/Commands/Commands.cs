using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Webhook;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSanya
{
    class Commands
    {
        JsonSerializer serializer = new JsonSerializer();

        public bool word_check(SocketMessage msg, string[] words)
        {
            bool check = false;
            for (int i = 0; i < words.Length; ++i)
            {
                if (msg.Content.StartsWith(words[i]))
                {
                    check = true;
                    return check;
                }
            }
            return check;
        }

        public Task diceroll(SocketMessage msg)
        {
            EmbedCreateClass embe = new EmbedCreateClass();
            string[] command = msg.Content.Split(' ');

            Random rnd = new Random();
            // команда вызова куба начинается с к20 или d20
            if (command[0] == "d20" || command[0] == "к20" || command[0] == "К20")
            {

                char[] markvalue;
                int answer = rnd.Next(1, 21);
                bool markexist = false;
                if (command.Length == 1)
                {
                    msg.Channel.SendMessageAsync(null, false, embe.EmbedCreate(answer.ToString()));
                    return Task.CompletedTask;
                }
                markvalue = command[1].ToCharArray();
                if (markvalue != null)
                {



                    if (markvalue[0] == '+' || markvalue[0] == '-')
                    {
                        markexist = true;

                    }
                }

                // проверка знака
                if (markexist)

                    switch (markvalue[0])
                    {



                        case '+':
                            var numberplus = command[1].Substring(1);
                            var znakplus = Convert.ToInt32(numberplus);




                            msg.Channel.SendMessageAsync(null, false, embe.EmbedCreate($"{answer} + {znakplus} = {answer + znakplus}"));

                            break;

                        case '-':
                            var numberminus = command[1].Substring(1);
                            var znakminus = Convert.ToInt32(numberminus);



                            msg.Channel.SendMessageAsync(null, false, embe.EmbedCreate($"{answer} - {znakminus} = {answer - znakminus}"));
                            break;

                        default:
                            msg.Channel.SendMessageAsync(null, false, embe.EmbedCreate(answer.ToString()));

                            break;

                    }
                else
                    msg.Channel.SendMessageAsync(null, false, embe.EmbedCreate(answer.ToString()));
            }
            return Task.CompletedTask;
        }


        public Task FedyaAnswer(SocketMessage msg, DiscordSocketClient client)
        {
            string[] call_words = { "Федя", "Федор", "Фёдор", "федя", "фёдор", "федор", "феодосий", "Феодосий", "федь", "Федь", "Фредерик", "фредерик", "Мой господин" };
            if ((msg.Channel.Id == 616732771733209140 || msg.Channel.Id == 885095772675260436 || msg.Channel.Id == 863849668826628116) && !msg.Author.IsBot && word_check(msg, call_words))

            {

                Random rnd = new Random();
                switch (rnd.Next(1, 101))
                {
                    case <= 10:
                        msg.Channel.SendMessageAsync("Хо-хо-хо");
                        break;

                    case <= 40:
                        msg.Channel.SendMessageAsync("Yes");
                        break;

                    case <= 70:
                        msg.Channel.SendMessageAsync("No");
                        break;     

                    case <= 85:
                        msg.Channel.SendMessageAsync("Даже Каллебаев не знает ответа");
                        break;

                    case <= 99:
                        msg.Channel.SendMessageAsync("Вопрос дискуссионный");
                        break;

                    case <= 100:
                        msg.Channel.SendMessageAsync("https://media.discordapp.net/attachments/863849668826628116/956280136557297775/QeXaaqxed8A.png");
                        break;
                }

            }
            
            return Task.CompletedTask;



        }

        public async Task Umoritelno(SocketMessage msg)
        {
            if (msg.Content.Contains("уморительно") || msg.Content.Contains("Уморительно"))
            {
                msg.Channel.SendMessageAsync("https://media.discordapp.net/attachments/616732771733209140/956283798524416111/fyPZ16YBjdU.png");
            }
            return;

        }

         public async Task Stats(SocketMessage msg)
        {
            CharacterSettings statis = new CharacterSettings();
            Commands cum = new Commands();
            string[] words = { "!статы", "!стат", "!ст" };
            EmbedCreateClass emb = new EmbedCreateClass();

            if (word_check(msg, words))
            {


                string path = $@"{(msg.Author.Id).ToString()}.json";
                if (File.Exists(path))
                {

                    var person = statis.Info(path);
                    msg.Channel.SendMessageAsync($"Характеристика вашего персонажа, {msg.Author.Mention}.", false, emb.EmbedCreateStat(person.strength, person.agility, person.constitution, person.intelligence, person.wisdom));
                }
                else
                    await msg.Channel.SendMessageAsync("У вас нет созданного персонажа. Создайте его с помощью команды !создать персонажа");
            }

            if (msg.Content == "!создать персонажа")
            {


                string path = $@"{(msg.Author.Id).ToString()}.json";

            didit:
                if (!File.Exists(path))
                {
                    await statis.CreateFile(path);
                    goto didit;
                }
                else
                {
                    var person = statis.Info(path);


                    await msg.Channel.SendMessageAsync("Если вы хотите изменить характеристики персонажа, то нажмите на нужную кнопку.", false, emb.EmbedCreateStat(person.strength, person.agility, person.constitution, person.intelligence, person.wisdom), null, null, null, emb.Button(msg));





                }

            }
            return;
        }

        public async Task TotalWriter(SocketMessage msg)
        {
            if (msg.Content != "!оставь надежду всяк сюда входящий.")
            {
                return;
            }
            var guild = (msg.Author as SocketGuildUser).Guild;

            var channels = guild.Channels.Where(x => !(x is ICategoryChannel || x is IVoiceChannel));
            var chan = channels.ToArray();
                for (int i = 0; i < chan.Length; ++i)
                {
                    
                    (chan[i] as ITextChannel).SendMessageAsync("**Оставь надежду всяк сюда входящий.**");
                    
                }
            return;
        }


        bool RoleCheck(SocketGuildUser user, ulong roleid)
        {
            var Context = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Id == roleid);

            if (!user.Roles.Contains(Context) || user.Roles.Contains(default))
            {
                return false;
            }
            else return true;


        }
        public Task AdminCheckStat(SocketMessage msg)
        {
            EmbedCreateClass EmbCreate = new EmbedCreateClass();



            if (msg.Content.Contains("!чек"))
            {

                if (!RoleCheck(msg.Author as SocketGuildUser, 863774786500034570))
                {
                    msg.Channel.SendMessageAsync(null, false, EmbCreate.EmbedCreate("Ошибка", "У вас недостаточно прав"));
                    return Task.CompletedTask;
                }

                if (msg.MentionedUsers.Count == 1)
                {
                    var mentioneduser = msg.MentionedUsers.First();
                    var path = $@"{(mentioneduser.Id).ToString()}.json";
                    if (!File.Exists(path))
                    {
                        msg.Channel.SendMessageAsync("У пользователя нет созданного персонажа.");
                        return Task.CompletedTask;
                    }
                    CharacterSettings stats = new CharacterSettings();
                    var person = stats.Info(path);

                    EmbedCreateClass emb = new EmbedCreateClass();
                    msg.Channel.SendMessageAsync($"Характеристика персонажа {mentioneduser.Username} , {msg.Author.Mention}.", false, emb.EmbedCreateStat(person.strength, person.agility, person.constitution, person.intelligence, person.wisdom));
                }
                else
                {
                    msg.Channel.SendMessageAsync("Вы упомянули больше 1 пользователя или ни одного");
                    return Task.CompletedTask;
                }






            }
            return Task.CompletedTask;
        }

        public enum TypeStats
        {
            Strength,
            Agility,
            Constitution,
            Intelligence,
            Wisdom
        }



        public Task RemoteStatChange(SocketMessage msg)
        {
            EmbedCreateClass EmbCreate = new EmbedCreateClass();

            if (RoleCheck(msg.Author as SocketGuildUser, 863762816229048330))
            {
                string[] usercommand = msg.Content.Split(' ');
                if (msg.MentionedUsers.Count != 1)
                {
                    msg.Channel.SendMessageAsync(null, false, EmbCreate.EmbedCreate("Ошибка", "Вы не упомянули ни одного пользователя."));
                    return Task.CompletedTask;
                }
                var path = $@"{(msg.MentionedUsers.First().Id).ToString()}.json";
                if (!File.Exists(path))
                {
                    msg.Channel.SendMessageAsync(null, false, EmbCreate.EmbedCreate("Ошибка", "У пользователя нет персонажа."));
                    return Task.CompletedTask;
                }
                try
                {
                    CharacterSettings ser = new CharacterSettings();
                    int num = Convert.ToInt32(usercommand[2]);

                    switch (usercommand[1])
                    {

                        case "сил":
                            ser.ManualChangeStats(path, TypeStats.Strength, num);
                            msg.Channel.SendMessageAsync("Готово.");
                            break;
                        case "лов":
                            ser.ManualChangeStats(path, TypeStats.Agility, num);
                            msg.Channel.SendMessageAsync("Готово.");
                            break;
                        case "тел":
                            ser.ManualChangeStats(path, TypeStats.Constitution, num);
                            msg.Channel.SendMessageAsync("Готово.");
                            break;
                        case "инт":
                            ser.ManualChangeStats(path, TypeStats.Intelligence, num);
                            msg.Channel.SendMessageAsync("Готово.");
                            break;
                        case "муд":
                            ser.ManualChangeStats(path, TypeStats.Wisdom, num);
                            msg.Channel.SendMessageAsync("Готово.");
                            break;
                        default:
                            throw new ArgumentException("Некорректная запись изменяемой характеристики");
                            break;
                    }
                }
                catch (Exception e)
                {
                    msg.Channel.SendMessageAsync(e.Message);
                }


            }
            else
            {
                msg.Channel.SendMessageAsync(null, false, EmbCreate.EmbedCreate("Ошибка", "У вас недостаточно прав"));
                return Task.CompletedTask;
            }
            return Task.CompletedTask;

        }

    }
}
