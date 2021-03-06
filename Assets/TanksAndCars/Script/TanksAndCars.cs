﻿using UnityEngine;
using System.Collections;


public class TanksAndCars: MonoBehaviour {

	public GameObject backgroundPrefab; 
	public GameObject backgroundPrefabLeft; 
	public GameObject backgroundPrefabRight; 
	int[] arrLevel = {0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,3};

	public GameObject level;
	public GameObject ground;

	float offset = 0.1f; // 0.05f;
	float[] arrLane = { 0.0f, 0.08f };

	// Use this for initialization
	void Start () {

		// create a level!
		// random
		float posz = 0.0f;
		for (int z=0;z<arrLevel.Length;z++) {
			int el = arrLevel[z];
			GameObject obj = null;
			if (el==0) {
				obj = (GameObject) Instantiate( backgroundPrefab, new Vector3(0.0f,0.0f,posz), new Quaternion());
			}
			if (el==1) {
				obj = (GameObject) Instantiate( backgroundPrefabRight , new Vector3(0.0f,0.0f,posz), new Quaternion());
			}
			if (el==2) {
				obj = (GameObject) Instantiate( backgroundPrefabLeft, new Vector3(0.0f,0.0f,posz), new Quaternion());
			}
			if (obj!=null) {
				obj.transform.parent = ground.transform;
			}
			posz = posz +0.2f;
		}

		time = Time.time;

		speedtoGo = speed;
	}
	
	// Update is called once per frame
	float speedo = 0.0f; // actual speed
	float speedSpeed = 0.001f;

	float speed = -0.003f;
	float speedExtended = -0.012f;
	int actualLaneLevel = 0;
	int actualLane = 0;
	float changeLaneSpeed = 0.01f;
	float actSpeedX = 0.0f;

	public float levelz = 0.0f;
	float depth = 0.2f;
	int actualTile = 0;

	int actJob = 0;

	float time = 0.0f;

	float speedtoGo = 0.0f;

	void FixedUpdate () {

		actJob = arrLevel[-actualTile];
		if (actJob==3) {
			// end
			levelz = 0.0f;
			time = Time.time;
		}



		// on change!
		/*
		if (actJob==0) {
			speedtoGo = speedExtended;
		}
		if (actJob==1) {
			if (actualLane==0) {
				speedtoGo = speed;
			} else {
				speedtoGo = speedExtended;
			}
		}
		if (actJob==2) {
			if (actualLane==1) {
				speedtoGo = speed;
			} else {
				// levelz = levelz - speedExtended;
				speedtoGo = speedExtended;
			}
		}
		*/

		// update speed
		if (speedo>speedtoGo) {
			speedo = speedo - 3*speedSpeed;
		}
		if (speedo<speedtoGo) {
			speedo = speedo + speedSpeed;
		}

		levelz = levelz + speedo;


		float actpos = (float) arrLane[actualLane];
		if (actpos<actSpeedX) actSpeedX = actSpeedX - changeLaneSpeed;
		if (actpos>actSpeedX) actSpeedX = actSpeedX + changeLaneSpeed;
		ground.transform.position = new Vector3(0.0f+offset+actSpeedX,0.0f,levelz);
		int iactualTile = (int)((levelz-0.4f)/depth);
		// next level
		/*
		if (((-actualTile)+1)>arrLevel.Length) {
			levelz = 0.0f;
			time = Time.time;
		}
		*/
		//if (actualTile!=iactualTile) {
		// Debug.Log("new actualLane "+actualTile+" "+actJob+"-["+speedtoGo+"/"+speedo+"]-"+levelz);
		//}
		actualTile = iactualTile;
	}

	void Update() {
		if (Input.GetKeyDown("left")) {
			// actualLane = 0	;	
			speedtoGo = speedExtended;
		}
		if (Input.GetKeyDown("right")) {
			// actualLane = 1	;
			speedtoGo = speed;

		}

	}

	void OnGUI() {
		GUI.Label(new Rect(0,0,200,20),"TIME: "+(Time.time-time));
	}
}
