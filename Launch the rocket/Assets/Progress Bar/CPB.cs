using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CPB : MonoBehaviour
{
   public Transform LoadingBar;
   public Transform TextIndicator;
   public Transform TextResource;
   [SerializeField] public float currentAmount;
   [SerializeField] public float speed;

   void Start()
   {
	   TextIndicator.GetComponent<Text>().text = "0%";
	   TextResource.gameObject.SetActive(true);
	   currentAmount = 0;
   }

   public void UpdateAmount()
   {
	   ClientSend.CPBUpdate(TextResource.gameObject.GetComponent<Text>().text);
	}

	public void SetAmount(float _amount)
	{
		if (currentAmount < 100)
		{
			currentAmount = _amount;
			TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
			TextResource.gameObject.SetActive(true);

		}
		LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}

  public float GetAmount(){
	return (LoadingBar.GetComponent<Image>().fillAmount);
  }
}
