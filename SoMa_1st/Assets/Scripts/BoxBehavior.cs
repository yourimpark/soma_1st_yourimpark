using UnityEngine;
using System.Collections;

public class BoxBehavior : MonoBehaviour {
	public float boxSpeed = 5.0f;
	public Vector3 direction = new Vector3(0,1,0);
	public bool trigger_flag = false;
	public bool rot_flag = false;
	Quaternion target = Quaternion.Euler(0, 0, 0);
	float rotationTime = 1.0f;

	// Update is called once per frame
	void Update()
	{
			this.transform.position += direction * boxSpeed * Time.deltaTime;
			
		if (rot_flag) {
			//transform.RotateAround(transform.Find("pivot").transform.position,Vector3.up,Time.deltaTime*10.0f);
			//transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 25.0f);
		}

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "ReverseBounce") {
			//Debug.Log ("Trigger");
			trigger_flag =true;
		}
		if (coll.gameObject.tag == "RotationPoint"&& !rot_flag) {
			Debug.Log ("asd");
			Vector3 pivot = this.gameObject.transform.position;
			Transform[] transform_c = GetComponentsInChildren<Transform>();
			foreach(Transform child in transform_c)
			{
				pivot += child.position;
			}
			pivot /= (1+transform_c.Length);

			GameObject obj = new GameObject();
			obj.transform.position = pivot;
			obj.transform.SetParent(this.gameObject.transform,true);
			obj.name = "pivot";

			rot_flag = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "RotationPoint") {
			//Debug.Log ("escape");
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log ("asd");

		if (coll.gameObject.tag == "EndofScene") {
			GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;
		}
		if (!trigger_flag) {
			if (coll.gameObject.tag == "Obstacle1") {
				//Debug.Log ("qwe");
				if (direction == Vector3.left) {
					direction = Vector3.up;
				} else if (direction == Vector3.up) {
					direction = Vector3.right;
				} else if (direction == Vector3.right) {
					direction = Vector3.down;
				} else if (direction == Vector3.down) {
					direction = Vector3.left;
				}

			} else if (coll.gameObject.tag == "Obstacle2") {
				//Debug.Log ("asd");
				if (direction == Vector3.right) {
					direction = Vector3.up;
				} else if (direction == Vector3.up) {
					direction = Vector3.left;
				} else if (direction == Vector3.left) {
					direction = Vector3.down;
				} else if (direction == Vector3.down) {
					direction = Vector3.right;
				}
			}
		}
		else{ //bounce 
			if (direction == Vector3.left) {
				direction = Vector3.right;
			} else if (direction == Vector3.up) {
				direction = Vector3.down;
			} else if (direction == Vector3.right) {
				direction = Vector3.left;
			} else if (direction == Vector3.down) {
				direction = Vector3.up;
			}
		}
		if(coll.gameObject.tag == "Object_player")
		{
			//Application.LoadLevel("Prototype_2");

			if(coll.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color)
			{
				/*
				float dx = Mathf.Round (this.transform.position.x - coll.gameObject.transform.position.x);
				float dy = Mathf.Round(this.transform.position.y - coll.gameObject.transform.position.y);
				//Debug.Log (dx);
				Destroy (coll.gameObject);
				if(dx == -1||dx == 1)
				{
					Vector2 temp_scale = this.transform.localScale;
					temp_scale.x= this.transform.localScale.x *2;
					this.transform.localScale = temp_scale;
				}
				if(dy == -1||dy == 1)
				{
					Vector2 temp_scale = this.transform.localScale;
					temp_scale.y= this.transform.localScale.y *2;
					this.transform.localScale = temp_scale;
				}*/


				coll.gameObject.transform.SetParent(this.gameObject.transform,true);


				Vector2 temp = coll.gameObject.transform.localPosition;
				temp.x= Mathf.Round(coll.gameObject.transform.localPosition.x);
				temp.y= Mathf.Round(coll.gameObject.transform.localPosition.y);
				coll.gameObject.transform.localPosition = temp;


			}

		}

		if(coll.gameObject.tag == "Player")
		{
			GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;
		}

		/*
		//optimize
		Vector3 pos = transform.position;
		pos.x = Mathf.Round (this.transform.position.x);
		pos.y = Mathf.Round (this.transform.position.y);
		transform.position = pos;
		*/
		Optimize (transform.position);

		trigger_flag = false;

	}

	void Optimize(Vector3 pos)
	{
		pos.x = Mathf.Round (this.transform.position.x);
		pos.y = Mathf.Round (this.transform.position.y);
		transform.position = pos;
	}
}
