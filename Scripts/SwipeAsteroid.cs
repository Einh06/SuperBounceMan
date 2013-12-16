using UnityEngine;
using System.Collections;

public class SwipeAsteroid : MonoBehaviour {

	public GameObject asteroidPrefab;
	public float strengthRatio = 10f;
	public float touchableRadius = 50f;
	private Vector2 creationPosition;
	private bool playerIsShooting = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0) && !playerIsShooting) {

			Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (isPositionInRange(worldMousePosition)) {

				creationPosition = worldMousePosition;
				playerIsShooting = true;
			}
		} else if (Input.GetMouseButtonUp (0) && playerIsShooting) {
			
			GameObject newObject = createObjectAtPos(creationPosition);

			Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 direction = throwDirection (worldMousePosition);
			float strength = throwStrength (worldMousePosition);
			
			newObject.rigidbody2D.AddForce (direction * strength);
			playerIsShooting = false;
		}
	}

	GameObject createObjectAtPos (Vector2 position)
	{
		GameObject newObject = (GameObject)Instantiate (asteroidPrefab);
		newObject.transform.position = position;
		return newObject;
	}

	Vector2 throwDirection (Vector2 releasePosition) {

		if (playerIsShooting) {
			return (releasePosition - creationPosition).normalized;
		} else {
			return Vector2.zero;
		}
	}

	float throwStrength (Vector2 releasePosition) {
		
		return Vector2.Distance (creationPosition, releasePosition) * strengthRatio;
	}

	bool isPositionInRange (Vector2 position) {
		
		return Vector2.Distance (position, transform.position) < touchableRadius;
	}
}