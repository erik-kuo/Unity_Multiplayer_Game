using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class launchRocket : MonoBehaviour
{
	public Slider progressBar;
	public SpriteRenderer image;
	public Sprite[] spriteArray;
	GameObject localPlayerPrefab;
	private float coal;
	private float water;
	private float metal;
	private float progress;
	//public ProgressBar progressBar;


	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if(progressBar == null)
        {
			if (GameManager.progressBar != null)
			{
				progressBar = GameManager.progressBar.GetComponent<Slider>();
			}
		}
		else
        {
			if (progressBar.value <= 0.5f)
			{
				if (progressBar.value <= 0.33f)
				{
					image.sprite = spriteArray[0];
				}
				else
				{
					image.sprite = spriteArray[1];
				}
			}
			else
			{
				if (progressBar.value <= 0.85f)
				{
					image.sprite = spriteArray[2];
				}
				else
				{
					image.sprite = spriteArray[3];
				}
			}
		}
	}
}
