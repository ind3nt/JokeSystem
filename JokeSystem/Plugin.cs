using System;
using Exiled.API.Features;
using JokeSystem.Events;

namespace JokeSystem
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;

        public override string Author => "sky3z";

        public override string Name => "JokeSystem";

        public override string Prefix => "JokeSystem";

        public override Version Version => base.Version;

        public string SoundsPath { get; private set; }

        public override void OnEnabled()
        {   
            base.OnEnabled();
            Instance = this;
            SoundsPath = Instance.ConfigPath.Replace("\\7777.yml", string.Empty).Replace($"\\{Server.Port}.yml", string.Empty);

            Exiled.Events.Handlers.Player.Verified += PlayerEvents.OnVerified;

            Database.OpenConnection();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Instance = null;
            SoundsPath = null;

            Exiled.Events.Handlers.Player.Verified -= PlayerEvents.OnVerified;

            Database.CloseConnection();
        }
    }
}
