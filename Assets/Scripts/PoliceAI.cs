using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    

    private Animator policeAnim;
    public float speed = 0.2f;

    [HideInInspector]
    public float tmpSpeed;


    //getting the zombie gameobject
    [HideInInspector]
    public GameObject zombie;
    Animator zombieAnimator;

    private float atkTimes = 0;

    [HideInInspector]
    public bool isAlive = true;

    //prefab for turning zombies
    //public GameObject prefab;
    //public Transform prefabPos;

    // Use this for initialization
    void Start()
    {

        tmpSpeed = speed;
        policeAnim = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //moving the gameobject in the X-axis
        transform.Translate(-Time.deltaTime * tmpSpeed, 0, 0);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {

            zombie = other.gameObject;

            tmpSpeed = 0;

            if (tmpSpeed == 0)
            {
                InvokeRepeating("Attack", 0.0f, 1.0f);
            }
            else
            {
                CancelInvoke();
                policeAnim.SetBool("isAttacking", false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            if (isAlive)
            {
                tmpSpeed = speed;
                //CancelInvoke();
                policeAnim.SetBool("isAttacking", false);
            }
        }
    }

    void Attack()
    {
        //zombieAnimator = zombie.GetComponent<Animator>();
        policeAnim.SetBool("isAttacking", true);
        //atkTimes += 1.0f;
        /*if (atkTimes <= 3)
        {
            //enemyAnimator = enemy.GetComponent<Animator> ();
            if (atkTimes == 1 && zombie.GetComponent<ZombieAI>().isAlive)
            {
                //zombieAnimator.SetTrigger("hit_1");
                //Debug.Log("zombie was hit");

            }
            else if (atkTimes == 2 && zombie.GetComponent<ZombieAI>().isAlive)
            {
                //zombieAnimator.SetTrigger("hit_1");


            }
            else if (atkTimes == 3 && zombie.GetComponent<ZombieAI>().isAlive)
            {
                EnemyDeath();

                //zombie.isAttacking = false;
                //Destroy(enemy.gameObject);
            }
        }*/
    }

    void EnemyDeath()
    {
        
        zombie.GetComponent<ZombieAI>().isAlive = false;
        //isAlive = false;
        
        policeAnim.SetBool("isAttacking", false);
        //zombieAnimator.SetTrigger("hit_1");
        zombieAnimator.SetTrigger("death");
        


        CancelInvoke();
        zombie.GetComponent<BoxCollider2D>().enabled = false;
        zombie.GetComponent<ZombieAI>().tmpSpeed = 0;
        //zombie.isAttacking = false;
        //tmpSpeed = speed;
        atkTimes = 0;

        

    }

    void destroyZombie()
    {
        Destroy(zombie);
    }
}
