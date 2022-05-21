using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Newtonsoft.Json;
using System.IO;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Webhook;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSanya
{
    class CharacterSettings
    {
        public class Person
        {

            public int points;
            public int strength;
            public int agility;
            public int constitution;
            public int intelligence;
            public int wisdom;

        }

        public Embed EmbedCreateStat(Person person)
        {
            var embed = new EmbedBuilder
            {
                Color = Color.Blue,
                Footer = null,
                Description = null,
                Title = null,


            };


            embed.AddField("Сила", $"{person.strength}");
            embed.AddField("Ловкость", $"{person.agility}");
            embed.AddField("Телосложение", $"{person.constitution}");
            embed.AddField("Интеллект", $"{person.intelligence}");
            embed.AddField("Мудрость", $"{person.wisdom}");

            var emb = embed.Build();

            return emb;
        }


        JsonSerializer serializer = new JsonSerializer();
        public Task CreateFile(string path)
        {
            Person person = new Person
            {

                points = 19,
                strength = 8,
                agility = 8,
                constitution = 8,
                intelligence = 8,
                wisdom = 8

            };
            using (StreamWriter sw = File.CreateText(path))
            {
                serializer.Serialize(sw, person);
            }
            return Task.CompletedTask;
        }



        public Person Info(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                Person persona = (Person)this.serializer.Deserialize(file, typeof(Person));

                return persona;
            }


        }

        void Input(string path, Person person)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                serializer.Serialize(sw, person);
            }
        }

        

        public Task FillingStats(SocketMessageComponent arg)
        {
            var path = $@"{(arg.User.Id).ToString()}.json";
            if (!File.Exists(path))
            {
                arg.Channel.SendMessageAsync($"{arg.User.Mention}, прекратите нажимать на чужие кнопки, у вас нет персонажа. Создайте своего с помощью команды !создать персонажа.");
                return Task.CompletedTask;
            }

            Person personi;
            var cusID = arg.Data.CustomId;
            personi = Info(path);
            // checking is user wanting to increase ability scores with 0 free points
            if (personi.points == 0 && (cusID == "s1" || cusID == "l1" || cusID == "t1" || cusID == "i1" || cusID == "m1"))
            {
                arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 0 из 19 свободных очков, увеличение атрибутов недоступно.");
                return Task.CompletedTask;
            }

            // checking is user wanting to decrease ability scores with maximum free points
            if (personi.points == 19 && (cusID == "s2" || cusID == "l2" || cusID == "t2" || cusID == "i2" || cusID == "m2"))
            {
                arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 19 из 19 свободных очков, уменьшение атрибутов недоступно.");
                return Task.CompletedTask;
            }



            switch (cusID)
            {
                case "s1":
                    if (personi.strength >= 16)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 16 из 16 очков данной характеристики (максимум)");
                        return Task.CompletedTask;
                    }

                    personi.strength++;
                    personi.points--;
                    Input(path, personi);
                    break;

                case "l1":
                    if (personi.agility >= 16)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 16 из 16 очков данной характеристики (максимум)");
                        return Task.CompletedTask;
                    }

                    personi.agility++;
                    personi.points--;
                    Input(path, personi);
                    break;

                case "t1":
                    if (personi.constitution >= 16)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 16 из 16 очков данной характеристики (максимум)");
                        return Task.CompletedTask;
                    }

                    personi.constitution++;
                    personi.points--;
                    Input(path, personi);
                    break;

                case "i1":
                    if (personi.intelligence >= 16)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 16 из 16 очков данной характеристики (максимум)");
                        return Task.CompletedTask;
                    }

                    personi.intelligence++;
                    personi.points--;
                    Input(path, personi);
                    break;

                case "m1":
                    if (personi.wisdom >= 16)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 16 из 16 очков данной характеристики (максимум)");
                        return Task.CompletedTask;
                    }

                    personi.wisdom++;
                    personi.points--;
                    Input(path, personi);
                    break;

                case "s2":
                    if (personi.strength <= 8)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 8 из 16 очков данной характеристики (минимум)");
                        return Task.CompletedTask;
                    }

                    personi.strength--;
                    personi.points++;
                    Input(path, personi);
                    break;

                case "l2":
                    if (personi.agility <= 8)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 8 из 16 очков данной характеристики (минимум)");
                        return Task.CompletedTask;
                    }

                    personi.agility--;
                    personi.points++;
                    Input(path, personi);
                    break;

                case "t2":
                    if (personi.constitution <= 8)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 8 из 16 очков данной характеристики (минимум)");
                        return Task.CompletedTask;
                    }

                    personi.constitution--;
                    personi.points++;
                    Input(path, personi);
                    break;

                case "i2":
                    if (personi.intelligence <= 8)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 8 из 16 очков данной характеристики (минимум)");
                        return Task.CompletedTask;
                    }

                    personi.intelligence--;
                    personi.points++;
                    Input(path, personi);
                    break;

                case "m2":
                    if (personi.wisdom <= 8)
                    {
                        arg.Channel.SendMessageAsync($"{arg.User.Mention}, у вас 8 из 16 очков данной характеристики (минимум)");
                        return Task.CompletedTask;
                    }

                    personi.wisdom--;
                    personi.points++;
                    Input(path, personi);
                    break;

            }
            return Task.CompletedTask;
        }

        public void ManualChangeStats(string path, Commands.TypeStats type, int num)
        {
            Person person = Info(path);
            switch (type)
            {
                case Commands.TypeStats.Strength:
                    person.strength= person.strength + num;
                    break;
                case Commands.TypeStats.Agility:
                    person.agility = person.agility + num;
                    break;
                case Commands.TypeStats.Constitution:
                    person.constitution = person.constitution + num;
                    break;
                case Commands.TypeStats.Intelligence:
                    person.intelligence= person.intelligence + num;
                    break;
                case Commands.TypeStats.Wisdom:
                    person.wisdom = person.wisdom + num;
                    break;
                default:
                    
                    break;
            }
            Input(path, person);
            return;
        }

    }

}

