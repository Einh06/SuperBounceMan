using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Gravity : MonoBehaviour {

	//[HideInInspector]
	public float gravityRange = 0;

	public const float gravitationalConstant = 6.6720e-08f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		pullAllInRange ();
	}

	Collider2D[] ObjectsInRange {
		get { 
			Vector2 center = transform.position;
			return Physics2D.OverlapCircleAll(center,gravityRange); 
		}
	}

	//this is supposed to be called every frame
	void pull (Rigidbody2D objectToAttract) {
		float distance = Vector2.Distance (transform.position, objectToAttract.transform.position);

		Vector2 direction = (transform.position - objectToAttract.transform.position).normalized;

		Vector2 force =  ((objectToAttract.mass * rigidbody2D.mass) / Mathf.Pow(distance,2)) * direction;

		/*
		Debug.Log("masses: " + rigidbody2D.mass + " " + objectToAttract.mass);
		Debug.Log("DistanceModifier " + Mathf.Pow(distance,2));
		Debug.Log("distance: " + distance);
		Debug.Log("direction: " + direction);
		Debug.Log("force: " + force);
		*/

		objectToAttract.AddForce (force);
	}

	void pullAllInRange () {
		foreach (Collider2D other in ObjectsInRange) {

			var otherRigidbody = other.GetComponent<Rigidbody2D>();
			if (otherRigidbody != null && other.gameObject != gameObject) {
				pull (otherRigidbody);
			}
		}
	}
}
