using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownSpawner : MonoBehaviour {

	public enum enemyType{
		cop,
		medcop,
		hardcop
	};

	public GameObject cop;

	public GameObject medcop;
	public GameObject hardcop;


	private GameObject currentTown;

	public GameObject townPrefab;

	public GameObject[] backgrounds = new GameObject[3];

	public Sprite[] townImages = new Sprite[9];

	public enum townSprite {
		basesprite
	}

	public int numTowns = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTown == null){
			numTowns++;
			Instantiate(townPrefab, new Vector3(16,-0.2f,-0.1f), Quaternion.identity);
			currentTown = GameObject.Find("Town(Clone)");
			prepareTheTown(numTowns);
            GameObject.Find("Player").GetComponent<Upgrades>().scaleSpawnCost(numTowns);
		}
		else if (currentTown.transform.position.x > 7f){
			currentTown.transform.position -= new Vector3(0.05f,0,0);
			for (int i = 0; i < backgrounds.Length; i++){
				backgrounds[i].transform.position -= new Vector3(0.05f,0,0);
				if (backgrounds[i].transform.position.x <= -20){
					backgrounds[i].transform.position = new Vector3(33.9f,backgrounds[i].transform.position.y,0);
				}
			}
			GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
			for (int i = 0; i < zombies.Length; i++){
				zombies[i].transform.position -= new Vector3(0.1f,0,0);
			}

		}
		
	}

	void prepareTheTown(int n){
		int TE = Random.Range(n+1, n+5); //total number of enemies
		Debug.Log("total enemies: " + TE);

		float SF = 9/n; //how often they spawn

		int hp = n * 100;

		//need something for multiple enemies and towns

		int image = Random.Range(0,9);

		enemyType e;

		if (n < 4){
			e = enemyType.cop;
		}
		else if (n < 8){
			e = enemyType.medcop;
		}
		else{
			e = enemyType.hardcop;
		}

		//always cops for now
		currentTown.GetComponent<TownScript>().SetUpTown(TE, SF, enemyType.cop, hp, townImages[image]);
	}
}
