using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour {

	//zombie prefab stats

	public int baseAttack;

	public int baseHealth;

	public float baseMS;

	public float baseAS;

	//end zombie prefab stats

	int atk, maxHealth, curHealth, poisonDMG, exploDMG;

	float AS, MS;

	public Upgrades player;

	private Animator animator;

	private GameObject enemy;

	public bool inCombat = false;

	private float attackTimer;

	public bool markedForDeath = false;

	float ASTime;

	private bool attackingTown = false;

	public Upgrades pscript;

	//for new targeting mechanism

	private TargetFinder TF;

	private float distanceToTarget;

	private float stoppingdistance = -1.2f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Upgrades>();
		int[] multipliers = player.returnUpgrades();

		//setting up base stats

		atk = baseAttack + multipliers[1];
		maxHealth = baseHealth + multipliers[2];
		curHealth = maxHealth;

		//attack speed block
		AS = baseAS + 0.1f * multipliers[3]; //you want increased attack speed
		ASTime = 3/AS; //goes lower as attackspeed gets higher
		animator = this.GetComponent<Animator>();
		animator.SetFloat("AttackSpeed", AS);
		Debug.Log("Attack Time: " + ASTime);

		//end block
		MS = baseMS + 0.1f * multipliers[0]; //movement slowly increases
		poisonDMG = multipliers[4];
		exploDMG = multipliers[5];

		setMovementTrigger();

		//set the numbers up top
		GameObject.Find("ATK").GetComponent<Text>().text = atk.ToString();
		GameObject.Find("HP").GetComponent<Text>().text = maxHealth.ToString();
		GameObject.Find("AS").GetComponent<Text>().text = AS.ToString();
		GameObject.Find("MS").GetComponent<Text>().text = MS.ToString();

		//zombie counter
		pscript = GameObject.Find("Player").GetComponent<Upgrades>();
		//pscript.zombieSpawned();
		TF = this.gameObject.GetComponent<TargetFinder>();
	}
	
	// Update is called once per frame
	void Update () {
		//zombie movement
		if (!markedForDeath){
			//new block for motion and stuff
			if (enemy != null){
				if (enemy.gameObject.tag == TF.secondaryTarget && TF.checkForPrimaryTargets() == true){
					Debug.Log("secondary target. searching for main target");
					findEnemy();
					checkDistance();
				}
				if (distanceToTarget > stoppingdistance){ //they are close enough to attack
					attackTimer += Time.deltaTime;
					if (attackTimer >= ASTime){
						attackTimer = 0;
						//animator.Play("skill_1");
						animator.SetTrigger("AttackTrigger");
						//Debug.Log("should trigger enemy");
						if (!attackingTown){
							inCombat = enemy.GetComponent<EnemyScript>().getAttacked(atk);
						}
						else {
							inCombat = enemy.GetComponent<TownScript>().getAttacked(atk);
						}
					}
				}
				else { //move closer
					checkDistance();
					setMovementTrigger();
					this.gameObject.transform.Translate(Time.deltaTime * MS, 0, 0);	
				}
			}
			else {
				findEnemy();
			}
		}
		else {
			animator.SetBool("isDead", true);
			Destroy(gameObject, 2);
		}
		 
	}

	private void findEnemy(){
		TF.checkForTargets();
		if (TF.targetAvailable()){ //true is targets are available
			enemy = TF.getNearestTarget(this.transform.position);
			if (enemy.tag == "Town"){
				attackingTown = true;
			}
			else {
				attackingTown = false;
			}
			checkDistance();
			Debug.Log("new enemy acquired");
		}
		else {
			inCombat = false;
			Debug.Log("No enemies detected");
		}
	}

	public void setMovementTrigger(){
		if (inCombat){
			//animator.SetBool("InCombat", true);
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsRunning", false);
		}
		else {
			//animator.SetBool("InCombat", false);
			if (MS >= 1.5f)
            {
                animator.SetBool("IsWalking", false);
				animator.SetBool("IsRunning", true);
            }
            if (MS < 1.5f)
            {
                animator.SetBool("IsWalking", true);
				animator.SetBool("IsRunning", false);
            }
		}
	}

	void checkDistance(){
		if (enemy != null){
			distanceToTarget = this.transform.position.x - enemy.transform.position.x;
			//Debug.Log("distance to target is " + distanceToTarget);
			if (distanceToTarget >= stoppingdistance){
				inCombat = true;
				attackTimer = ASTime * 0.5f;
			}
			else {
				inCombat = false;
			}
		}
		else {
			inCombat = false;
		}
		setMovementTrigger();
	}

	void OnTriggerEnter2D(Collider2D other)
    {
		/*
		if (!inCombat){
			if (other.gameObject.tag == "Police")
			{
				//Debug.Log("entering combat!");
				
				if (other.GetComponent<EnemyScript>().markedForDeath == false){
					enemy = other.gameObject;
					inCombat = true;
				setMovementTrigger();
				attackTimer = ASTime * 0.5f;
				attackingTown = false;
				}
			}
			if (other.gameObject.tag == "Town"){
				enemy = other.gameObject;
				inCombat = true;
				setMovementTrigger();
				attackTimer = ASTime * 0.5f;
				attackingTown = true;
			}  
		} */
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
