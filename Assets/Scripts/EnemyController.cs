using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BoxerController
{
    LineRenderer lineRenderer;
    bool fighting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DoAttack());
        lineRenderer = GetComponent<LineRenderer>();
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
        if (number >= 6) {
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
            if (number >= 6) {
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
                    animator.SetTrigger("Hit");
                }
            }
            yield return new WaitForSeconds(1.5f);
            if (!isDown)
                busy = false;
        }
    }

    IEnumerator startFight() {
        animator.SetTrigger("Ready");
        badGuy.GetComponent<Animator>().SetTrigger("Ready");
        busy = false;
        badGuy.GetComponent<BoxerController>().busy = false;
        fighting = true;
        yield return new WaitForSeconds(1.5f);
    }

    void Update() {

        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        Vector3[] positions = new Vector3[2] {transform.position, badGuy.transform.position };
        lineRenderer.SetPositions(positions);
        float distance = (transform.position - badGuy.transform.position).magnitude;

        if(distance > 1.5 || distance < 1) {
            lineRenderer.material.color = Color.red;
        } else {
            if (!fighting)
                StartCoroutine(startFight());
            lineRenderer.material.color = Color.green;   
        }
    }

}
