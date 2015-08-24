using UnityEngine;
using System.Collections;

public class ObstacleBehavior : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			this.GetComponent<ParticleSystem>().Play ();
		}
	}
}
