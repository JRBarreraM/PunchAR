using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BoxerController
{
    private float[] attack = {6, 4, 2};
    private float[] block = {7, 6, 3};
    private int level;

    void Start()
    {
        level = fightManager.level;
        animator = GetComponent<Animator>();
        StartCoroutine(DoAttack());
        busy = true;
    }   

    IEnumerator DoAttack() {
        while(true) {
            yield return new WaitForSeconds(2f);
            Attack();
        }
    }

    void Attack() {
        float number = Random.Range(0,11);
        if (number >= attack[level]) {
            int attack = Random.Range(1,3);
            switch (attack){
                case 1:
                    DoLeftHook();
                    break;
                case 2:
                    DoRightHook();
                    break;
            }
        }
    }

    public override IEnumerator PunchReceived(bool left){
        bool dodgeIt = false;
        if (!busy){
            float number = Random.Range(0,11);
            if (number >= block[level]) {
                dodgeIt = true;
                if (left){
                    DoRightBlock();
                }
                else {
                    DoLeftBlock();
                }
            }
        }
        if (!dodgeIt){
            busy = true;
            if ((!lDodgeAct && !left) || (!rDodgeAct && left)){
                bool alive = healthBar.DecreaseHealth();
                if (!alive && !isDown){
                    StartCoroutine(Knocked());
                }
                else if (alive){
                    audMan.Play("OOF");
                    animator.SetTrigger("Hit");
                }
            }
            yield return new WaitForSeconds(1.5f);
            if (!isDown)
                busy = false;
        }
    }

    void Update() {
        // Looking at player
        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));
    }

}
