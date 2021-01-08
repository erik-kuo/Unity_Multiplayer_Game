using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlueCharacterStats{
  Idle =0, Walk = 1, Bomb = 2,
}

public class BlueAni : MonoBehaviour
{
	public Animator anim;
	public BlueCharacterStats cs;
	public Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
	  if(Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical")){
		cs = BlueCharacterStats.Walk;
	  }else if(Input.GetButtonUp("Horizontal")||Input.GetButtonUp("Vertical")){
		cs = BlueCharacterStats.Idle;
	  }
	  if(cs == BlueCharacterStats.Idle){
		anim.SetBool("walk",false);
	  }
	  if(cs == BlueCharacterStats.Walk){
		anim.SetBool("walk",true);
	  }
	}

	IEnumerator BombActionTime()
	{
		yield return new WaitForSeconds(2);
		anim.SetBool("isBombed", false);
		rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
	}

	public void getBombed()
	{
		anim.SetBool("isBombed", true);
		rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
		StartCoroutine(BombActionTime());
	}
}
