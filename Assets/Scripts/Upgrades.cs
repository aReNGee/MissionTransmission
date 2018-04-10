using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour {

	public enum UpgradeType {
		MovementSpeed,
		Damage,
		Health,
		AttackSpeed,
		Poison,
		Explode,
		Skeleton
	};

	//putting zombie spawn block in here for now

	private GameObject currentZombie;

	public GameObject baseZombie;

	public GameObject lvl1Zombie;

	public GameObject lvl2Zombie;

	public GameObject lvl3Zombie;

	public GameObject[] zombieType = new GameObject[4];

	//time block
	float counter = 0;

	public float timeToSpawnZombie;

	//end zombie block

	private int[] upgradeLevel = new int[7];

	private int[] upgradeCost = new int[7];

	private int POINTS = 30;

	private float[] multiplier = new float[7];

	private Text pointsDisplay;

	public Transform spawnHere;

	private int numZombies = 0;

	public Text zCounter;

    private float timeCount = 0;

    private int timeDisplay = 0;


	// Use this for initialization
	void Start () {
		/*upgradeLevel = new int[7];

		upgradeCost = new int[7];
		
		multiplier = new float[7];*/

		//Debug.Log(upgradeLevel.Length);

		for (int x = 0; x < upgradeLevel.Length; x++){
			upgradeLevel[x] = 0;
		}
        
        for (int x = 0; x < upgradeCost.Length; x++){
			upgradeCost[x] = 0;
		}

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("UpgradeButton")){
            g.GetComponent<UpgradeButton>().setupButton();
        }


		/*
		

		for (int x = 0; x < multiplier.Length; x++){
			multiplier[x] = 1;
		}*/

		spawnHere = GameObject.Find("SpawnAtThis").GetComponent<Transform>();

		pointsDisplay = GameObject.Find("PointDisplay").GetComponent<Text>();
		//setting up zombies
		zombieType[0] = baseZombie;
		zombieType[1] = lvl1Zombie;
		zombieType[2] = lvl2Zombie;
		zombieType[3] = lvl3Zombie;

		currentZombie = zombieType[0];

		zCounter = GameObject.Find("ZombieCounter").GetComponent<Text>();

        GameObject workaround = Instantiate(currentZombie, new Vector3(-30, -30, 0), Quaternion.identity);
        Destroy(workaround, 0.1f);

        Debug.Log("LAST TIME WAS: " + PlayerPrefs.GetInt("LastTime"));
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int x = 0; x < upgradeLevel.Length; x++){
			Debug.Log(upgradeLevel[x]);
		}*/

        //POINTS++;
        pointsDisplay.text = POINTS.ToString();

        //zombie spawning

        //counter += Time.deltaTime; //uncomment to have zombies spawn over time
        if (counter >= timeToSpawnZombie)
        {
            counter = 0;
            //spawn a zombie
            Instantiate(currentZombie, spawnHere.position, Quaternion.identity);
        }

        //zCounter
        numZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
        zCounter.text = numZombies.ToString();

        //time update block
        timeCount += Time.deltaTime;
        timeDisplay = (int)timeCount;
        GameObject.Find("DISPLAYTIME").GetComponent<Text>().text = timeDisplay.ToString();

        
    }

	public int enumToInt(UpgradeType y){
		int x = 0;
		 switch (y)
        {
        case UpgradeType.MovementSpeed:
            x = 0;
            break;
        case UpgradeType.Damage:
            x = 1;
            break;
		case UpgradeType.Health:
            x = 2;
            break;
		case UpgradeType.AttackSpeed:
            x = 3;
            break;
		case UpgradeType.Poison:
            x = 4;
            break;
		case UpgradeType.Explode:
            x = 5;
            break;
		case UpgradeType.Skeleton:
            x = 6;
            break;
        default:
            
            break;
        }
		return x;
	}

    public void upgradeAbility(UpgradeType y, int increase, bool flat)
    {
        int x = enumToInt(y);
        if (POINTS >= upgradeCost[x])
        {
            POINTS -= upgradeCost[x];
            upgradeCost[x] = (int)((float)upgradeCost[x] * multiplier[x]);
            //Debug.Log("New Upgrade Cost: " + upgradeCost[x]);
            if (y == UpgradeType.Poison)
            { //kill everything
                killEverything();
            }
            else if (y == UpgradeType.Explode)
            {
                SceneManager.LoadScene("Win");
            }
            else if (y != UpgradeType.Skeleton)
            { //normal upgrade

                if (flat)
                {
                    upgradeLevel[x] += increase;
                }
                else
                { //multiplicitive. passed as a percentage
                    upgradeLevel[x] += upgradeLevel[x] * (increase / 100);
                }
                //comment out extra zombies for now
                /*
                //zombie upgrade block
                if (upgradeLevel[1] >= 400 && upgradeLevel[2] >= 500) //both
                {
                    currentZombie = zombieType[3];
                }
                else if (upgradeLevel[1] >= 400) //atk
                {
                    currentZombie = zombieType[2];
                }
                else if (upgradeLevel[2] >= 500){ //health
                    currentZombie = zombieType[1];
                }
                */

                GameObject workaround = Instantiate(currentZombie, new Vector3(-30, -30, 0), Quaternion.identity);
                Destroy(workaround, 0.1f);
            }
            else
            { //spawn a zombie
                Instantiate(currentZombie, spawnHere.position, Quaternion.identity);
            }
        }
    }

    public void setStartingInfo(UpgradeType y, int startCost, float mul){
		int x = enumToInt(y);
		upgradeCost[x] = startCost;
		multiplier[x] = mul;
	}

	public string pullUpgradeCost(UpgradeType y){
		int x = enumToInt(y);
		return upgradeCost[x].ToString();
	}

	public int[] returnUpgrades(){
		return upgradeLevel;
	}

	public void zombieSpawned(){
		numZombies++;
		zCounter.text = numZombies.ToString();
	}

	public void zombieDied(){
		numZombies--;
		zCounter.text = numZombies.ToString();
	}

	public void enemyDied(){
        int copLevel = GameObject.Find("GameController").GetComponent<TownSpawner>().numTowns;
        POINTS += copLevel * 5;
        Instantiate(currentZombie, spawnHere.position, Quaternion.identity);
    }

    public void scaleSpawnCost(int x)
    {
        upgradeCost[6] = upgradeCost[6] + (5 * x);
    }

    public void addPoints(int x){
        POINTS += x;
    }

    public void killEverything(){
        GameObject[] gg = GameObject.FindGameObjectsWithTag("Police");
        GameObject[] notGG = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject g in gg)
        {
            g.GetComponent<EnemyScript>().markedForDeath = true;
            g.GetComponent<EnemyScript>().killedByPoison = true;
        }
        foreach (GameObject n in notGG)
        {
            n.GetComponent<ZombieScript>().inCombat = false;
            n.GetComponent<ZombieScript>().setMovementTrigger();
        }
    }

    public int getTime(){
        return timeDisplay;
    }
}
