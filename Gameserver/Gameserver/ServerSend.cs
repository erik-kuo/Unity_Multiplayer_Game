using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
	class ServerSend
	{
		/// <summary>Sends a packet to a client via TCP.</summary>
		/// <param name="_toClient">The client to send the packet the packet to.</param>
		/// <param name="_packet">The packet to send to the client.</param>
		private static void SendTCPData(int _toClient, Packet _packet)
		{
			_packet.WriteLength();
			Server.clients[_toClient].tcp.SendData(_packet);
		}

		/// <summary>Sends a packet to a client via UDP.</summary>
		/// <param name="_toClient">The client to send the packet the packet to.</param>
		/// <param name="_packet">The packet to send to the client.</param>
		private static void SendUDPData(int _toClient, Packet _packet)
		{
			_packet.WriteLength();
			Server.clients[_toClient].udp.SendData(_packet);
		}

		/// <summary>Sends a packet to all clients via TCP.</summary>
		/// <param name="_packet">The packet to send.</param>
		private static void SendTCPDataToAll(Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				Server.clients[i].tcp.SendData(_packet);
			}
		}
		/// <summary>Sends a packet to all clients except one via TCP.</summary>
		/// <param name="_exceptClient">The client to NOT send the data to.</param>
		/// <param name="_packet">The packet to send.</param>
		private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				if (i != _exceptClient)
				{
					Server.clients[i].tcp.SendData(_packet);
				}
			}
		}

		/// <summary>Sends a packet to all clients via UDP.</summary>
		/// <param name="_packet">The packet to send.</param>
		private static void SendUDPDataToAll(Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				Server.clients[i].udp.SendData(_packet);
			}
		}
		/// <summary>Sends a packet to all clients except one via UDP.</summary>
		/// <param name="_exceptClient">The client to NOT send the data to.</param>
		/// <param name="_packet">The packet to send.</param>
		private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				if (i != _exceptClient)
				{
					Server.clients[i].udp.SendData(_packet);
				}
			}
		}

		#region Packets
		/// <summary>Sends a welcome message to the given client.</summary>
		/// <param name="_toClient">The client to send the packet to.</param>
		/// <param name="_msg">The message to send.</param>
		public static void Welcome(int _toClient, string _msg)
		{
			using (Packet _packet = new Packet((int)ServerPackets.welcome))
			{
				_packet.Write(_msg);
				_packet.Write(_toClient);

				SendTCPData(_toClient, _packet);
			}
		}

		public static void UDPTest(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.udpTest))
			{
				_packet.Write("A test packet for UDP.");

				SendUDPData(_toClient, _packet);
			}
		}

		/// <summary>Tells a client to spawn a player.</summary>
		/// <param name="_toClient">The client that should spawn the player.</param>
		/// <param name="_player">The player to spawn.</param>
		public static void SpawnPlayer(int _toClient, Player _player, float _timeRemaining)
		{
			using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.red);
				_packet.Write(_player.position);
				_packet.Write(_player.rotation);
				_packet.Write(_timeRemaining);
				_packet.Write(_player.charaterStatus);
				_packet.Write(_player.isUsingWeapon);
				_packet.Write(_player.weaponRotation);

				SendTCPData(_toClient, _packet);
			}
		}
		
		/// <summary>Sends a player's updated position to all clients.</summary>
		/// <param name="_player">The player whose position to update.</param>
		public static void PlayerTransform(Player _player)
		{
			using (Packet _packet = new Packet((int)ServerPackets.playerTransform))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.position2D);
				_packet.Write(_player.rotation);

				SendUDPDataToAll(_player.id, _packet);
			}
		}

		public static void PlayerDisconnected(int _playerId)
		{
			using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
			{
				_packet.Write(_playerId);

				SendTCPDataToAll(_packet);
			}
		}

		public static void CPBAmount(string _type, float _amount)
        {
			using (Packet _packet = new Packet((int)ServerPackets.CPBAmount))
			{
				_packet.Write(_type);
				_packet.Write(_amount);

				SendTCPDataToAll(_packet);
			}
		}

		public static void PlayerWeaponState(Player _player)
        {
			using (Packet _packet = new Packet((int)ServerPackets.playerWeaponState))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.isUsingWeapon);

				SendTCPDataToAll(_packet);
			}
		}

		public static void UpdateWeaponRotation(Player _player)
        {
			using (Packet _packet = new Packet((int)ServerPackets.updateWeaponRotation))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.weaponRotation);

				SendUDPDataToAll(_packet);
			}
		}

		public static void SpawnProjectile(int _id)
		{
			using (Packet _packet = new Packet((int)ServerPackets.spawnProjectile))
			{
				_packet.Write(_id);

				SendTCPDataToAll(_packet);
			}
		}

		public static void SpawnBomb(int _id)
		{
			using (Packet _packet = new Packet((int)ServerPackets.spawnBomb))
			{
				_packet.Write(_id);

				SendTCPDataToAll(_packet);
			}
		}

		public static void EndGame()
		{
			using (Packet _packet = new Packet((int)ServerPackets.endGame))
			{
				SendTCPDataToAll(_packet);
			}
		}
		public static void UpdateAnimation(int _id, int _status)
		{
			using (Packet _packet = new Packet((int)ServerPackets.updateAnimation))
			{
				_packet.Write(_id);
				_packet.Write(_status);
				SendTCPDataToAll(_packet);
			}
		}

		public static void CharacterMovement(Player _player)
        {
			using (Packet _packet = new Packet((int)ServerPackets.characterMovement))
            {
				_packet.Write(_player.id);
				_packet.Write(_player.characterMovement);
				SendUDPDataToAll(_packet);
            }
        }
		#endregion
	}
}