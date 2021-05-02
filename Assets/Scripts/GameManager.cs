using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int currentLevel;
    public bool readyToLoad;
    private float checkCD;
    public int clearLevel;

    public static GameManager instance = null;

    public void CloseToNPC()//giving a delay on npc Checking 
    {
        checkCD = 0.1f;
    }

    public bool FarToNPC()
    {
        if (checkCD <= 0)//if player is far to npc after the delay
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CanLoad(bool v)
    {
        readyToLoad = v;
    }
    public void SetSceneToLoad(int v)
    {
        currentLevel = v;
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        readyToLoad = false;
        DontDestroyOnLoad(this);
        checkCD = 0f;
        clearLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkCD>0)
        {
            checkCD -= Time.deltaTime;
        }

        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    NextLevel();
        //}


        LoadGame();
        
    }

    private void LoadGame()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (readyToLoad == true)
            {
                SceneManager.LoadScene(currentLevel,LoadSceneMode.Single);
            }
        }

    }
    public void NextLevel()
    {
        if(currentLevel == clearLevel)
        {
            clearLevel++;
            SceneManager.LoadScene("WalkingTest", LoadSceneMode.Single);
        }
    }
}
