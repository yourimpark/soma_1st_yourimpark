using UnityEngine;
using System.Collections;

public class BoxBehavior_3 : MonoBehaviour {
	public float boxSpeed = 5.0f;
	public Vector3 direction;
	bool left_turning_flag = false;
	bool right_turning_flag = false;
	bool startBase_flag = true;
	bool attach_flag= false;
	bool trig_flag = false;
	float threshold = 0.3f;

	//float offset_x=0;
	//float offset_y=0;

	void Update()
	{
		this.transform.position += direction * boxSpeed * Time.deltaTime;
	}

	//정수 단위로 움직이게 위치를 조정시키는 역할
	void Optimize(Vector3 pos)
	{
		pos.x = Mathf.Round (this.transform.position.x);
		pos.y = Mathf.Round (this.transform.position.y);
		transform.position = pos;
	}
	//정수 단위로 움직이게 위치를 조정시키는 역할
	void Optimize_local(Vector3 pos)
	{
		Vector2 temp = pos;
		temp.x= Mathf.Round(pos.x);
		temp.y= Mathf.Round(pos.y);
		pos = temp;
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "EndofScene") {
			GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;
		}
		
		if(coll.gameObject.tag == "Player" && direction !=Vector3.zero )
		{
			//attach each other
			if(coll.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color)
			{
				//this.transform.SetParent(coll.gameObject.transform,true);
				coll.gameObject.transform.SetParent(this.gameObject.transform,true);
				Destroy(coll.gameObject.GetComponent<Rigidbody2D>());


				Vector2 temp = coll.gameObject.transform.localPosition;
				temp.x= Mathf.Round(coll.gameObject.transform.localPosition.x);
				temp.y= Mathf.Round(coll.gameObject.transform.localPosition.y);
				coll.gameObject.transform.localPosition = temp;


			}else{//collide and gameover
				Destroy (this.gameObject);
				GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Obstacle" && !trig_flag) {
			Vector3 min_from_obstacle = this.gameObject.transform.position;
			float min_dist = Vector3.Distance(coll.transform.position,min_from_obstacle);
			
			if(transform.childCount > 0)
			{
				Transform[] child_pos = GetComponentsInChildren<Transform>();

				foreach(Transform child in child_pos)
				{
					float dist = Vector3.Distance(coll.transform.position,child.transform.position);
					if(dist<min_dist)
					{
						min_dist = dist;
						min_from_obstacle = child.transform.position;
					}
				}
			}
			else
			{
				min_from_obstacle = this.gameObject.transform.position;
			}

			//Debug.Log (min_from_obstacle);

			Vector3 player_vector = coll.gameObject.transform.position - min_from_obstacle;
			Vector3 temp = player_vector;
			temp.x = Mathf.Round (player_vector.x);
			temp.y = Mathf.Round (player_vector.y);
			player_vector = temp;
			player_vector = player_vector.normalized;
			

			if (player_vector == Vector3.right) {
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				Debug.Log ("right");
				if (obstacle_angle == 0 || obstacle_angle == 270)
					direction = Vector3.left;
				else if (obstacle_angle == 90) {
					left_turning_flag = true;
				} else if (obstacle_angle == 180) {
					right_turning_flag = true;
				}
			} else if (player_vector == Vector3.left) {
				Debug.Log ("left");
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if (obstacle_angle == 90 || obstacle_angle == 180)
					direction = Vector3.right;
				else if (obstacle_angle == 270) {
					left_turning_flag = true;
				} else if (obstacle_angle == 0) {
					right_turning_flag = true;
				}
			} else if (player_vector == Vector3.up) {
				Debug.Log ("Up");
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if (obstacle_angle == 0 || obstacle_angle == 90)
					direction = Vector3.down;
				else if (obstacle_angle == 180) {
					left_turning_flag = true;
				} else if (obstacle_angle == 270) {
					right_turning_flag = true;
				}
			} else if (player_vector == Vector3.down) {//down
				Debug.Log ("Down");
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if (obstacle_angle == 180 || obstacle_angle == 270)
					direction = Vector3.up;
				else if (obstacle_angle == 0) {
					left_turning_flag = true;
				} else if (obstacle_angle == 90) {
					right_turning_flag = true;
				}
			}
		}
		trig_flag = true;

	}

	void OnTriggerStay2D(Collider2D coll){

		Vector3 min_from_obstacle = this.gameObject.transform.position;
		float min_dist = Vector3.Distance(coll.transform.position, min_from_obstacle);
		
		if(transform.childCount > 0)
		{
			Transform[] child_pos = GetComponentsInChildren<Transform>();
			
			foreach(Transform child in child_pos)
			{
				float dist = Vector3.Distance(coll.transform.position,child.transform.position);
				if(dist < min_dist)
				{
					min_dist = dist;
					min_from_obstacle = child.transform.position;
				}
			}
		}
		else
		{
			min_from_obstacle = this.gameObject.transform.position;
		}

		//turn left
		if (coll.gameObject.tag == "Obstacle" && left_turning_flag) {
			Vector3 player_vector =coll.gameObject.transform.position - min_from_obstacle;
			if(Mathf.Abs(player_vector.x)<threshold&&Mathf.Abs(player_vector.y)<threshold)
			{
				Optimize(coll.gameObject.transform.position);
				if (direction == Vector3.right) {
					direction = Vector3.up;
				} else if (direction == Vector3.up) {
					direction = Vector3.left;
				} else if (direction == Vector3.left) {
					direction = Vector3.down;
				} else if (direction == Vector3.down) {
					direction = Vector3.right;
				}

				left_turning_flag = false;
			}
		}else if (coll.gameObject.tag == "Obstacle" && right_turning_flag) {
			//Debug.Log ("turn right");
			Vector3 player_vector =coll.gameObject.transform.position - min_from_obstacle;
			if(Mathf.Abs(player_vector.x)<threshold && Mathf.Abs(player_vector.y)<threshold)
			{
				
				Optimize(coll.gameObject.transform.position);

				if (direction == Vector3.left) {
					direction = Vector3.up;
				} else if (direction == Vector3.up) {
					direction = Vector3.right;
				} else if (direction == Vector3.right) {
					direction = Vector3.down;
				} else if (direction == Vector3.down) {
					direction = Vector3.left;
				}
				right_turning_flag = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		left_turning_flag = false;
		right_turning_flag = false;
		trig_flag = false;
	}

}
