using System.Linq;
using SCPSLAudioApi.AudioCore;
using Exiled.API.Features;
using UnityEngine;
using Random = System.Random;
using MEC;
using Mirror;
using Exiled.API.Enums;

namespace JokeSystem
{
    class Jokesystem
    {
        private static Random random = new Random();

        public static bool InitializeJoke(Player player)
        {
            PlayerInfo playerInfo = Database.CheckInDatabase(player);

            AddChance(playerInfo);

            string joke = Plugin.Instance.Config.Jokes.RandomItem();

            var playerList = Player.List.Where(target => Vector3.Distance(target.Position, player.Position) < 5);

            if (playerInfo.Chance > random.Next(0, 100))
            {
                Play(player, true);

                foreach (Player target in playerList)
                {
                    target.Broadcast(3, joke, Broadcast.BroadcastFlags.Normal, true);
                    target.Health = target.Health + 10f;
                }
                
                return true;
            }

            foreach (Player target in playerList)
            {
                target.Broadcast(3, joke, Broadcast.BroadcastFlags.Normal, true);
                target.Health = target.Health - 10f;
            }

            Play(player, false);
            return false;
        }

        public static void Play(Player player, bool joke)
        {
            var npc = Npc.Spawn("Joker", PlayerRoles.RoleTypeId.Tutorial, player.Position);
            npc.Scale = Vector3.zero;
            npc.Position = player.Position;

            var audioPlayer = AudioPlayerBase.Get(npc.ReferenceHub);

            string soundPath = null;

            if (player.Role.Side == Side.Scp)
            {
                if (joke)
                {
                    soundPath = Plugin.Instance.SoundsPath + "\\scpLaugh\\happy" + Plugin.Instance.Config.Audios.RandomItem();
                }
                else
                {
                    soundPath = Plugin.Instance.SoundsPath + "\\scpLaugh\\bad" + Plugin.Instance.Config.Audios.RandomItem();
                }
            }
            
            if (player.Role.Side == Side.Tutorial || player.Role.Side == Side.Mtf || player.Role.Side == Side.ChaosInsurgency)
            {
                if (joke)
                {
                    soundPath = Plugin.Instance.SoundsPath + "\\playerLaugh\\happy" + Plugin.Instance.Config.Audios.RandomItem();
                }
                else
                {
                    soundPath = Plugin.Instance.SoundsPath + "\\playerLaugh\\bad" + Plugin.Instance.Config.Audios.RandomItem();
                }
            }
            
            audioPlayer.Loop = false;
            audioPlayer.Enqueue(soundPath, 0);
            audioPlayer.Play(0);
            audioPlayer.Volume = 100;

            Timing.CallDelayed(5f, () =>
            {
                AudioPlayerBase.AudioPlayers.Remove(npc.ReferenceHub);
                NetworkServer.Destroy(npc.GameObject);
            });
        }

        public static void AddChance(PlayerInfo playerInfo)
        {
            PlayerInfo newPlayerInfo = new PlayerInfo(playerInfo.SteamID, (playerInfo.Chance + Plugin.Instance.Config.GiveChancePerJoke));

            Database.EditInDatabase(newPlayerInfo);
        }
    }
}
