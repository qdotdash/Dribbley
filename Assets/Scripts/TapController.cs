using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour {

	 public delegate void PlayerDelegate();
	 public static event PlayerDelegate OnPlayerDied;
	 public static event PlayerDelegate OnPlayerScored;

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector3 startPos = new Vector3(0f, 0f, 0f);			/////Remember this
	public AudioSource tapSound;
	public AudioSource scoreSound;
	public AudioSource dieSound;
	public Vector3 temp;

	Rigidbody2D rigidBody;
	Quaternion downRotation;
	Quaternion forwardRotation;

	GameManager game;
	//TrailRenderer trail;

	void Start() {

		rigidBody = GetComponent<Rigidbody2D>();
		downRotation = Quaternion.Euler(0, 0 ,-100);
		forwardRotation = Quaternion.Euler(0, 0, 40);
		game = GameManager.Instance;
		rigidBody.simulated = false;
		transform.localPosition = startPos;                           /////Remember this
		//trail = GetComponent<TrailRenderer>();
		//trail.sortingOrder = 20; 
	}

	 void OnEnable() {
	 	GameManager.OnGameStarted += OnGameStarted;
	 	GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	 }

	 void OnDisable() {
	 	GameManager.OnGameStarted -= OnGameStarted;
	 	GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	 }

	void OnGameStarted() {
		rigidBody.velocity = Vector3.zero;
		rigidBody.simulated = true;
	}

	 void OnGameOverConfirmed() {
	 	transform.localPosition = startPos;
	 	transform.rotation = Quaternion.identity;
	 }

	void Update() {
		if (game.GameOver) return;

		if (Input.GetMouseButtonDown(0)) {
			//Time.timeScale += 1; 				
			// temp = transform.localScale;
			// temp.x += Time.deltaTime;
			// temp.y += Time.deltaTime;
			// transform.localScale = temp;			
			rigidBody.velocity = Vector2.zero;
			transform.rotation = forwardRotation;
			rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
			tapSound.Play();
		}

		if(rigidBody.simulated == true)						/////Remember this
			transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "scorezone") {
			OnPlayerScored();
			scoreSound.Play();
		}
		if (col.gameObject.tag == "deadzone") {
			rigidBody.simulated = false;
			OnPlayerDied();
			dieSound.Play();
		}
	}

}
