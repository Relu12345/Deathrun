using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenuscript : MonoBehaviour
{
    
    void Start()
    {
        
    }

   public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void Update()
    {
        
    }
}
