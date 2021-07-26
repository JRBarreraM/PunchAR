using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public void SelectGameModeMarkers()
    {
        gameManager.gameModeMarkers = true;
        SceneManager.LoadScene("Fight");
    }
    public void SelectGameModeScreen()
    {
        gameManager.gameModeMarkers = false;
        SceneManager.LoadScene("Fight");
    }
    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Start(){
        gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}