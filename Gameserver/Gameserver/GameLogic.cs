using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer
{
	class GameLogic
	{
		private static int lastPlayerCount = 0;
		private static int newPlayerCount = 0;

		public static Dictionary<string, float> progressBar = new Dictionary<string, float>();
		public static float OverallProgress = 0;

		public static void Init()
        {
			progressBar.Add("Coal", 0);
			progressBar.Add(" Water", 0);
			progressBar.Add(" Metal", 0);
        }

		public static void Reset()
        {
			progressBar["Coal"] = 0;
			progressBar[" Water"] = 0;
			progressBar[" Metal"] = 0;
        }

		/// <summar
		/// y>Runs all game logic.</summary>
		public static void Update()
		{
			OverallProgress = progressBar.Min(x => x.Value);
			if (OverallProgress == 100)
            {
				ServerSend.EndGame();
				Server.serverTimer.Reset();
            }

			newPlayerCount = 0;

			foreach (Client _client in Server.clients.Values)
			{
				if (_client.player != null)
				{
					newPlayerCount += 1;
					_client.player.Update();
				}
			}
			if (lastPlayerCount == 0 && newPlayerCount > 0 )
            {
				Server.serverTimer.Start();
            }
			if (lastPlayerCount > 0 && newPlayerCount == 0)
            {
				Server.serverTimer.Reset();
            }
			lastPlayerCount = newPlayerCount;
			Server.serverTimer.Update();
			/*
			OverallProgress = Math.Min(progressBar["Coal"], Math.Min(progressBar["Water"], progressBar["Metal"]));
			if (OverallProgress > 100) // and time remain > red win 
            {
				Console.WriteLine("progress completed.");
            }
			*/
			ThreadManager.UpdateMain();
		}
	}
}