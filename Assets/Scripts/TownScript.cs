using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScript : TownSpawner {

	
	//set these variables to set the town
	public int totalEnemies;
	public float spawnFrequency;

	public enemyType eType;

	public int townHealth;

	private int curHealth;


	//end variables

	public TownSpawner tspawn;

	float counter;

	private bool markedForDeath = false;

	// Use this for initialization
	void Start () {
		tspawn = GameObject.Find("GameController").GetComponent<TownSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (markedForDeath){
			int PointsWhenKilled = 5 + (10 * tspawn.numTowns);
			Upgrades p = GameObject.Find("Player").GetComponent<Upgrades>();
			p.addPoints(PointsWhenKilled);
			p.killEverything();
			Destroy(gameObject);

		}
		counter += Time.deltaTime;
		if (counter > spawnFrequency){
			if (totalEnemies > 0){
				totalEnemies -=1;
				spawnEnemy();
			}
			counter = 0;
		}
	}

	public void SetUpTown(int TE, float SF, enemyType ET, int TH, Sprite sprt){
		totalEnemies = TE;
		spawnFrequency = SF;
		eType = ET;
		townHealth = TH;
		curHealth = townHealth;
		//setTownSprite(TS);
		this.gameObject.GetComponent<SpriteRenderer>().sprite = sprt;
	}

	void spawnEnemy(){ //do the thing
		//use switch case to iterate through
		switch (eType) {
			case enemyType.cop:
				Instantiate(tspawn.cop, this.transform.position - new Vector3(0,0.8f,0), Quaternion.identity);
			break;

			case enemyType.medcop:
				Instantiate(tspawn.medcop, this.transform.position - new Vector3(0,0.8f,0), Quaternion.identity);
			break;

			case enemyType.hardcop:
				Instantiate(tspawn.hardcop, this.transform.position - new Vector3(0,0.8f,0), Quaternion.identity);
			break;
			default:

			break;
		}
	}

	void setTownSprite(townSprite TS){
		//use switchcase to iterate through
	}

	public bool getAttacked(int incomingAtk){
		curHealth -= incomingAtk;
		bool alive = true;
		if (curHealth <= 0){
			markedForDeath = true;
			alive = false;
		}
		return alive;
	}
}
