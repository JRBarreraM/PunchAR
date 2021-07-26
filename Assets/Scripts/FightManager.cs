using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    LineRenderer lineRenderer;
    public AudioManager audMan;
    public GameObject player;
    public GameObject[] badGuys;
    bool fighting = false;
    public bool playerMarker = false;
    public bool badGuyMarker = false;
    private int level = 0;

    void Awake()
	{
        audMan = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
                if (!fighting)
                    StartCoroutine(startFight());
            }
        }
    }
}
