using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

	public GameObject current_money_GUI;
	private int current_money;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("Money")) {
			//PlayerPrefs.SetInt ("Money",0);
			current_money = PlayerPrefs.GetInt ("Money");
			shortenedForGUI();
			//current_money_GUI.GetComponent<Text> ().text = current_money.ToString ();
		}
	}

	public void IncreaseCurrentMoney(int n)
	{
		current_money += n;
		print (current_money);
		shortenedForGUI ();
	}

	public void saveMoney()
	{
		PlayerPrefs.SetInt ("Money",current_money);
	}
	void shortenedForGUI()
	{
		if (current_money < 1000) {
			current_money_GUI.GetComponent<Text> ().text = current_money.ToString ();
		} else {
			float n = (float)current_money / 1000;
			current_money_GUI.GetComponent<Text> ().text = n.ToString ()+" K";
		}
	}

	void OnApplicationQuit()
	{
		saveMoney ();
	}
}
