using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private Transform currentPlanetTransform = null;
	public float jumpForce = 100;
	public float jumpClickRange = 10;
	public float jumpRange = 20;
	public float upFactor = 10;
	public float rightFactor = 10;
	public float jetpackDistance = 10;

	public AudioClip dieAudio;
	public AudioClip landAudio;
	public AudioClip jumpSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Collider2D[] nearbyPlanets = planetsInJetpackRange();
		
		foreach (Collider2D col in nearbyPlanets) {
			
			repulseFromPlanet (col.transform);
			currentPlanetTransform = col.gameObject.transform;
			
		}
		
		

		if (Input.GetMouseButtonDown (0)) {
			
			Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (InJumpRange && clickInRange(worldMousePosition)) {
				jump ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag == "Station")
			win ();

		else if (col.gameObject.tag == "Asteroid")
			die ();
	}
	

	void die () {
		audio.PlayOneShot(dieAudio);
		audio.Play();
		//Destroy(gameObject);
	}

	void win () {
	}

	void jump () {
		//rigidbody2D.velocity = Vector2.zero;
		//repulseFromPlanet (currentPlanetTransform, -repulsionFactor);

		Vector2 direction = (transform.position - currentPlanetTransform.position).normalized;
		gameObject.rigidbody2D.AddForce (direction * jumpForce);
		
		audio.PlayOneShot(jumpSound);
		audio.Play();
		
	}
	
	bool clickInRange (Vector2 position) {
		return  Vector2.Distance (position, transform.position) < jumpClickRange;
	}

	void repulseFromPlanet (Transform repulseFrom) {
		Vector2 upDirection = (transform.position - repulseFrom.transform.position).normalized;
		Vector2 rightDirection = Vector3.Cross(upDirection, Vector3.back);
		gameObject.rigidbody2D.AddForce ((rightDirection * rightFactor) + (upDirection * upFactor) );
		/*
		Debug.Log ("REPULSE");
		Debug.Log ("upDirection: " + upDirection);
		Debug.Log ("rightDirection: " + rightDirection);
		*/
	}

	Collider2D[] planetsInJetpackRange () {

		Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position,jetpackDistance);
		List<Collider2D> filteredArray = new List<Collider2D>();
		foreach (Collider2D col in objectsInRange) {
			if (col.gameObject.tag == "Planet")
				filteredArray.Add (col);
		}

		return filteredArray.ToArray();
	}

	bool InJumpRange {
		get {
			Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position,jumpRange);
			
			foreach (Collider2D col in nearbyObjects) {
				if (col.gameObject.tag == "Planet") {

					//BEWARE HACK
					currentPlanetTransform = col.gameObject.transform;


					return true;
				}
			}

			return false;
		}
	}
}
