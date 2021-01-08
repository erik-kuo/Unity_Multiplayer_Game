using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
	class ServerHandle
	{
		public static bool redTeam = true;
		public static void WelcomeReceived(int _fromClient, Packet _packet)
		{
			int _clientIdCheck = _packet.ReadInt();
			string _username = _packet.ReadString();

			Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
			if (_fromClient != _clientIdCheck)
			{
				Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
			}
			Server.clients[_fromClient].SendIntoGame(redTeam);
			redTeam = !redTeam;
		}

		public static void UDPTestReceived(int _fromClient, Packet _packet)
		{
			string _msg = _packet.ReadString();

			Console.Write($"Received packet via UDP. Contains mesage: {_msg}");
		}
		
		public static void Player2DPosition(int _fromClient, Packet _packet)
		{
			Vector3 _position = _packet.ReadVector3();
			Quaternion _rotation = _packet.ReadQuaternion();

			Server.clients[_fromClient].player.Set2DPosition(_position, _rotation);
		}

		public static void CPBUpdate(int _fromClient, Packet _packet)
		{
			string _type = _packet.ReadString();

			GameLogic.progressBar[_type] += Constants.PROGRESS_STEP;
			ServerSend.CPBAmount(_type, GameLogic.progressBar[_type]);
		}
		
		public static void PlayerUsingWeapon(int _fromClient, Packet _packet)
		{
			bool _state = _packet.ReadBool();

			Server.clients[_fromClient].player.SetWeaponUsing(_state);
		}

		public static void WeaponRotation(int _fromClient, Packet _packet)
        {
			Quaternion _rotation = _packet.ReadQuaternion();

			Server.clients[_fromClient].player.SetWeaponRotation(_rotation);
        }

		public static void PlayerShoot(int _fromClient, Packet _packet)
		{
			ServerSend.SpawnProjectile(_fromClient);
		}

		public static void PlayerDropBomb(int _fromClient, Packet _packet)
		{
			ServerSend.SpawnBomb(_fromClient);
		}

		public static void CharacterStatus(int _fromClient, Packet _packet)
        {
			int _status = _packet.ReadInt();
			ServerSend.UpdateAnimation(_fromClient, _status);
        }
	}
}