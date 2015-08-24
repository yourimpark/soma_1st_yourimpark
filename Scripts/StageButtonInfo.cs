using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StageButtonInfo : MonoBehaviour {

	private Image lock_mode;
	private Image flag_mode;
	private bool can_beAccessed = false;
	private bool easy_isClear = false;
	private bool normal_isClear = false;
	private bool hard_isClear = false;
	private int stage_num;

	// Use this for initialization
	void Start () {

		stage_num = int.Parse(this.transform.Find ("Stage_Number").GetComponent<Text> ().text);
		stage_num += (GameObject.Find ("_Manager").GetComponent<StageManager> ().GetCurrentStage () - 1) * 10;

		lock_mode = this.transform.Find ("Lock").GetComponent<Image> ();
		flag_mode = this.transform.Find ("Flag").GetComponent<Image> ();
		flag_mode.enabled = false;

		SetAccessibility ();

		if (can_beAccessed) {
			lock_mode.enabled = false;

			GetComponent<Button>().interactable = true;

			if (easy_isClear)
				this.transform.Find ("Easy").GetComponent<Image> ().color = new Color32 (103, 217, 255, 200);
			
			if (normal_isClear)
				this.transform.Find ("Normal").GetComponent<Image> ().color = new Color32 (255, 232, 103, 200);
			
			if (hard_isClear)
				this.transform.Find ("Hard").GetComponent<Image> ().color = new Color32 (255, 103, 103, 200);

			if(easy_isClear&&normal_isClear&&hard_isClear)
				flag_mode.enabled = true;

		} else {
			lock_mode.enabled = true;
			GetComponent<Button>().interactable = false;
		}

	}

	public void Set_Mode_Status(bool easy, bool normal, bool hard)
	{
		easy_isClear = easy;
		normal_isClear = normal;
		hard_isClear = hard;
	}

	public void SetAccessibility()
	{
		bool[] cleared_stages = PlayerPrefsX.GetBoolArray ("Cleared_Stage");
		bool[] stage_mode = PlayerPrefsX.GetBoolArray ("Cleared_Stage_Mode");

		if (stage_num == 1) {
			can_beAccessed = true;
			int n =(stage_num-1)*3;
			Set_Mode_Status(stage_mode[n],stage_mode[n+1],stage_mode[n+2]);
		}else if(cleared_stages[stage_num-2]==true) {
			can_beAccessed = true;
			int n =(stage_num-1)*3;
			Set_Mode_Status(stage_mode[n],stage_mode[n+1],stage_mode[n+2]);

		} else 
		{
			can_beAccessed = false;
		}

	}

	public int GetStageNum()
	{
		return stage_num;
	}
}
