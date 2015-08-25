using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_CompleteScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void GoNextLevel () {
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void GoMainMenu(){
		Application.LoadLevel ("Main_stage");
	}
}
