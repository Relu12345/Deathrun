using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenuscript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Multiplayer_Game");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void Update()
    {
        
    }
}
