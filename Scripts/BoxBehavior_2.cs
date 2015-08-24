using UnityEngine;
using System.Collections;

public class BoxBehavior_2 : MonoBehaviour {
	public float boxSpeed = 5.0f;
	public Vector3 direction;
	bool left_turning_flag = false;
	bool right_turning_flag = false;
	bool startBase_flag = true;
	float threshold = 0.3f;
	public GameObject explodeParticles;
	void Start()
	{
		explodeParticles = Instantiate(Resources.Load ("Particles"),this.gameObject.transform.position,Quaternion.identity) as GameObject;
		explodeParticles.SetActive (false);
	}

	void Update()
	{
		//moving
		this.transform.position += direction * boxSpeed * Time.deltaTime;

	}

	void Optimize(Vector3 pos)
	{
		pos.x = Mathf.Round (this.transform.position.x);
		pos.y = Mathf.Round (this.transform.position.y);
		transform.position = pos;
	}

	IEnumerator OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "EndofScene") {
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			yield return StartCoroutine(Explode());
			Destroy(this.gameObject);
			GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;
		}
		//collide each other
		if(coll.gameObject.tag == "Player")
		{
			//explode.SetActive(true);
			//Vector3 coll_pos = coll.transform.position;
			//coll.gameObject.;
			coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			yield return StartCoroutine(Explode());
			//explodeParticles = Instantiate(Resources.Load ("Particles"),this.gameObject.transform.position,Quaternion.identity) as GameObject;
	
			Destroy(coll.gameObject);
			Destroy(this.gameObject);

			GameObject.Find ("_Manager").GetComponent<StageInfo>().retry_flag = true;

		}

	}

	IEnumerator Explode()
	{
		explodeParticles.SetActive (true);
		explodeParticles.transform.position = this.transform.position;
		explodeParticles.GetComponent<ParticleSystem>().Play();

		yield return new WaitForSeconds (1);
	}
	void OnTriggerStay2D(Collider2D coll)
	{
		//turn left
		if (coll.gameObject.tag == "Obstacle" && left_turning_flag) {
			Vector3 player_vector =coll.gameObject.transform.position - this.gameObject.transform.position;
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
			Vector3 player_vector =coll.gameObject.transform.position - this.gameObject.transform.position;
			if(Mathf.Abs(player_vector.x)<threshold && Mathf.Abs(player_vector.y)<threshold)
			{
				//Debug.Log ("turningPoint!");
				//coll.gameObject.transform.position.x = Mathf.Round(coll.gameObject.transform.position.x);
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
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Obstacle") {
			Vector3 player_vector =coll.gameObject.transform.position - this.gameObject.transform.position;
			player_vector = player_vector.normalized;
			Debug.Log (player_vector);

			if(player_vector == Vector3.right)
			{
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;

				if(obstacle_angle ==0||obstacle_angle ==270)
					direction = Vector3.left;
				else if(obstacle_angle==90)
				{
					left_turning_flag = true;
				}
				else if(obstacle_angle==180)
				{
					right_turning_flag = true;
				}

				
			}else if (player_vector == Vector3.left)
			{
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if(obstacle_angle ==90||obstacle_angle ==180)
					direction = Vector3.right;
				else if(obstacle_angle==270)
				{
					left_turning_flag = true;
				}
				else if(obstacle_angle==0)
				{
					right_turning_flag = true;
				}
			}else if (player_vector == Vector3.up)
			{
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if(obstacle_angle ==0||obstacle_angle ==90)
					direction = Vector3.down;
				else if(obstacle_angle==180)
				{
					left_turning_flag = true;
				}
				else if(obstacle_angle==270)
				{
					right_turning_flag = true;
				}
			}
			else{//down
				int obstacle_angle = (int)coll.gameObject.transform.rotation.eulerAngles.z;
				
				if(obstacle_angle ==180||obstacle_angle ==270)
					direction = Vector3.up;
				else if(obstacle_angle==0)
				{
					left_turning_flag = true;
				}
				else if(obstacle_angle==90)
				{
					right_turning_flag = true;
				}
			}

		}
	}

}
