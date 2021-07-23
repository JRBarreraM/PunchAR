using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerController : MonoBehaviour
{
    protected Animator animator;
    public GameObject badGuy;
    public HealthBar healthBar;
    public Countdown countdown;
    public int KO = 0;
    public bool isDown = false;
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
        if (lHookAct || rHookAct) {
            badGuy.GetComponent<BoxerController>().PunchReceived(lHookAct);
        }
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
        if ((!lDodgeAct && left) || (!rDodgeAct && !left)){
            bool alive = healthBar.DecreaseHealth();
            if (!alive && !isDown){
                StartCoroutine(Knocked());
            }
            else if (alive){
                animator.SetTrigger("Hit");
            }
        }
    }

    IEnumerator Knocked(){
        KO++;
        isDown = true;
        busy = true;
        yield return new WaitForSeconds(1f);
        switch (KO)
        {
            case 1:
                countdown.CountTo(4);
                yield return new WaitForSeconds(4f);
                break;
            case 2:
                countdown.CountTo(7);
                yield return new WaitForSeconds(7f);
                break;
            default:
                countdown.CountTo(10);
                yield return new WaitForSeconds(10f);
                break;
        }

        yield return new WaitForSeconds(1f);
        busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));

    }
}
