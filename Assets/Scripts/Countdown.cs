using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public AudioManager audMan;
    public TMP_Text countdownDisplay;

    void Awake()
	{
		audMan = GameObject.Find("GameManager").GetComponent<AudioManager>();
        CountTo(4);
	}

    public void CountTo(int goal){
        StartCoroutine(CountdownToStart(goal));
    }

    IEnumerator CountdownToStart(int goal){
        string sound;

        switch (goal)
        {
            case 4:
                sound = "countTo4";
                break;
            case 7:
                sound = "countTo7";
                break;
            default:
                sound = "countTo10";
                break;
        }
        audMan.Play(sound);
        yield return new WaitForSeconds(1f);
        int i = 1;
        while(i <= goal){
            countdownDisplay.text = i.ToString();
            yield return new WaitForSeconds(1f);
            i++;
        }
        countdownDisplay.text = "";
    }
}