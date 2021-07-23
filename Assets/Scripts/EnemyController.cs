using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BoxerController
{

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DoAttack());
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
}
