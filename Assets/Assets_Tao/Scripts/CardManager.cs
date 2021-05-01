using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    /// <summary>
    /// easy(8 cards), medium(16 cards), hard(32 cards)
    /// </summary>

    public static int gameMode;
    public GameObject[] card;
    public GameObject canvas;
    public Transform pos;//image position
    public Sprite[] cardBackSprite1;
    public Sprite[] cardBackSprite2;
    public Sprite[] cardBackSprite3;
    public bool flag;
    public float timer = 4.0f;
    public static int cardCount;
    public GameObject[] levels;

    private float checkCD;
    [SerializeField] private List<FlipCard> cardList = new List<FlipCard>();
    private List<int> useIndex = new List<int>();
    private List<int> rawIndex = new List<int>();
    public static List<FlipCard> activeCard = new List<FlipCard>();


    // Start is called before the first frame update
    void Start()
    {
        gameMode = 2;
        CreateLevel(gameMode);
        Debug.Log($"Current game mode is: {gameMode}");

        flag = false;
        AssignCardSprite();
        foreach (var c in card)
        {
            cardList.Add(c.GetComponentInChildren<FlipCard>());
        }
        LevelCardSprite();
        checkCD = 0;
    }

    private void Update()
    {
        if(checkCD > 0)
        {
            checkCD -= Time.deltaTime;
        }
        onlyTwoCardAllowed();
        DeleteCard();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("WalkingTest");
        }
    }

    void CreateLevel(int gameMode)
    {
        GameObject go = Instantiate(levels[gameMode - 1]);
        card = new GameObject[go.transform.childCount];

        go.transform.parent = canvas.transform;
        go.transform.position = pos.position;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            card[i] = go.transform.GetChild(i).gameObject;
        }
       
    }

    void DeleteCard()
    {
        if (flag && checkCD <= 0)
        {
            if (activeCard[0].cardBack.sprite == activeCard[1].cardBack.sprite)
            {
                if (timer < 1)
                {
                    checkCD = 1.1f;
                    activeCard[0].StayTurned();
                    activeCard[1].StayTurned();
                    activeCard[0].parentButton.enabled = false;
                    activeCard[1].parentButton.enabled = false;
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
                cardList[i].parentButton.enabled = false;
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
                cardList[i].parentButton.enabled = true;
            }
        }
    }

    void AssignCardSprite()
    {
        //assign cardBack
        for (int i = 0; i < card.Length; i++)
        {
            rawIndex.Add(i);
        }
    }

    void RandomCardBack(Sprite[] cardBackSprite)
    {
        for (int i = 0; i < card.Length; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            useIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
            cardList[i].cardBack.sprite = cardBackSprite[useIndex[i]];
        }
    }

    void LevelCardSprite()
    {
        switch (gameMode)
        {
            case 1:
                RandomCardBack(cardBackSprite1);
                break;
            case 2:
                RandomCardBack(cardBackSprite2);
                break;
            case 3:
                RandomCardBack(cardBackSprite3);
                break;
            default:
                break;
        }
    }
}
