using System;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Discord.Commands;
using Discord.Webhook;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSanya
{
    class EmbedCreateClass
    {

        public Embed EmbedCreate(string Text)
        {
            var embed = new EmbedBuilder
            {
                Color = Color.Blue,

            };

            embed.AddField("Результат броска", Text);
            var emb = embed.Build();
            return emb;
        }

        public Embed EmbedCreate(string header, string Text)
        {
            var embed = new EmbedBuilder
            {
                Color = Color.Blue,

            };

            embed.AddField(header, Text);
            var emb = embed.Build();
            return emb;
        }

        public Embed EmbedCreateStat(int strength, int agility, int constitution, int intelligence, int wisdom)
        {
            var embed = new EmbedBuilder
            {
                Color = Color.Blue,
                Footer = null,
                Description = null,
                Title = null,


            };


            embed.AddField("Сила", $"{strength.ToString()}");
            embed.AddField("Ловкость", $"{agility.ToString()}");
            embed.AddField("Телосложение", $"{constitution.ToString()}");
            embed.AddField("Интеллект", $"{intelligence.ToString()}");
            embed.AddField("Мудрость", $"{wisdom.ToString()}");

            var emb = embed.Build();

            return emb;
        }


        public MessageComponent Button(SocketMessage msg)
        {
            var auth = msg.Author;
            ComponentBuilder builder = new ComponentBuilder()
                .WithButton("Сил +1", "s1")
                .WithButton("Лов +1", "l1")
                .WithButton("Тел +1", "t1")
                .WithButton("Инт +1", "i1")
                .WithButton("Муд +1", "m1")
                .WithButton("Сил -1", "s2")
                .WithButton("Лов -1", "l2")
                .WithButton("Тел -1", "t2")
                .WithButton("Инт -1", "i2")
                .WithButton("Муд -1", "m2");
           

            return builder.Build();
        }


        public Embed EmbedCheckStat(string[] Text)
        {
            var embed = new EmbedBuilder
            {
                Color = Color.Blue,
                Footer = null,
                Description = null,
                Title = null,


            };
            for (int i = 2; i < Text.Length; ++i)
            {

                string[] str1 = Text[i].Split(' ');
                embed.AddField(str1[0], str1[1]);
            }

            var emb = embed.Build();

            return emb;
        }

    }
}
