using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lookAtCamera : MonoBehaviour
{

    public GameObject Camera;
    public AudioManager audMan;

    void Awake()
	{
        audMan = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("ARCamera");
    }

    public void GoToMenu(){
        audMan.Stop("JohnCena");
        audMan.Play("Main Theme");
        SceneManager.LoadScene("Menu");
    }
    // Update is called once per frame
    void Update()
    {
        // Looking at camera
        Vector3 targetPostition = new Vector3(Camera.transform.position.x, this.transform.position.y, Camera.transform.position.z);
        this.transform.LookAt(targetPostition);
    }
}
