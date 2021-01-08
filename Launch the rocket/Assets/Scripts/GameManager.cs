using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
	public static Dictionary<string, CPB> CPBs = new Dictionary<string, CPB>();
	public static ProgressBar progressBar;
	
	public GameObject localRedPlayerPrefab;
	public GameObject localBluePlayerPrefab;
	public GameObject redPlayerPrefab;
	public GameObject bluePlayerPrefab;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Debug.Log("Instance already exists, destroying object!");
			Destroy(this);
		}
	}

	/// <summary>Spawns a player.</summary>
	/// <param name="_id">The player's ID.</param>
	/// <param name="_red">The player's team.</param>
	/// <param name="_position">The player's starting position.</param>
	/// <param name="_rotation">The player's starting rotation.</param>
	public void SpawnPlayer(int _id, bool _red, Vector3 _position, Quaternion _rotation, float _timeRemaining)
	{
		GameObject _player;
		if (_id == Client.instance.myId)
		{
			if (_red)
            {
				_player = Instantiate(localRedPlayerPrefab, _position, _rotation);
			}
            else
            {
				_player = Instantiate(localBluePlayerPrefab, _position, _rotation);
			}
			progressBar = _player.transform.Find("HUD").Find("Overall Progress Bar").GetComponent<ProgressBar>();
			CPBs.Add("Coal", _player.transform.Find("HUD").Find("Coal Progress Bar").GetComponent<CPB>());
			CPBs.Add(" Water", _player.transform.Find("HUD").Find("Water Progress Bar").GetComponent<CPB>());
			CPBs.Add(" Metal", _player.transform.Find("HUD").Find("Metal Progress Bar").GetComponent<CPB>());
			_player.transform.Find("HUD").Find("TImer counter").GetComponent<countdownTimer>().timeRemaining = _timeRemaining;
			/*
			if (_player.transform.Find("HUD").Find("TImer counter") == null)
            {
				Debug.Log("no counter");
            }
			*/
		}
		else
		{
			if (_red)
			{
				_player = Instantiate(redPlayerPrefab, _position, _rotation);
			}
			else
			{
				_player = Instantiate(bluePlayerPrefab, _position, _rotation);
			}
		}

		_player.GetComponent<PlayerManager>().Initialize(_id, _red);
		players.Add(_id, _player.GetComponent<PlayerManager>());
	}
}