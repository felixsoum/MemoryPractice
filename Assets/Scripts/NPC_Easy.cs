using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Easy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int gameMode; // number for the scene to load
    [SerializeField] private GameObject popUp; // pop up dialog when player is close


    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    
    }

    // Update is called once per frame
    void Update()
    {
        float disctance = Vector3.Distance(player.transform.position, transform.position);//check distance between player and npc
  

        //Debug.Log(disctance);
        if (disctance <= 2.5f)//player is close to NPC
        {
            popUp.SetActive(true);
            gm.SetSceneToLoad(gameMode);
            gm.CanLoad(true);
            gm.CloseToNPC(); //set the delay for the npc checking to avoid conflict between each npc


        }
        else if(gm.FarToNPC())//player is far to NPC 
        {
            popUp.SetActive(false);
            gm.SetSceneToLoad(0);
            gm.CanLoad(false);
        }
    }
}
