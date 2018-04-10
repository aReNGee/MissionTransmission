using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

	public Upgrades.UpgradeType thisis;

	public GameObject player;

	public int increaseD;

	public bool flatIncrease; //if not flat, multiplicitave, passed as an integer percentage

	public int startingCost;

	public float multi;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		//Debug.Log(this.gameObject.name);
		//Debug.Log("startingcost: " + startingCost);
		
		this.GetComponent<Button>().onClick.AddListener(() => Upgrade());
	}

	public void setupButton(){
		player.GetComponent<Upgrades>().setStartingInfo(thisis, startingCost, multi);
		Debug.Log("cost after " + this.gameObject.name + " button is done with it " + player.GetComponent<Upgrades>().pullUpgradeCost(thisis));
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponentInChildren<Text>().text = player.GetComponent<Upgrades>().pullUpgradeCost(thisis);

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
			float x = Input.GetTouch(0).position.x - transform.position.x;
			float y = Input.GetTouch(0).position.y - transform.position.y;
			Debug.Log("TOUCH X: " + Mathf.Abs(x) + " y: " + Mathf.Abs(y));
			if (Mathf.Abs(x) <= 50 && Mathf.Abs(y) <= 50){
				Upgrade();
			}
		}

		
	}

	public void Upgrade(){
		
		//Debug.Log("enum to int " + enumToInt(thisis));
		player.GetComponent<Upgrades>().upgradeAbility(thisis, increaseD, flatIncrease);
		/*if (thisis == UpgradeType.Skeleton){ //only one skeleton upgrade
			this.gameObject.SetActive(false);
		}*/
	}
}
