using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageManager : MonoBehaviour {
	public GameObject[] stages;
	public int stage_num = 3;
	private int current_showing_stage=1;
	public Text text_clearStage;
	public GameObject noticeBox_10th;
	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.HasKey ("Cleared_Stage")) {
			bool[] cleared_stages = PlayerPrefsX.GetBoolArray ("Cleared_Stage");
			//bool[] stage_mode = PlayerPrefsX.GetBoolArray ("Cleared_Stage_Mode");
			int cleared_stage_num=0;

			foreach(bool item in cleared_stages)
			{
				if(item == true)
				{
					cleared_stage_num++;
				}
			}
			if(cleared_stage_num==10)
			{
				noticeBox_10th.SetActive(true);
			}
			int i =Mathf.FloorToInt(cleared_stage_num/10)+1;
			Show_This_Stage(i);
			Show_ClearedText(i);
		}
	}

	void Show_ClearedText(int index)
	{
		if (PlayerPrefs.HasKey ("Cleared_Stage_Mode")) {
			bool[] stage_mode = PlayerPrefsX.GetBoolArray ("Cleared_Stage_Mode");
			int indexForStage = (index-1)*30;
			int showing_num=0;
			for(int i=indexForStage;i<indexForStage+10*3;i++)
			{
				if(stage_mode[i] == true)
					showing_num++;
			}
			text_clearStage.text = showing_num.ToString();
		}
	}
	public void Go_Next()
	{
		if (current_showing_stage != stage_num) {
			current_showing_stage ++;
			for (int i=0; i<stage_num; i++) 
			{
				stages [i].SetActive (false);
			}
			stages [current_showing_stage-1].SetActive (true);
			Show_ClearedText(current_showing_stage);
		}
	}

	public void Go_Previous()
	{
		if (current_showing_stage != 1) {
			current_showing_stage--;
			for (int i=0; i<stage_num; i++) 
			{
				stages [i].SetActive (false);
			}
			stages [current_showing_stage-1].SetActive (true);
			Show_ClearedText(current_showing_stage);
		}
	}
	void Show_This_Stage(int index)
	{
		current_showing_stage = index;
		for (int i=0; i<stage_num; i++) 
		{
			stages [i].SetActive (false);
		}
		stages [current_showing_stage-1].SetActive (true);
	}
	
	public void loadGame(int level)
	{
		Application.LoadLevel ((current_showing_stage-1) * 10 + level);
	}
	public void GotoOpeningScene()
	{
		Application.LoadLevel(0);
	}

	public int GetCurrentStage()
	{
		return current_showing_stage;
	}
}
