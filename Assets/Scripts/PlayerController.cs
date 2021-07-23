using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    public GameObject badGuy;
    public HealthBar healthBar;
    public bool busy = false;
    public bool lHookAct = false;
    public bool rHookAct = false;
    public bool lDodgeAct = false;
    public bool rDodgeAct = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLeftHook(){
        if (!busy){lHookAct = true; busy = true; StartCoroutine(DoAnimation("LeftHook"));}
    }

    public void DoRightHook(){
        if (!busy){rHookAct = true; busy = true; StartCoroutine(DoAnimation("RightHook"));}
    }

    public void DoRightBlock(){
        if (!busy){rDodgeAct = true; busy = true; StartCoroutine(DoAnimation("RightBlock"));}
    }

    public void DoLeftBlock(){
        if (!busy){lDodgeAct = true; busy = true; StartCoroutine(DoAnimation("LeftBlock"));}
    }

    IEnumerator DoAnimation(string action){
        animator.SetTrigger(action);
        yield return new WaitForSeconds(0.5f);
        if (lHookAct || rHookAct)
            badGuy.GetComponent<EnemyController>().PunchReceived(lHookAct);
        switch (action)
        {
            case "LeftHook":
                lHookAct = false; busy = false; break;
            case "RightHook":
                rHookAct = false; busy = false; break;
            case "RightBlock":
                lDodgeAct = false; busy = false; break;
            case "LeftBlock":
                rDodgeAct = false; busy = false; break;
        }
    }

    public void PunchReceived(bool left){
        if (!lDodgeAct && left){
            animator.SetTrigger("Hit");
            healthBar.DecreaseHealth();
        }
        else if (!rDodgeAct && !left) {
            animator.SetTrigger("Hit");
            healthBar.DecreaseHealth();
        }  
    }

    // Update is called once per frame
    void Update()
    {

        bool leftHook = Input.GetKey("h");
        bool rightBlock = Input.GetKey("k");
        bool leftBlock = Input.GetKey("j");
        bool rightHook = Input.GetKey("l");

        // animator.SetBool("LeftHook", leftHook);
        // animator.SetBool("LeftBlock", leftBlock);
        // animator.SetBool("RightBlock", rightBlock);
        // animator.SetBool("RightHook", rightHook);

        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));

    }
}
