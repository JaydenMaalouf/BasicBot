using BasicBot.Classes;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasicBot.Commands
{
    public class BasicCommands : ModuleBase<CommandContext>
    {
        [Command("give")]
        [Alias("pass")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task GiveRole(IRole role, IGuildUser user) => await GiveRole(user, role);

        [Command("give")]
        [Alias("pass")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task GiveRole(IGuildUser user, IRole role)
        {
            await user.AddRoleAsync(role);
            await ReplyAsync($"Added role for {user.Mention} successfully!");
        }

        [Command("prefix")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetPrefix([Remainder]string newPrefix)
        {
            var settings = SettingsHandler.GetSettings();
            settings.BotPrefix = newPrefix;
            SettingsHandler.SaveSettings();
            await ReplyAsync($"Prefix has been to `{newPrefix}`");
        }

        [Command("coinflip")]
        [Alias("cf")]
        [RequireContext(ContextType.Guild)]
        public async Task CoinFlip()
        {
            var randomiser = new Random();
            var newRandom = randomiser.Next(0, 2);
            if (newRandom == 0)
            {
                await ReplyAsync("It's heads!");
            }
            else
            {
                await ReplyAsync("It's tails!");
            }
        }
    }
}
