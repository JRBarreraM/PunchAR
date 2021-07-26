using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxerController : MonoBehaviour
{
    protected Animator animator;
    public AudioManager audMan;
    public GameObject badGuy;
    protected FightManager fightManager;
    public HealthBar healthBar;
    public Countdown countdown;
    private Button BlueButton;
    private Button RedButton;
    private Button YellowButton;
    private Button GreenButton;
    public ScoreVisualizer scoreVisualizer;
    public bool gameModeMarkers;
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
        busy = true;
        animator = GetComponent<Animator>();
        BlueButton = GameObject.Find("BlueButton").GetComponent<Button>();
        GreenButton = GameObject.Find("GreenButton").GetComponent<Button>();
        RedButton = GameObject.Find("RedButton").GetComponent<Button>();
        YellowButton =  GameObject.Find("YellowButton").GetComponent<Button>();
    }

    void Awake()
	{
        gameModeMarkers = GameObject.Find("GameManager").GetComponent<GameManager>().gameModeMarkers;
        fightManager = GameObject.Find("FightManager").GetComponent<FightManager>();
        audMan = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    public void DoLeftHook(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown && !isDown){
            lHookAct = true;
            busy = true;
            if (gameObject.tag == "Player" && !gameModeMarkers)
                RedButton.interactable = false;
            StartCoroutine(DoAnimation("LeftHook"));}
    }

    public void DoRightHook(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown && !isDown){
            rHookAct = true;
            busy = true;
            if (gameObject.tag == "Player" && !gameModeMarkers)
                BlueButton.interactable = false;
            StartCoroutine(DoAnimation("RightHook"));
        }
    }

    public void DoRightBlock(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown && !isDown){
            audMan.Play("Esquivo");
            rDodgeAct = true;
            busy = true;
            if (gameObject.tag == "Player" && !gameModeMarkers)
                YellowButton.interactable = false;
            StartCoroutine(DoAnimation("RightBlock"));
        }
    }

    public void DoLeftBlock(){
        if (!busy && !badGuy.GetComponent<BoxerController>().isDown && !isDown){
            audMan.Play("Esquivo");
            lDodgeAct = true;
            busy = true;
            if (gameObject.tag == "Player" && !gameModeMarkers)
                GreenButton.interactable = false;
            StartCoroutine(DoAnimation("LeftBlock"));
        }
    }

    IEnumerator DoAnimation(string action){
        animator.SetTrigger(action);
        if(gameObject.tag != "Player"){
            yield return new WaitForSeconds(0.25f);
            animator.speed = 0.3f;
            yield return new WaitForSeconds(0.25f);
            animator.speed = 1f;
        }
        yield return new WaitForSeconds(0.5f);
        if (lHookAct || rHookAct) {
            StartCoroutine(badGuy.GetComponent<BoxerController>().PunchReceived(lHookAct));
        }
        switch (action)
        {
            case "LeftHook":
                yield return new WaitForSeconds(1.5f);
                lHookAct = false;
                busy = false;
                if (gameObject.tag == "Player" && !gameModeMarkers)
                    RedButton.interactable = true;
                break;
            case "RightHook":
                yield return new WaitForSeconds(1.5f);
                rHookAct = false;
                busy = false;
                if (gameObject.tag == "Player" && !gameModeMarkers)
                    BlueButton.interactable = true;
                break;
            case "RightBlock":
                yield return new WaitForSeconds(0.5f);
                rDodgeAct = false;
                busy = false;
                if (gameObject.tag == "Player" && !gameModeMarkers)
                    YellowButton.interactable = true;
                break;
            case "LeftBlock":
                yield return new WaitForSeconds(0.5f);
                lDodgeAct = false;
                busy = false;
                if (gameObject.tag == "Player" && !gameModeMarkers)
                    GreenButton.interactable = true;
                break;
        }
    }

    public virtual IEnumerator PunchReceived(bool left){
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
        busy = false;
    }

    public IEnumerator Knocked(){
        KO++;
        scoreVisualizer.SetScore(KO);
        isDown = true;
        busy = true;
        if(KO < 3) {
            animator.SetTrigger("Stunned");
        } else {
            animator.SetTrigger("KnockedOut");
        }
        switch (KO)
        {
            case 1:
                audMan.Play("PunchKO");
                countdown.CountTo(4);
                yield return new WaitForSeconds(4f);
                animator.SetTrigger("GettingUp");
                isDown = false;
                healthBar.SetHealthBarValue(1.0f);
                break;
            case 2:
                audMan.Play("PunchKO");
                countdown.CountTo(7);
                yield return new WaitForSeconds(7f);
                animator.SetTrigger("GettingUp");
                healthBar.SetHealthBarValue(1.0f);
                isDown = false;
                break;
            default:
                audMan.Stop("Main Theme");
                audMan.Play("PunchKO");
                yield return new WaitForSeconds(0.5f);
                audMan.Play("BIGOOF");
                animator.speed = 0.3f;
                yield return new WaitForSeconds(2f);
                animator.speed = 1f;
                countdown.CountTo(10);
                yield return new WaitForSeconds(10f);
                badGuy.GetComponent<Animator>().SetTrigger("Victory");
                if (gameObject.tag != "Player")
                    audMan.Play("JohnCena");
                else {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>().enabled = true;
                    audMan.Play("Naruto");
                }
                fightManager.boxerDown(gameObject.tag);
                break;
        }
        yield return new WaitForSeconds(4f);
        if(KO < 3)
            busy = false;
    }

    public virtual void Reset(){
        healthBar.SetHealthBarValue(1.0f);
        KO = 0;
        scoreVisualizer.SetScore(KO);
        busy = true;
        animator.SetTrigger("NewFight");
        badGuy = fightManager.badGuys[fightManager.level];
    }

    // Update is called once per frame
    void Update()
    {
        // Looking at Rival
        Vector3 targetPostition = new Vector3(badGuy.transform.position.x, this.transform.position.y, badGuy.transform.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));
    }
}
