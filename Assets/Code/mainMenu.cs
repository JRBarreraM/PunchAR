﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Fight");
    }
    public void QuitGame(){
        Debug.Log("Quit!");
        Application.Quit();
    }
}