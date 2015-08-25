using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInUI : MonoBehaviour {
	public CanvasGroup myCanvasGroup;
	private bool fadeIn;
	private bool fadeIn_complete = false;

	void Start()
	{
		Fading ();
	}

	public void Fading()
	{
		fadeIn =true;
		myCanvasGroup.alpha = 0f;

	}
	void Update()
	{
		if (fadeIn) {
			myCanvasGroup.alpha = myCanvasGroup.alpha + Time.deltaTime;
			if (myCanvasGroup.alpha >= 1) {
				myCanvasGroup.alpha = 1;
				fadeIn = false;
				fadeIn_complete= true;		

			}
		} 
		if (fadeIn_complete == true) {
			
		}
	}

}
