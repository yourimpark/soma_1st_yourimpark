using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public int stage_index;
	void Start()
	{
		this.GetComponent<Button> ().onClick.AddListener (()=>GotoSpecificStage(stage_index));
	}

	public void GotoSpecificStage(int index)
	{
		Application.LoadLevel(index);
	}
}
