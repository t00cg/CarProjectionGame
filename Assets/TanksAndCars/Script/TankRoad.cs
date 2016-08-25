using UnityEngine;
using System.Collections;

public class TankRoad : MonoBehaviour {

	float speed = 0.001f;
	float relativez = 0.0f;

	public TanksAndCars gameLogic;


	// Use this for initialization
	void Start () {
		speed = 0.002f;
		relativez = -8.7f;
		transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameLogic.levelz+relativez);
			
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// gameLogic. levelz
		relativez = relativez + speed;
		// gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameLogic.levelz+relativez);
		gameObject.transform.Translate(0.0f,0.0f,speed);
	}
}
