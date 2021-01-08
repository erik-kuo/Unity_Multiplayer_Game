using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
	public static void Welcome(Packet _packet)
	{
		string _msg = _packet.ReadString();
		int _myId = _packet.ReadInt();

		Debug.Log($"Message from server: {_msg}");
		Client.instance.myId = _myId;
		ClientSend.WelcomeReceived();

		// Now that we have the client's id, connect UDP
		Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
	}

	public static void UDPTest(Packet _packet)
	{
		string _msg = _packet.ReadString();

		Debug.Log($"Received packet via UDP. Contains mesage: {_msg}");
		ClientSend.UDPTestReceived();
	}

	public static void SpawnPlayer(Packet _packet)
	{
		int _id = _packet.ReadInt();
		bool _red = _packet.ReadBool();
		Vector3 _position = _packet.ReadVector3();
		Quaternion _rotation = _packet.ReadQuaternion();
		float _timeRemaining = _packet.ReadFloat();

		GameManager.instance.SpawnPlayer(_id, _red, _position, _rotation, _timeRemaining);
	}
	
	public static void PlayerTransform(Packet _packet)
	{
		int _id = _packet.ReadInt();
		Vector3 _position = _packet.ReadVector3();
		Quaternion _rotation = _packet.ReadQuaternion();
		if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
		{
			_player.astronaut.gameObject.transform.position = _position;
			_player.astronaut.gameObject.transform.rotation = _rotation;
		}
	}
	
	public static void PlayerDisconnected(Packet _packet)
	{
		int _id = _packet.ReadInt();

		Destroy(GameManager.players[_id].gameObject);
		GameManager.players.Remove(_id);
	}

	public static void CPBAmount(Packet _packet)
    {
		string _type = _packet.ReadString();
		float _amount = _packet.ReadFloat();
		if (GameManager.CPBs.TryGetValue(_type, out CPB _cpb))
		{
			_cpb.SetAmount(_amount);
		}
	}

	public static void PlayerWeaponState(Packet _packet)
    {
		int _id = _packet.ReadInt();
		bool _state = _packet.ReadBool();

		if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
		{
			if (_state)
            {
				_player.astronaut.SpawnWeapon();
			}
            else
            {
				_player.astronaut.DestroyWeapon();
            }
		}
	}

	public static void UpdateWeaponRotation(Packet _packet)
    {
		int _id = _packet.ReadInt();
		Quaternion _rotation = _packet.ReadQuaternion();

		if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
		{
			_player.astronaut.SetWeaponRotation(_rotation);
		}
	}

	public static void SpawnProjectile(Packet _packet)
	{
		int _playerId = _packet.ReadInt();

		GameManager.players[_playerId].astronaut.Shoot();
	}

	public static void SpawnBomb(Packet _packet)
	{
		int _playerId = _packet.ReadInt();

		GameManager.players[_playerId].astronaut.DropBomb();
	}

	public static void EndGame(Packet _packet)
	{
		Debug.Log("EndGame!");
		Application.Quit();
		#if UNITY_EDITOR
		if (EditorApplication.isPlaying)
		{
			UnityEditor.EditorApplication.isPlaying = false;
		}
		#endif
	}
	
	public static void UpdateAnimation(Packet _packet)
    {
		int _id = _packet.ReadInt();
		int _cs = _packet.ReadInt();

		if (GameManager.players[_id].astronaut.red)
        {
			GameManager.players[_id].astronaut.gameObject.GetComponent<actionAni>().UpdateAnimation((CharacterStats)_cs);
        }
    }
}