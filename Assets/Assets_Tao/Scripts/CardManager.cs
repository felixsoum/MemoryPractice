using System;
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
    public static int score;
    public GameObject[] card;
    public GameObject canvas;
    public Transform pos;//image position
    public Sprite[] cardBackSprite1;
    public Sprite[] cardBackSprite2;
    public Sprite[] cardBackSprite3;
    public bool flag;
    public float timer = 4f;
    public static int cardCount;
    public GameObject[] levels;
    public GameObject winPannel;
    public Text scoreText;

    private GameObject levelGo;
    private GameManager gm;
    private float checkCD;
    [SerializeField] private List<FlipCard> cardList = new List<FlipCard>();
    private List<int> useIndex = new List<int>();
    private List<int> rawIndex = new List<int>();
    public static List<FlipCard> activeCard = new List<FlipCard>();

    [SerializeField] ParticleSystem matchParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = 1;
        gm = GameManager.instance;
        winPannel.SetActive(false);
        levelGo = CreateLevel(gameMode);
        levelGo.transform.localScale = 0.5f * Vector3.one;
        AssignCardSprite();
        foreach (var c in card)
        {
            cardList.Add(c.GetComponentInChildren<FlipCard>());
        }
        LevelCardSprite();
        checkCD = 0;
        flag = true;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (checkCD > 0)
        {
            checkCD -= Time.deltaTime;
        }
        Cheat();
        onlyTwoCardAllowed();
        DeleteCard();

        if (cardList.Count == 0)
        {
            StartCoroutine(FinishLevel());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("WalkingTest");
        }
    }

    GameObject CreateLevel(int gameMode)
    {
        GameObject go = Instantiate(levels[gameMode - 1]);
        card = new GameObject[go.transform.childCount];
        go.transform.parent = canvas.transform;
        go.transform.position = pos.position;
        //assign prefab
        for (int i = 0; i < go.transform.childCount; i++)
        {
            card[i] = go.transform.GetChild(i).gameObject;
        }
        return go;
    }

    public void DeleteCard()
    {
        if (checkCD <= 0 && activeCard.Count == 2)
        {
            if (activeCard[0].cardBack.sprite == activeCard[1].cardBack.sprite)
            {
                Invoke(nameof(ShowMatchEffect), 1.5f);
                checkCD = 1.1f;
                CardTurn(true);
                activeCard[0].parentButton.enabled = false;
                activeCard[1].parentButton.enabled = false;
                foreach (var item in activeCard)
                {
                    if (cardList.Contains(item))
                    {
                        cardList.Remove(item);
                        StartCoroutine(NumberChange());
                    }
                }
                timer = 0;//make player can select new card instantly after find 2 same cards
                activeCard.Clear();
            }
            else
            {
                checkCD = 1.1f;
                CardTurn(false);
                activeCard[0].parentButton.enabled = false;
                activeCard[1].parentButton.enabled = false;
                activeCard.Clear();
            }
        }
    }

    public void ShowMatchEffect()
    {
        matchParticleSystem.Play();
    }

    void onlyTwoCardAllowed()
    {
        //Stop time count when 1 card is turned
        if (flag)
        {
            timer -= Time.deltaTime;
        }
        
        //If only 1 card flips, stay turned
        if (cardCount == 1)
        {
            flag = false;
            activeCard[0].TurnCard(true);
        }

        //Disable all buttons when 2 cards flip
        if (cardCount == 2)
        {
            flag = true;
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i].parentButton.enabled = false;
            }
        }

        //Reset activeCard List
        if (timer <= 0f)
        {
            if(cardCount >= 2)
            {
                cardCount = 0;
                CardTurn(false);
                activeCard.Clear();
            }
        }

        //Enable all buttons except removed cards
        if (cardCount == 0)
        {
            timer = 4.0f;
            for (int i = 0; i < cardList.Count; i++)
            {
                if (cardList[i].parentButton != null)
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
            int tempIndex = UnityEngine.Random.Range(0, rawIndex.Count);
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
    public void Win()
    {
        gm.NextLevel();
    }

    IEnumerator NumberChange()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            score += 1;
        }
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1f);
        winPannel.SetActive(true);
        Destroy(levelGo);
    }

    public void Cheat()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            winPannel.SetActive(true);
            Destroy(levelGo);
        }
    }

    public void CardTurn(bool bTurn)
    {
        if (activeCard.Count > 0)
        {
            activeCard[0].TurnCard(bTurn);
        }

        if (activeCard.Count > 1)
        {
            activeCard[1].TurnCard(bTurn);
        }
    }
}
