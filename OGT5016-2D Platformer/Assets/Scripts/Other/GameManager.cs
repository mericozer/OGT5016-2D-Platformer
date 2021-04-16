using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Makes start the game from main menu
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Life", 3);
    }

    //opens any level with full life count
    public void ChooseLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
        PlayerPrefs.SetInt("Life", 3);
    }

    //restart the current scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //pass to the next level
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //back to main menu
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
    
    //closes the game
    public void Quit()
    {
        Application.Quit();
    }



}
