using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour {
	public GameObject mode1;
	public GameObject mode2;
	public bool start_flag= false;
	public GameObject obs_behind;
	public LayerMask mask = 5;
	


	// Use this for initialization
	void Start () {
		mode1 = GameObject.Find ("mode1");
		mode2 = GameObject.Find ("mode2");

		foreach (Transform child in mode1.transform) {
			GameObject obj= (GameObject) Instantiate(obs_behind,child.position,child.localRotation);
			obj.transform.SetParent(mode2.transform,true);
		}
		foreach (Transform child in mode2.transform) {
			GameObject obj= (GameObject) Instantiate(obs_behind,child.position,child.localRotation);
			obj.transform.SetParent(mode1.transform,true);
		}
		mode1.gameObject.SetActive (true);
		mode2.gameObject.SetActive (false);

		//test = Camera.main.GetComponent<GUILayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(start_flag)
		{
			if (Input.GetMouseButtonDown(0))
			{
				EventSystem eventSystem = EventSystem.current;
				if (!eventSystem.IsPointerOverGameObject())
				{
					if(mode1.gameObject.activeSelf == true)
					{
						mode1.gameObject.SetActive (false);
						mode2.gameObject.SetActive (true);
					}else{
						mode1.gameObject.SetActive (true);
						mode2.gameObject.SetActive (false);
					}
					
				}
			}
		}

	}
}
