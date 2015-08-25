using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationManager : MonoBehaviour {
	public Sprite speaker_on;
	public Sprite speaker_off;

	// Use this for initialization
	void Start () {
	
	}

	public void SoundOnOff()
	{
	
	}

	// Update is called once per frame
	public void GotoStageScene () {
		Application.LoadLevel ("1_Main_stage");
	}
}
