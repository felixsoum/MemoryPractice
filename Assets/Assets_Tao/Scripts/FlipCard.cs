using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCard : MonoBehaviour
{
    public Image parentImage;
    public Button parentButton;

    public Image cardBack;
    public GameObject cardFront;
    public GameObject btn;

    public int AnimSpeed = 2;

    public bool cardBackIsActive;

    public int counter;

    public float timer;

    public bool bRunOnce;

    [SerializeField] private bool isRotateFinished;

    private void Start()
    {
        bRunOnce = true;
        cardBackIsActive = false;
        isRotateFinished = true;
        timer = 3f;
    }

    private void Update()
    {
        if(!isRotateFinished)
        {
            CardReset();
        }
    }

    /// <summary>
    /// Only Allow Player click card once
    /// </summary>
    public void StartFlip()
    {
        //avoid same card 
        if(!CardManager.activeCard.Contains(this))
        {
            CardManager.cardCount++;
            CardManager.activeCard.Add(this);
        }
        //Disable Button
        btn.GetComponent<Button>().enabled= false;
        if (isRotateFinished)
        {
            StartCoroutine(CalculateFlip());
            isRotateFinished = false;
        }
    }

    public void Flip()
    {
        if (cardBackIsActive == true)
        {
            cardFront.SetActive(true);
            cardBack.enabled = false;
            cardBackIsActive = false;
        }
        else
        {
            cardBack.enabled = true;
            cardFront.SetActive(false);
            cardBackIsActive = true;
        }
    }

    /// <summary>
    /// After 3 sec, card automatically flip
    /// </summary>
    public void CardReset()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            //Only run it once in Update()
            if (bRunOnce)
            {
                bRunOnce = false;
                StartCoroutine(CalculateFlip());
                isRotateFinished = true;//Disable rotation
            }
        }
    }

    public void TurnCard(bool turncard)
    {
        isRotateFinished = turncard;
    }


    IEnumerator CalculateFlip()
    {
        timer = 3f;
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.01f / AnimSpeed);
            transform.Rotate(Vector3.up);//each time add 1 degree
            counter++;
            if (counter == 90 || counter == -90)
            {
                Flip();
            }
        }
        bRunOnce = true;
        counter = 0;
        //Enable button
        //btn.GetComponent<Button>().enabled = true;
    }
}
