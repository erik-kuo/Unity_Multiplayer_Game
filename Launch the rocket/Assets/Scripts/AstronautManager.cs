using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautManager : MonoBehaviour
{
	public GameObject gun;
	public Transform weaponSpawnPoint;
	public bool red;

	private GameObject currentWeapon;

	public void Initialize(bool _red)
	{
		red = _red;
	}
	private void Update()
	{
	}

	public void SpawnWeapon()
    {
		currentWeapon = Instantiate(gun, weaponSpawnPoint.position, Quaternion.identity);
		currentWeapon.transform.parent = gameObject.transform;
	}

	public void DestroyWeapon()
    {
		Destroy(currentWeapon);
	}

	public void SetWeaponRotation(Quaternion _rotation)
    {
		if (currentWeapon != null)
        {
			currentWeapon.transform.rotation = _rotation;
        }
    }

	public void Shoot()
    {
		currentWeapon.GetComponent<Weapon>().Shoot();
    }
}
