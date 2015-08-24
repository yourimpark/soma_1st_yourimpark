using UnityEngine;
using System.Collections;

public class GoalCheckerScript : MonoBehaviour {
	StageInfo stageInfo;
	public bool completeMotion_flag = false;
	Vector3 startScale = new Vector3(0.8f,0.8f,0.8f);
	Vector3 endScale = new Vector3(1.2f,1.2f,1.2f);
	float startTime;
	// Use this for initialization
	void Start () {
		stageInfo = GameObject.Find ("_Manager").GetComponent<StageInfo> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (completeMotion_flag) {
			//float distCovered = (Time.time - startTime) / 1.0f;
			transform.parent.localScale = Vector3.Slerp(startScale,endScale,Mathf.PingPong(Time.time*10,1));

			StartCoroutine(motion());
			completeMotion_flag = false;

		}

	}

	IEnumerator motion()
	{

		yield return new WaitForSeconds (1f);
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.transform.parent.tag == "Player"|| coll.gameObject.tag== "Player") {
			if(coll.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color){
				completeMotion_flag = true;
				stageInfo.getGoal++;
				Destroy (coll.gameObject);
				if(stageInfo.getGoal == stageInfo.numofGoal)
				{
					stageInfo.complete_flag = true;
				}

			}
		}
	}

}
