using UnityEngine;
using System.Collections;

public class SuperWinner : MonoBehaviour {

	public AudioClip winAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			audio.volume = 1;
			audio.PlayOneShot (winAudio);
			audio.Play();
			Destroy (col.gameObject);
		}
	}
}
