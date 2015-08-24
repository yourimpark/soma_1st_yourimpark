using UnityEngine;
using System.Collections;

public class BoxGenerator : MonoBehaviour {
	public GameObject box;
	// Use this for initialization
	void Start () {
		GameObject a = (GameObject)Instantiate (box, new Vector2 (-3, -3), Quaternion.identity);
		GameObject b = (GameObject)Instantiate (box, new Vector2 (-3, 3), Quaternion.identity);
		GameObject c = (GameObject)Instantiate (box, new Vector2 (3, -3), Quaternion.identity);
		GameObject d = (GameObject)Instantiate (box, new Vector2 (3, 3), Quaternion.identity);

		a.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
		b.GetComponent<SpriteRenderer>().color = new Color(0,255,0);
		c.GetComponent<SpriteRenderer>().color = new Color(0,0,255);
		d.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
