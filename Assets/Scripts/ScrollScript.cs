using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3(0.05f,0,0);
		if (transform.position.x <= -20){
			transform.position = new Vector3(20f,transform.position.y,0);
		}
	}
}
