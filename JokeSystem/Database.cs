using System;
using Exiled.API.Features;
using MySql.Data.MySqlClient;

namespace JokeSystem
{
    class Database
    {
        static readonly string DBConnect = Plugin.Instance.Config.DBConnect;
        static MySqlConnection MSConnection;
        static MySqlCommand MSCommand;

        public static bool OpenConnection()
        {
            try
            {
                MSConnection = new MySqlConnection(DBConnect);
                MSConnection.Open();
                MSCommand = new MySqlCommand();
                MSCommand.Connection = MSConnection;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Не удалось соедениться с базой данный!");
                Log.Error(ex);
                return false;
            }
        }

        public static void CloseConnection()
        {
            if (MSConnection.State == System.Data.ConnectionState.Open)
                MSConnection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return MSConnection;
        }

        public static void AddToDatabase(PlayerInfo playerinfo)
        {
            MSCommand.CommandText = @"INSERT INTO jokerplayers VALUES('" + playerinfo.SteamID + "', '" + playerinfo.Chance + "')";
            MSCommand.ExecuteNonQuery();
        }

        public static PlayerInfo CheckInDatabase(Player player)
        {
            MSCommand.CommandText = @"SELECT Chance from jokerplayers WHERE SteamID = '" + player.RawUserId + "' ";
            Object result = MSCommand.ExecuteScalar();

            if (result == null)
            {
                var newPlayerInfo = new PlayerInfo(player.RawUserId, 0);

                AddToDatabase(newPlayerInfo);

                return newPlayerInfo;
            }

            var reciviedData = new PlayerInfo(player.RawUserId, Convert.ToInt32(result));

            return reciviedData;
        }

        public static void EditInDatabase(PlayerInfo playerInfo)
        {
            MSCommand.CommandText = @"UPDATE jokerplayers SET Chance = '" + playerInfo.Chance + "' WHERE SteamID = '" + playerInfo.SteamID + "';";
            MSCommand.ExecuteNonQuery();
        }
    }

    public class PlayerInfo
    {
        public string SteamID { get; set; }

        public int Chance { get; set; }

        public PlayerInfo(string SteamID, int Chance)
        {
            this.SteamID = SteamID;
            this.Chance = Chance;
        }
    }
}
