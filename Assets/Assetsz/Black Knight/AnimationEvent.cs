using UnityEngine;
using System;
using System.Collections;

//Just for demonstration, you can replace it with your own code logic.
public class AnimationEvent : MonoBehaviour {

	public GameObject enemy;
    ZombieAI zombie;
    Animator enemyAnimator;

    private int atkTimes = 0;

    void Start()
    {

        zombie = GetComponent<ZombieAI>();

    }

 //   public void AttackStart () {
 //       enemyAnimator = enemy.GetComponent<Animator>();
 //       Debug.Log ("Attack Start");
 //       Debug.Log(enemyAnimator);
	//	//Just for demonstration, you can replace it with your own code logic.
	//	atkTimes++;
	//	if (enemy && atkTimes <= 3) {
	//		//enemyAnimator = enemy.GetComponent<Animator> ();
	//		if (atkTimes == 1 && zombie.isAttacking == true)
 //           {
 //               enemyAnimator.SetTrigger("Hurt");
                

 //           } else if (atkTimes == 2 && zombie.isAttacking == true)
 //           {
 //               enemyAnimator.SetTrigger("Hurt");


 //           } else if (atkTimes == 3 && zombie.isAttacking == true)
 //           {
 //               EnemyDeath();

 //               zombie.isAttacking = false;
 //               //Destroy(enemy.gameObject);
	//		} 
	//	}
	//}

	//public void AttackStartEffectObject () {
	//	Debug.Log ("Fire Effect Object");
	//}

    //public void PlayDamage()
    //{
    //    //Animator enemyAnimator = enemy.GetComponent<Animator>();
    //    this.gameObject.GetComponent<Animator>().SetTrigger("hit_1");
    //}

    //void EnemyDeath()
    //{
    //    enemyAnimator.SetTrigger("Hurt");

    //    enemyAnimator.SetBool("isDead", true);
    //    zombie.CancelInvoke();
    //    enemy.GetComponent<BoxCollider2D>().enabled = false;
    //    enemy.GetComponent<PoliceAI>().tmpSpeed = 0;
    //    zombie.isAttacking = false;
    //    zombie.tmpSpeed = zombie.speed;
    //    atkTimes = 0;
    //}
}
