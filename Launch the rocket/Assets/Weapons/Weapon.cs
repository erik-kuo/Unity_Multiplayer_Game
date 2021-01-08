using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float offset;

	public GameObject projectile;
	public GameObject shotEffect;
	public Transform shotPoint;
	// public GameObject Player;
	// public Animator camAnim;

	private float timeBtwShots;
	public float startTimeBtwShots;

	private void Update()
	{
		// Handles the weapon facing direction
		// facingDirection facingDirection = Player.GetComponent<facingDirection>();
		// if (facingDirection.facing == "left")
		// {
		//     transform.Rotate(0, 0, 0);
		// }
		// else
		// {
		//     transform.Rotate(0, 180, 0);
		// }

		// Handles the weapon rotation
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
		ClientSend.WeaponRotation(Quaternion.Euler(0f, 0f, rotZ + offset));

		if (timeBtwShots <= 0)
		{
			if (Input.GetMouseButton(0))
			{
				ClientSend.PlayerShoot();
				timeBtwShots = startTimeBtwShots;
			}
		}
		else {
			timeBtwShots -= Time.deltaTime;
		}
	   
	}

	public void Shoot()
    {
		Destroy(Instantiate(shotEffect, shotPoint.position, Quaternion.identity), 2.0f);
		Instantiate(projectile, shotPoint.position, transform.rotation);
	}
}