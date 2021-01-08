using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public enum CharacterStats{
  Idle =0, Walk = 1, Coal = 2, Metal = 3, Water = 4, Lab = 5,
}

public class actionAni : MonoBehaviour
{

	public Animator anim;
	public Animation animation;
	public CharacterStats cs;
	public Rigidbody2D rb;

	public CPB coalSlider;
	public CPB waterSlider;
	public CPB metalSlider;

	public Vector3 movement;
	// Start is called before the first frame update
	void Start()
	{
	  anim = gameObject.GetComponent<Animator>();
	  animation = gameObject.GetComponent<Animation>();
	  rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		/*
	  if(Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical")){
		cs = CharacterStats.Walk;
	  }else if(Input.GetButtonUp("Horizontal")||Input.GetButtonUp("Vertical")){
		cs = CharacterStats.Idle;
	  }
		*/
	  if(cs == CharacterStats.Idle){
		anim.SetBool("walk",false);
	  }
	  if(cs == CharacterStats.Walk){
		anim.SetBool("walk",true);
	  }
	anim.SetFloat("Horizontal", movement.x);
	anim.SetFloat("Vertical", movement.y);
	}

	IEnumerator actionTime(){
	  yield return new WaitForSeconds(2);
	  anim.SetBool("coal",false);
	  anim.SetBool("metal",false);
	  anim.SetBool("water",false);
	  anim.SetBool("develop",true);
	  yield return new WaitForSeconds(1);
	  rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
	}

	IEnumerator BombActionTime()
	{
		yield return new WaitForSeconds(2);
		anim.SetBool("isBombed", false);
		rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
	}


	public void UpdateAnimation(CharacterStats _cs)
	{
		switch (_cs)
		{
			case CharacterStats.Water:
				anim.SetBool("water", true);
				rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
				anim.SetBool("increase_water", true);
				StartCoroutine(actionTime());
				break;
			case CharacterStats.Coal:
				anim.SetBool("coal", true);
				rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
				anim.SetBool("increase_coal", true);
				StartCoroutine(actionTime());
				break;
			case CharacterStats.Metal:
				anim.SetBool("metal", true);
				rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
				anim.SetBool("increase_metal", true);
				StartCoroutine(actionTime());
				break;
			case CharacterStats.Lab:
				anim.SetBool("develop", false);
				if (anim.GetBool("increase_coal"))
				{
					if (gameObject.tag == "LocalPlayer")
                    {
						coalSlider.UpdateAmount();
					}
					anim.SetBool("increase_coal", false);
				}
				if (anim.GetBool("increase_metal"))
				{
					if (gameObject.tag == "LocalPlayer")
					{
						metalSlider.UpdateAmount();
					}
					anim.SetBool("increase_metal", false);
				}
				if (anim.GetBool("increase_water"))
				{
					if (gameObject.tag == "LocalPlayer")
					{
						waterSlider.UpdateAmount();
					}
					anim.SetBool("increase_water", false);
				}
				break;
		}
	}

	public void getBombed()
	{
		anim.SetBool("increase_metal", false);
		anim.SetBool("increase_water", false);
		anim.SetBool("increase_coal", false);
		anim.SetBool("isBombed", true);
		rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
		StartCoroutine(BombActionTime());
	}
}
