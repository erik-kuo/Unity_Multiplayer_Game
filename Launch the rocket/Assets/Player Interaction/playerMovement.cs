using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

	Rigidbody2D rigidbody2d;
	public float speed = 3f;
	public Animator animator;
	Vector2 movement;

	private bool isUsingWeapon = false;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
		{
			ClientSend.CharacterMovement((int)CharacterStats.Walk);
		}
		else if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
		{
			ClientSend.CharacterMovement((int)CharacterStats.Idle);
		}

		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);

		// Press G to take out or put back weapon
		if (!gameObject.GetComponent<AstronautManager>().red)
		{
			if (!isUsingWeapon)
			{
				if (Input.GetKeyDown(KeyCode.G))
				{
					ClientSend.PlayerUsingWeapon(true);
					isUsingWeapon = true;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.G))
				{
					ClientSend.PlayerUsingWeapon(false);
					isUsingWeapon = false;
				}
			}
		}

		// Press B to drop bomb
		if (Input.GetKeyDown(KeyCode.B))
		{
			ClientSend.PlayerDropBomb();
		}
	}

	// void FixedUpdate()
	// {
	//     Vector3 position = rigidbody2d.position;
	//     position.x = position.x + 3.0f * horizontal * Time.deltaTime;
	//     position.y = position.y + 3.0f * vertical * Time.deltaTime;
	//     position.z = 0;
	//     position = new Vector3(
	//       Mathf.Clamp(position.x,-22f,21.5f),
	//       Mathf.Clamp(position.y,-16f,15.5f),
	//       0
	//     );
	//
	//     rigidbody2d.MovePosition(position);
	// }

	void FixedUpdate(){
		Vector2 position = rigidbody2d.position;
		position = rigidbody2d.position + movement * speed * Time.fixedDeltaTime;
		position = new Vector2(
		Mathf.Clamp(position.x,-22f,21.5f), Mathf.Clamp(position.y,-16f,15.5f));
		rigidbody2d.MovePosition(position);
		ClientSend.Player2DPosition(position);
	}

	void OnTriggerStay2D(Collider2D aaa)
	{
		if (gameObject.tag == "LocalPlayer")
        {
			if (aaa.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.Space))
			{
				ClientSend.CharacterStatus((int)CharacterStats.Water);
			}
		}
	}

	void OnCollisionStay2D(Collision2D aaa) //aaa為自定義碰撞事件
	{
		if (gameObject.tag == "LocalPlayer")
        {
			if (aaa.gameObject.tag == "Coal" && Input.GetKeyDown(KeyCode.Space))
			{
				ClientSend.CharacterStatus((int)CharacterStats.Coal);
			}
			if (aaa.gameObject.tag == "Metal" && Input.GetKeyDown(KeyCode.Space))
			{
				ClientSend.CharacterStatus((int)CharacterStats.Metal);
			}
			if (aaa.gameObject.tag == "lab" && Input.GetKeyDown(KeyCode.Space))
			{
				ClientSend.CharacterStatus((int)CharacterStats.Lab);
			}
		}
	}
}
