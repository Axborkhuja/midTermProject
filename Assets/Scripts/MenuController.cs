using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void quitgame(){
        Application.Quit();
    }
}
