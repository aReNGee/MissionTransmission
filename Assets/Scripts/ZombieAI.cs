using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject enemy;
    Animator enemyAnimator;
    private Animator animator;

    //how many times Youll be attacking
    private int atkTimes = 0;
    

    //private float walkStartTime = 0;
    public float speed = 0.5f;

    [HideInInspector]
    public float tmpSpeed;
    //public float dis = 0.1f;
    public float attackSpeed = 1.0f;

    //prefab for turning zombies
    public GameObject prefabReverseAnimClip;
    public GameObject prefabZombie;
    GameObject instance;
    
    //public Transform prefabPos;
    private Vector3 spawnPos;

    [HideInInspector]
    public bool isAlive = true;

    

    // Use this for initialization
    void Start()
    {
        
        tmpSpeed = speed;
        animator = this.GetComponent<Animator>();
        isAlive = true;
        this.GetComponent<BoxCollider2D>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        

        


        //moving the gameobject in the X-axis
        

        

    }

    


    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Police")
        {
            if (isAlive)
            {
                //Debug.Log("speed normal");
                tmpSpeed = speed;
                //CancelInvoke();
                animator.SetBool("isAttacking", false);
            }
        }

        //CancelInvoke();
    }



    void Attack()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        //animator.SetTrigger("skill_1");
        animator.SetBool("isAttacking", true);
        //Debug.Log("Police was hit");
        animator.SetFloat("AttackSpeed", attackSpeed);
        atkTimes++;

        if (atkTimes <= 3)
        {
            //enemyAnimator = enemy.GetComponent<Animator> ();
            if (atkTimes == 1 && enemy.GetComponent<PoliceAI>().isAlive)
            {
                enemyAnimator.SetTrigger("Hurt");


            }
            else if (atkTimes == 2 && enemy.GetComponent<PoliceAI>().isAlive)
            {
                enemyAnimator.SetTrigger("Hurt");


            }
            else if (atkTimes == 3 && enemy.GetComponent<PoliceAI>().isAlive)
            {
                //EnemyDeath();

                
                //Destroy(enemy.gameObject);
            }
        }
        
    }

    

    void EnemyDeath()
    {
        enemy.GetComponent<PoliceAI>().isAlive = false;
        animator.SetBool("isAttacking", false);
        enemyAnimator.SetTrigger("Hurt");

        enemyAnimator.SetBool("isDead", true);
        CancelInvoke();
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        enemy.GetComponent<PoliceAI>().tmpSpeed = 0;
        
        //tmpSpeed = speed;
        atkTimes = 0;
        //enemyAnimator.SetBool("turnZombie", true);
        //spawnPos = new Vector3(enemy.GetComponent<Transform>().position.x, enemy.GetComponent<Transform>().position.y, enemy.GetComponent<Transform>().position.z);

       // Invoke("SpawnZombieReverseAnim", 2);


    }

    void SpawnZombieReverseAnim()
    {
        Destroy(enemy);
        Debug.Log("invoked");
        instance = Instantiate(prefabReverseAnimClip, new Vector3(spawnPos.x - 5.0f, spawnPos.y - 0.5f, spawnPos.z) , Quaternion.identity);
        Invoke("SpawnZombie", 1.0f);
    }

    void SpawnZombie()
    {
        Destroy(instance);
        Instantiate(prefabZombie, new Vector3(spawnPos.x - 5.0f, spawnPos.y - 0.5f, spawnPos.z), Quaternion.identity);
    }

}