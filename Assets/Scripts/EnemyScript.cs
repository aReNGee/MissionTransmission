using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyScript : MonoBehaviour {

	//zombie prefab stats

	public int baseAttack;

	public int baseHealth;

	public float baseMS;

	public float ASCap;

	public float curHealth;

	//end zombie prefab stats

	private Animator animator;

	private GameObject myEnemy;

	private bool inCombat = false;

	private float attackTimer;

	public bool markedForDeath = false;

    public bool killedByPoison = false;

	float ASTime;

	public Upgrades pscript;

    private bool ded = false;

	private TargetFinder TF;

	private float distanceToTarget;

	private float stoppingdistance = -1.2f;

	// Use this for initialization
	void Start () {
        pscript = GameObject.Find("Player").GetComponent<Upgrades>();

        int copLevel = GameObject.Find("GameController").GetComponent<TownSpawner>().numTowns;

        baseAttack = (baseAttack * copLevel);
        baseHealth = (baseHealth * copLevel);
		float baseAS = 0.7f + (0.1f * copLevel);
		if (baseAS > ASCap){
			baseAS = ASCap;
		}
        ASTime = 3/baseAS; //goes lower as attackspeed gets higher
		animator = this.GetComponent<Animator>();
		animator.SetFloat("AttackSpeed", baseAS);
		Debug.Log("Attack Time: " + baseAS);

		curHealth = baseHealth;

		TF = this.gameObject.GetComponent<TargetFinder>();

		setMovementTrigger();
	}
	
	// Update is called once per frame
	void Update () {
		//enemy movement

		if (!markedForDeath){
			//new thingy
			if (myEnemy != null){
				if (myEnemy.gameObject.tag == TF.secondaryTarget && TF.checkForPrimaryTargets() == true){
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
						inCombat = myEnemy.GetComponent<ZombieScript>().getAttacked(baseAttack);
					}
				}
				else { //move closer
					checkDistance();
					setMovementTrigger();
					this.gameObject.transform.Translate(Time.deltaTime * -baseMS, 0, 0);
					if (this.transform.position.x <= -10){
					//PlayerPrefs.SetInt("LastTime", pscript.getTime());
					//you lose
					SceneManager.LoadScene("Lose");
					}
				}
			}
			else if (TF.targetAvailable()){
				findEnemy();
			}
			else {
				this.gameObject.transform.Translate(Time.deltaTime * -baseMS, 0, 0);
				if (this.transform.position.x <= -10){
					SceneManager.LoadScene("Lose");
					}
			}
				//old block
				/*/
			if (!inCombat){
				//setMovementTrigger();
				this.gameObject.transform.Translate(Time.deltaTime * -baseMS, 0, 0);
				//animator.SetBool("Walking", true);
				if (this.transform.position.x <= -10){
					//you lose
					SceneManager.LoadScene("Lose");
				}
			}
			else { //in combat
                if (myEnemy == null)
                {
                    inCombat = false;
                    setMovementTrigger();
                    this.gameObject.transform.Translate(0, 0, 0.1f); //jiggle to reset triggers/targets
                    this.gameObject.transform.Translate(0, 0, -0.1f);
                }
                attackTimer += Time.deltaTime;
				if (attackTimer >= ASTime){
					attackTimer = 0;
					//animator.Play("skill_1");
					animator.SetTrigger("AttackTrigger");
					inCombat = myEnemy.GetComponent<ZombieScript>().getAttacked(baseAttack);
					if (!inCombat){
						//pscript.zombieDied();
						setMovementTrigger();
						this.gameObject.transform.Translate(0, 0, 0.1f); //jiggle to reset triggers/targets
						this.gameObject.transform.Translate(0, 0, -0.1f);
					}	
					//Debug.Log("TO BATTLE");
					//Attack
				}
			} */
		}
		else {
            dies();
		} 
	}

	private void findEnemy(){
		TF.checkForTargets();
		if (TF.targetAvailable()){ //true is targets are available
			myEnemy = TF.getNearestTarget(this.transform.position);
			checkDistance();
			Debug.Log("new enemy acquired");
		}
		else {
			inCombat = false;
			Debug.Log("No enemies detected");
		}
	}

	void checkDistance(){
		if (myEnemy != null){
			distanceToTarget = myEnemy.transform.position.x - this.transform.position.x;
			Debug.Log("distance to target is " + distanceToTarget);
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

    void dies()
    {
        if (!ded)
        {
            animator.SetBool("isDead", true);
            Invoke("goddamnit",1.0f);
            Destroy(gameObject, 1);
            ded = true;
        }
    }

    void goddamnit()
    {
        if (!killedByPoison)
        {
            pscript.enemyDied();
        }
    }

	void setMovementTrigger(){
		if (inCombat){
			animator.SetBool("Walking", false);
		}
		else {
			animator.SetBool("Walking", true);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
    {
		/*/
		if (!inCombat){
			if (other.gameObject.tag == "Zombie")
			{
				//Debug.Log("entering combat!");
				if (other.GetComponent<ZombieScript>().markedForDeath == false){
				myEnemy = other.gameObject;
				inCombat = true;
				setMovementTrigger();
				attackTimer = ASTime * 0.5f;
				//animator.SetTrigger("AttackTrigger");
				}
			}  
		} 
		*/
    }

	public bool getAttacked(int incomingAtk){
		Debug.Log("ENEMY IS TAKING AN ATTACK");
		curHealth -= incomingAtk;
		bool alive = true;
		if (curHealth <= 0){
			markedForDeath = true;
			alive = false;
		}
		return alive;
	}
}
