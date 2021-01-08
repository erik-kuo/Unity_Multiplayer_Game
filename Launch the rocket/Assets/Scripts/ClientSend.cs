using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
	/// <summary>Sends a packet to the server via TCP.</summary>
	/// <param name="_packet">The packet to send to the sever.</param>
	private static void SendTCPData(Packet _packet)
	{
		_packet.WriteLength();
		Client.instance.tcp.SendData(_packet);
	}

	/// <summary>Sends a packet to the server via UDP.</summary>
	/// <param name="_packet">The packet to send to the sever.</param>
	private static void SendUDPData(Packet _packet)
	{
		_packet.WriteLength();
		Client.instance.udp.SendData(_packet);
	}

	#region Packets
	/// <summary>Lets the server know that the welcome message was received.</summary>
	public static void WelcomeReceived()
	{
		using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
		{
			_packet.Write(Client.instance.myId);
			_packet.Write(Client.instance.ip);

			SendTCPData(_packet);
		}
	}

	public static void UDPTestReceived()
	{
		using (Packet _packet = new Packet((int)ClientPackets.udpTestRecieved))
		{
			_packet.Write("Received a UDP packet.");

			SendUDPData(_packet);
		}
	}

	/// <summary>Sends player position and rotation to the server.</summary>
	/// <param name="_position"></param>
	public static void Player2DPosition(Vector2 _position)
	{
		using (Packet _packet = new Packet((int)ClientPackets.player2DPosition))
		{
			_packet.Write(_position);
			if (GameManager.players[Client.instance.myId] != null)
			{
				_packet.Write(GameManager.players[Client.instance.myId].astronaut.gameObject.transform.rotation);
			}

			SendUDPData(_packet);
		}
	}

	public static void CPBUpdate(string _type, float _amount)
	{
		using (Packet _packet = new Packet((int)ClientPackets.CPBUpdate))
		{
			_packet.Write(_type);
			_packet.Write(_amount);

			SendTCPData(_packet);
		}
	}

	public static void PlayerUsingWeapon(bool _state)
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerUsingWeapon))
		{
			_packet.Write(_state);

			SendTCPData(_packet);
		}
	}

	public static void WeaponRotation(Quaternion _rotation)
	{
		using (Packet _packet = new Packet((int)ClientPackets.weaponRotation))
		{
			_packet.Write(_rotation);

			SendUDPData(_packet);
		}
	}

	public static void PlayerShoot()
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
		{
			SendTCPData(_packet);
		}
	}

	public static void PlayerDropBomb()
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerDropbomb))
		{
			SendTCPData(_packet);
		}
	}
	/*
	public static void PlayerShoot(Vector3 _facing)
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
		{
			_packet.Write(_facing);

			SendTCPData(_packet);
		}
	}

	public static void PlayerThrowItem(Vector3 _facing)
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerThrowItem))
		{
			_packet.Write(_facing);

			SendTCPData(_packet);
		}
	}
	*/
	#endregion
}