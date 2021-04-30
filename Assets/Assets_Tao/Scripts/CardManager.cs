using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    /// <summary>
    /// easy(8 cards), medium(16 cards), hard(32 cards)
    /// </summary>
 
    public GameObject []card;
    public Sprite[] cardBackSprite;
    public bool flag;
    public float timer = 4.0f;
    public static int cardCount;

    private float checkCD;
    private GameObject[] cardBack;
    [SerializeField] private List<GameObject> cardList = new List<GameObject>();
    private List<int> useIndex = new List<int>();
    private List<int> rawIndex = new List<int>();
    public static List<GameObject> activeCard = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        AssignCardSprite();
        RandomCardBack();
        checkCD = 0;
        foreach (var c in card)
        {
            cardList.Add(c.transform.GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        if(checkCD > 0)
        {
            checkCD -= Time.deltaTime;
        }
        onlyTwoCardAllowed();
        DeleteCard();
       
    }
    
    void DeleteCard()
    {
        if (flag && checkCD <= 0)
        {
            if (activeCard[0].transform.GetChild(1).GetComponent<Image>().sprite.name.ToString()
           == activeCard[1].transform.GetChild(1).GetComponent<Image>().sprite.name.ToString())
            {
                if (timer < 1)
                {
                    checkCD = 1.1f;
                    activeCard[0].GetComponent<FlipCard>().StayTurned();
                    activeCard[1].GetComponent<FlipCard>().StayTurned();
                    activeCard[0].transform.parent.GetComponent<Button>().enabled = false;
                    activeCard[1].transform.parent.GetComponent<Button>().enabled = false;
                    foreach (var item in activeCard)
                    {
                        if(cardList.Contains(item))
                        {
                            cardList.Remove(item);
                        }
                    }
                    flag = false;
                    activeCard.Clear();
                }
            }
        }
    }

    void onlyTwoCardAllowed()
    {
        if (cardCount == 2)
        {
            flag = true;
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i].transform.parent.GetComponent<Button>().enabled = false;
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            cardCount = 0;
            flag = false;
            activeCard.Clear();
        }

        if (cardCount == 0)
        {
            timer = 4.0f;
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i].transform.parent.GetComponent<Button>().enabled = true;
            }
        }
    }

    void AssignCardSprite()
    {
        cardBack = new GameObject[card.Length];
        //assign cardBack
        for (int i = 0; i < card.Length; i++)
        {
            rawIndex.Add(i);
            cardBack[i] = card[i].transform.GetChild(0).GetChild(1).gameObject;
        }
    }

    void RandomCardBack()
    {
        for (int i = 0; i < card.Length; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            useIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
            cardBack[i].GetComponent<Image>().sprite = cardBackSprite[useIndex[i]];
        }
    }
}
