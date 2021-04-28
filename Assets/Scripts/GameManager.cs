using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int sceneToLoad;
    public bool readyToLoad;
    private float checkCD;

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
        sceneToLoad = v;
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
        sceneToLoad = 0;
        readyToLoad = false;
        DontDestroyOnLoad(this);
        checkCD = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkCD>0)
        {
            checkCD -= Time.deltaTime;
        }


        LoadGame();
        
    }

    private void LoadGame()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (readyToLoad == true)
            {
                SceneManager.LoadScene(sceneToLoad,LoadSceneMode.Single);
            }
        }

    }
}
