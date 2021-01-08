using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public AstronautManager astronaut;
	public int id;

	public void Initialize(int _id, bool _red)
	{
		id = _id;
		astronaut = gameObject.transform.Find("Astronaut").GetComponent<AstronautManager>();
		astronaut.Initialize(_red);
	}
}