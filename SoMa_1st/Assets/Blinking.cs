using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {
	Image image;
	Color32 image_color;
	Color32 image_alpha;
	// Use this for initialization
	void Start () {
		image = this.GetComponent<Image> ();
		image_color = image.color;
		image_alpha = image_color;
		image_alpha.a = 100;
		InvokeRepeating ("Blink", 0.5f, 0.5f);
	}
	void Blink()
	{
		if (image.color == image_color)
			image.color = image_alpha;
		else
			image.color = image_color;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
