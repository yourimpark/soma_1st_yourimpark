using UnityEngine;
using System.Collections;

public class SettingButtonScrupt : MonoBehaviour {
	public GameObject settings_panel;

	public void OpenPanel()
	{
		settings_panel.SetActive (true);
		Time.timeScale = 0;
	}

	public void ClosePanel()
	{
		settings_panel.SetActive (false);
		Time.timeScale = 1;
	}

	public void retry_event()
	{
		//ClosePanel ();
		Application.LoadLevel (Application.loadedLevel);

	}
}
