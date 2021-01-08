using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject projectile;
	public GameObject shotEffect;
	public Transform shotPoint;
	public void Shoot()
    {
		Destroy(Instantiate(shotEffect, shotPoint.position, Quaternion.identity), 2.0f);
		Instantiate(projectile, shotPoint.position, transform.rotation);
	}
}