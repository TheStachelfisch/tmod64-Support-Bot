﻿using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using tMod64Bot.Utils;

namespace tMod64Bot.Modules
{
    [RequireOwner]
    public class OwnerModule : ModuleBase
    {
        [Command("update")]
        public async Task Update()
        {
            if (!File.Exists("../tMod64Update.bash"))
            {
                await ReplyAsync(embed: EmbedHelper.ErrorEmbed("`tMod64Update.bash` doesn't exist. Stachel forgot to add the script"));
                return;
            }

            var message = await ReplyAsync(embed:new EmbedBuilder()
            {
                Title = "Command output",
                Description = "Starting command..."
            }.Build());

            string result = "../tMod64Update.bash".Bash();
            await message.ModifyAsync(x => x.Embed = new EmbedBuilder
            {
                Title = "Command output",
                Description = result
            }.Build());
        }
    }
}