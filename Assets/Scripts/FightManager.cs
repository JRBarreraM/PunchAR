using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    LineRenderer lineRenderer;
    public AudioManager audMan;
    public GameObject player;
    public GameObject[] badGuys;
    protected GameObject GoToMenuButton;
    protected GameObject GoToNextFight;
    protected GameObject GoToMonaButton;
    bool fighting = false;
    public bool playerMarker = false;
    public bool badGuyMarker = false;
    public int level = 0;
    public bool gameModeMarkers;
    public bool BlueMarker;
    public bool RedMarker;
    public bool YellowMarker;
    public bool GreenMarker;

    void Awake()
	{
        gameModeMarkers = GameObject.Find("GameManager").GetComponent<GameManager>().gameModeMarkers;
        if (gameModeMarkers){
            GameObject.Find("BlueButton").SetActive(false);
            GameObject.Find("GreenButton").SetActive(false);
            GameObject.Find("RedButton").SetActive(false);
            GameObject.Find("YellowButton").SetActive(false);
        }else{
            GameObject.Find("BlueButtonAR").SetActive(false);
            GameObject.Find("GreenButtonAR").SetActive(false);
            GameObject.Find("RedButtonAR").SetActive(false);
            GameObject.Find("YellowButtonAR").SetActive(false);
        }
        audMan = GameObject.Find("GameManager").GetComponent<AudioManager>();
        GoToMenuButton = GameObject.Find("Menu");
        GoToNextFight = GameObject.Find("NextFight");
        GoToMonaButton = GameObject.Find("Congratulations");
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GoToMenuButton.SetActive(false);
        GoToNextFight.SetActive(false);
        GoToMonaButton.SetActive(false);
    }

    public void activeButtonRed(){
        RedMarker = true;
    }
    public void inactiveButtonRed(){
        RedMarker = false;
    }
    public void activeButtonBlue(){
        BlueMarker = true;
    }
    public void inactiveButtonBlue(){
        BlueMarker = false;
    }
    public void activeButtonYellow(){
        YellowMarker = true;
    }
    public void inactiveButtonYellow(){
        YellowMarker = false;
    }
    public void activeButtonGreen(){
        GreenMarker = true;
    }
    public void inactiveButtonGreen(){
        GreenMarker = false;
    }
    public void activePlayer() {
        playerMarker = true;
    }
    
    public void inactivePlayer() {
        playerMarker = false;
    }

    public void activeRival() {
        badGuyMarker = true;
    }

    public void inactiveRival() {
        badGuyMarker = false;
    }

    public void boxerDown(string boxer){
        if (boxer == "Player"){
            GoToMenuButton.SetActive(true);
        }
        else if (boxer != "Player" && level < 2){
            GoToNextFight.SetActive(true);
        }
        else {
            GoToMonaButton.SetActive(true);
        }
    }

    public void StartNextFight(){
        fighting = false;
        badGuys[level].GetComponent<BoxerController>().Reset();
        audMan.Stop("JohnCena");
        audMan.Play("Main Theme");
        badGuys[level].SetActive(false);
        level++;
        player.GetComponent<BoxerController>().Reset();
        badGuys[level].SetActive(true);
        GoToNextFight.SetActive(false);
        lineRenderer.enabled = true;
    }

    IEnumerator startFight() {
        fighting = true;
        audMan.Play("countTo3");
        yield return new WaitForSeconds(4f);
        lineRenderer.enabled = false;
        player.GetComponent<Animator>().SetTrigger("Ready");
        badGuys[level].GetComponent<Animator>().SetTrigger("Ready");
        player.GetComponent<BoxerController>().busy = false;
        badGuys[level].GetComponent<BoxerController>().busy = false;
    }

    public void GoToMenu(){
        audMan.Stop("Naruto");
        audMan.Play("Main Theme");
        SceneManager.LoadScene("Menu");
    }

    public void GoToMona(){
        SceneManager.LoadScene("Mona");
    }

    // Update is called once per frame
    void Update()
    {
        if ( playerMarker && badGuyMarker ) {
            // Rendering Line
            Vector3[] positions = new Vector3[2] {player.transform.position, badGuys[level].transform.position };
            lineRenderer.SetPositions(positions);
            float distance = (player.transform.position - badGuys[level].transform.position).magnitude;

            if(distance > 1.5 || distance < 1) {
                lineRenderer.material.color = Color.red;
            } else {
                lineRenderer.material.color = Color.green;
                if (!fighting){
                    if ((gameModeMarkers && BlueMarker && RedMarker && YellowMarker && GreenMarker) || !gameModeMarkers){
                        StartCoroutine(startFight());
                    }
                }
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}
