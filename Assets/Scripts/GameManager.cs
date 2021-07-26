using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioManager audMan;
    public bool gameModeMarkers;

    // Start is called before the first frame update
    void Start()
    {
        audMan.Play("Main Theme");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
