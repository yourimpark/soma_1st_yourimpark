using UnityEngine;
using System.Collections;

public class SaveStageClearStatus : MonoBehaviour {
	private int total_stage_num = 50;
	private int cleared_stage_num = 0;

	public bool[] is_cleared;
	public bool[] clear_mode;


	// Use this for initialization
	void Awake () {
		//PlayerPrefs.DeleteAll ();

		is_cleared = new bool[total_stage_num];
		clear_mode = new bool[total_stage_num * 3];
		if (!PlayerPrefs.HasKey ("Cleared_Stage")) 
		{
			for(int i=0;i<is_cleared.Length;i++) {
				is_cleared[i] = false;
			}
			for(int i=0;i<clear_mode.Length;i++) {
				clear_mode[i] = false;
			}

			PlayerPrefsX.SetBoolArray("Cleared_Stage_Mode",clear_mode);
			PlayerPrefsX.SetBoolArray("Cleared_Stage",is_cleared);
		}
	}

}
