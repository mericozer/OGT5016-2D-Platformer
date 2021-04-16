using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//General control point of players life count and collectibles
//It uses Singleton pattern
public class CanvasController : MonoBehaviour
{
    //Text objects to visualize the life and collectibles on the screen
    [SerializeField]
    private Text lifeCountText; //Life text
    [SerializeField]
    private Text flowerCountText; //Flower text
    [SerializeField]
    private Text keyCountText; //Key text

    //Condition panels and game objects which text objects are connected
    [SerializeField]
    private GameObject keyCounter; //Key object
    [SerializeField]
    private GameObject flowerCounter; //Flower robject
    [SerializeField]
    private GameObject pausePanel; //Pause
    [SerializeField]
    private GameObject winPanel; //Win
    [SerializeField]
    private GameObject losePanel; //Lose, it shows when one life decreasing
    [SerializeField]
    private GameObject PermaDeath; //Permanent death works when life count decreases to 0
    
    private int lifeCount;
    private int flowerCount;
    private int keyCount;
    
    private bool isGameRunning = true;

    //Singleton
    private static CanvasController _instance;

    public static CanvasController Instance
    {
        get { return _instance; }
    }

    //Getter and setters for multiple variables
    public bool IsGameRunning
    {
        get => isGameRunning;
        set => isGameRunning = value;
    }

    public int LifeCount
    {
        get => lifeCount;
        set => lifeCount = value;
    }

    public int FlowerCount
    {
        get => flowerCount;
        set => flowerCount = value;
    }

    public void Awake()
    {
        //checks if there are any Canvas controller in the scene because of the singleton pattern
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //To make scenes works in normal time scale
        Time.timeScale = 1f;
        
        //life count has to keep in memory so player can continue to all levels with its current life count
        if (PlayerPrefs.GetInt("Life") > 0)
        {
            lifeCount = PlayerPrefs.GetInt("Life");
        }
        else
        {
            lifeCount = 3;
            PlayerPrefs.SetInt("Life", lifeCount);
        }
        
        //visualize life count
        lifeCountText.text = "x" + lifeCount.ToString();
        
        //adjust keys and flower counts for the start of the level
        flowerCount = 0;
        keyCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //pause the game. If press while game is paused, game continues
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameRunning)
            {
                Time.timeScale = 0;
                isGameRunning = false;
                pausePanel.SetActive(true);
            }
            else
            {
                ContinueGame();
            }
           
            
        }
    }

    //works for only decreasing the players life count
    //it updates the life count and visualizes it
    public void UpdateLifeStats()
    {
        lifeCount--;
        lifeCountText.text = "x" + lifeCount.ToString();
        PlayerPrefs.SetInt("Life", lifeCount);
        if (lifeCount <= 0)
        {
            PermaDeath.SetActive(true);
        }
    }

    //works for flowers and keys
    //key count can be increase and decrease. It work for both ways
    //flower count can only increase 
    //both key and flower visuals are not active at the start if their count is more than 0 than they become active in the scene
    public void UpdateCollectibles(String obj)
    {
        switch (obj)
        {
            case "KeyUp":
                if (!keyCounter.activeSelf)
                {
                    keyCounter.SetActive(true);
                }

                keyCount++;
                keyCountText.text = "x" + keyCount;
                break;
            case "KeyDown":

                keyCount--; 
                if (keyCounter.activeSelf && keyCount == 0)
                {
                    keyCounter.SetActive(false);
                }
                keyCountText.text = "x" + keyCount;
                
                break;
            case "Flower":
                if (!flowerCounter.activeSelf)
                {
                    flowerCounter.SetActive(true);
                }

                flowerCount++;
                flowerCountText.text = "x" + flowerCount;

                break;
        }
    }

    //checks if player have any keys
    public bool HaveKey()
    {
        Debug.Log("Keys: " + keyCount);
        if (keyCount > 0)
        {
            UpdateCollectibles("KeyDown");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    //unpause the game
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isGameRunning = true;
        pausePanel.SetActive(false);
        
    }

    //delay for showing panels after the win and death
    public IEnumerator OpenPanel(string panel){
       
        yield return new WaitForSeconds(1f);
        switch (panel)
        {
            case "Dead":
                losePanel.SetActive(true);
                break;
            
            case "Win":
                winPanel.SetActive(true);
                break;
            
        }
        
    }



}
