using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
	class Player
	{
		public int id;
		public bool red;
		public bool isUsingWeapon = false;
		public int charaterStatus = 0;

		public Vector3 position;
		public Vector3 position2D;
		public Quaternion rotation;
		public Quaternion weaponRotation;

		public Player(int _id, bool _red, Vector3 _spawnPosition)
		{
			id = _id;
			red = _red;
			position = _spawnPosition;
			position2D = new Vector3((float)-11.52727, (float)-3.569426, (float)26.26336);
			rotation = Quaternion.Identity;
			weaponRotation = Quaternion.Identity;
		}
		
		/// <summary>Processes player input and moves the player.</summary>
		public void Update()
		{
			ServerSend.PlayerTransform(this);
			if (isUsingWeapon)
            {
				ServerSend.UpdateWeaponRotation(this);
            }
		}

		public void Set2DPosition(Vector3 _position, Quaternion _rotation)
        {
			position2D.X = _position.X;
			position2D.Y = _position.Y;
			rotation = _rotation;
        }

		public void SetWeaponUsing(bool _state)
		{
			if (!red)
			{
				if(_state != isUsingWeapon)
                {
					isUsingWeapon = _state;
					ServerSend.PlayerWeaponState(this);
				}
			}
		}

		public void SetWeaponRotation(Quaternion _rotation)
        {
			weaponRotation = _rotation;
        }
	}
}