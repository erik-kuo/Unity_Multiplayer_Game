using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	class GameLogic
	{
		/*
		public static Dictionary<string, float> progressBar = new Dictionary<string, float>();
		public static float OverallProgress;

		public static void Initial()
        {
			progressBar.Add("Coal", 0);
			progressBar.Add(" Water", 0);
			progressBar.Add(" Metal", 0);
			OverallProgress = 0;
        }
		*/
		/// <summar
		/// y>Runs all game logic.</summary>
		public static void Update()
		{
			foreach (Client _client in Server.clients.Values)
			{
				if (_client.player != null)
				{
					_client.player.Update();
				}
			}
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