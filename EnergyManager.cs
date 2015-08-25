using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class EnergyManager : MonoBehaviour {
	public GameObject energy_timer;
	public GameObject energy_left;
	private int energy_num = 20;
	private float cool_time;
	// Use this for initialization
	private int seconds;
	private int minutes;
	private int hours;
	DateTime currentDate;
	DateTime oldDate;

	void Start () 
	{
		if(PlayerPrefs.HasKey("Energy"))
		{
			//PlayerPrefs.SetInt ("Energy",20);

			energy_num = PlayerPrefs.GetInt ("Energy");
			//energy_num=20;
			energy_left.GetComponent<Text>().text = energy_num.ToString();
		}

		if (PlayerPrefs.HasKey ("Timer")) 
		{
			//PlayerPrefs.SetInt ("Timer",60);
			cool_time = PlayerPrefs.GetFloat ("Timer");
		}
		if (PlayerPrefs.HasKey ("sysString")) 
		{
			currentDate = System.DateTime.Now;
			long temp = System.Convert.ToInt64 (PlayerPrefs.GetString ("sysString"));
			DateTime oldDate = DateTime.FromBinary(temp);
			TimeSpan difference = currentDate.Subtract(oldDate);
			print("Difference: " + difference.TotalSeconds);
			int addedEnergy = (int) Mathf.FloorToInt((float)difference.TotalSeconds)/60;
			int addedtimer = (int) Mathf.RoundToInt((float)difference.TotalSeconds)%60;
			print ("addedEnergy:" + addedEnergy);
			if(addedEnergy!=0)
				IncreaseEnergyNum(addedEnergy);

			if(cool_time - addedtimer>0)
				cool_time -= addedtimer;
		}

		InvokeRepeating ("DecreaseTimeRemaining", 1.0f, 1.0f);
	}

	public void DecreaseEnergyNum()
	{
		if (energy_num != 0) {
			energy_num--;
			energy_left.GetComponent<Text>().text = energy_num.ToString();
			PlayerPrefs.SetInt ("Energy",energy_num);
		}
	}

	public void IncreaseEnergyNum()
	{
		if (energy_num != 20) {
			energy_num++;
			energy_left.GetComponent<Text> ().text = energy_num.ToString ();
			PlayerPrefs.SetInt ("Energy", energy_num);
		}
	}
	public void IncreaseEnergyNum(int n)
	{
		if (!(energy_num + n >= 20)) {
			energy_num += n;
			energy_left.GetComponent<Text> ().text = energy_num.ToString ();
			PlayerPrefs.SetInt ("Energy", energy_num);
		} else {
			energy_num = 20;
			energy_left.GetComponent<Text> ().text = energy_num.ToString ();
			PlayerPrefs.SetInt ("Energy", energy_num);
		}
	}
	
	public int GetEnergyNum()
	{
		return energy_num;
	}

	public float GetCoolTime()
	{
		return cool_time;
	}

	void DecreaseTimeRemaining()
	{
		if (energy_left.GetComponent<Text> ().text != "20") {
			cool_time--;
			if (cool_time > 9)
				energy_timer.GetComponent<Text> ().text = "0:" + cool_time.ToString ();
			else
				energy_timer.GetComponent<Text> ().text = "0:0" + cool_time.ToString ();
		} else {
			energy_timer.GetComponent<Text> ().text = "MAX";
		}

	}

	// Update is called once per frame
	void Update () {
		if (cool_time == 0) {
			cool_time = 60f;
			energy_timer.GetComponent<Text> ().text = "1:00";
			IncreaseEnergyNum();
		}
	}

	public void SaveEnergyStatus()
	{
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
		PlayerPrefs.SetFloat ("Timer",cool_time);
		PlayerPrefs.SetInt ("Energy",energy_num);
	}

	void OnApplicationQuit()
	{
		SaveEnergyStatus ();
	}
}
