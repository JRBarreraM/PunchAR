using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxerController : MonoBehaviour
{
    protected Animator animator;
    public GameObject badGuy;
    public HealthBar healthBar;
    public Countdown countdown;
    private Button BlueButton;
    private Button RedButton;
    private Button YellowButton;
    private Button GreenButton;
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
        BlueButton = GameObject.Find("BlueButton").GetComponent<Button>();
        GreenButton = GameObject.Find("GreenButton").GetComponent<Button>();
        RedButton = GameObject.Find("RedButton").GetComponent<Button>();
        YellowButton =  GameObject.Find("YellowButton").GetComponent<Button>();
    }

    public void DoLeftHook(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown){lHookAct = true; busy = true; RedButton.interactable = false; StartCoroutine(DoAnimation("LeftHook"));}
    }

    public void DoRightHook(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown){rHookAct = true; busy = true; BlueButton.interactable = false; StartCoroutine(DoAnimation("RightHook"));}
    }

    public void DoRightBlock(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown){rDodgeAct = true; busy = true; YellowButton.interactable = false; StartCoroutine(DoAnimation("RightBlock"));}
    }

    public void DoLeftBlock(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown){lDodgeAct = true; busy = true; GreenButton.interactable = false; StartCoroutine(DoAnimation("LeftBlock"));}
    }

    IEnumerator DoAnimation(string action){
        animator.SetTrigger(action);
        if(gameObject.tag != "Player"){
            yield return new WaitForSeconds(0.25f);
            animator.speed = 0.3f;
            yield return new WaitForSeconds(0.25f);
            animator.speed = 1f;
        }
        yield return new WaitForSeconds(0.3f);
        if (lHookAct || rHookAct) {
            StartCoroutine(badGuy.GetComponent<BoxerController>().PunchReceived(lHookAct));
        }
        switch (action)
        {
            case "LeftHook":
                yield return new WaitForSeconds(1.5f);
                lHookAct = false; busy = false; RedButton.interactable = true; break;
            case "RightHook":
                yield return new WaitForSeconds(1.5f);
                rHookAct = false; busy = false; BlueButton.interactable = true; break;
            case "RightBlock":
                yield return new WaitForSeconds(0.5f);
                rDodgeAct = false; busy = false; YellowButton.interactable = true; break;
            case "LeftBlock":
                yield return new WaitForSeconds(0.5f);
                lDodgeAct = false; busy = false; GreenButton.interactable = true; break;
        }
    }

    public virtual IEnumerator PunchReceived(bool left){
        busy = true;
        if ((!lDodgeAct && left) || (!rDodgeAct && !left)){
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

    public IEnumerator Knocked(){
        KO++;
        isDown = true;
        busy = true;
        if(KO < 3) {
            animator.SetTrigger("Stunned");
        } else {
            animator.SetTrigger("KnockedOut");
        }
        yield return new WaitForSeconds(1f);
        switch (KO)
        {
            case 1:
                countdown.CountTo(4);
                yield return new WaitForSeconds(4f);
                animator.SetTrigger("GettingUp");
                isDown = false;
                healthBar.SetHealthBarValue(1.0f);
                break;
            case 2:
                countdown.CountTo(7);
                yield return new WaitForSeconds(7f);
                animator.SetTrigger("GettingUp");
                healthBar.SetHealthBarValue(1.0f);
                isDown = false;
                break;
            default:
                countdown.CountTo(10);
                yield return new WaitForSeconds(10f);
                break;
        }
        yield return new WaitForSeconds(4f);
        busy = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));
        // if (gameObject.tag == "Player")
            // Debug.Log(busy);
    }
}
