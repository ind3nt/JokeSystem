using Exiled.Events.EventArgs.Player;

namespace JokeSystem.Events
{
    class PlayerEvents
    {
        public static void OnVerified(VerifiedEventArgs ev)
        {
            if (ev.Player.DoNotTrack)
                return;

            if (ev.Player == null)
                return;

            Database.CheckInDatabase(ev.Player);
        }
    }
}
