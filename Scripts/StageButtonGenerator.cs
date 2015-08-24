using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StageButtonGenerator : MonoBehaviour {
	public GameObject button_prefab;
	public int button_num;
	// Use this for initialization
	void Start () {
		//Instantiate (button_prefab,
		GameObject temp;
		for (int i=0; i<button_num; i++) {
			temp=(GameObject)Instantiate(button_prefab,new Vector3 (100*(i%3)-100, 150-Mathf.CeilToInt(i/3)*120, 0),Quaternion.identity);
			temp.transform.SetParent (this.transform, false);
			temp.GetComponentInChildren<Text>().text = (i+1).ToString();
			temp.GetComponent<ButtonManager>().stage_index = i+1;
		}
	}
}
