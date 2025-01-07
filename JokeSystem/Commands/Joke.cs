using System;
using CommandSystem;
using Exiled.API.Features;

namespace JokeSystem.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class AddXPCommand : ICommand
    {
        public string Command => "Joke";

        public string[] Aliases { get; } = { };

        public string Description => "Пошутить";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player.DoNotTrack)
            {
                response = "У вас включенна функция DoNotTrack!";
                return false;
            }

            if (!player.IsAlive)
            {
                response = "Вы не можете использовать эту команду, будучи мертвым!";
                return false;
            }

            bool jokeType = Jokesystem.InitializeJoke(player);

            if (jokeType)
            {
                response = "Вы пошутили смешно!";
                return true;
            }
            
            response = "Вы пошутили не смешно!";
            return true;
        }
    }
}
