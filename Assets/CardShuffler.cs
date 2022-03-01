using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShuffler : MonoBehaviour
{


    public Sprite[] sprites;
    public Sprite back;

    public GameObject boardObject;
    public GameObject cardPrefab;

    private int numberOfCards;

    //Source: https://stackoverflow.com/questions/273313/randomize-a-listt
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        numberOfCards = 4;
        generate2x4();
        
    }

    internal bool IsPending()
    {
        return pending;
    }

    private GameObject firstUncoveredCard, secondUncoveredCard;
    private bool pending = false;

    void result()
    {
        if (firstUncoveredCard.GetComponent<Image>().sprite == secondUncoveredCard.GetComponent<Image>().sprite)
        {
            firstUncoveredCard.GetComponent<CardScript>().Hide();
            secondUncoveredCard.GetComponent<CardScript>().Hide();
        }
        else
        {
            firstUncoveredCard.GetComponent<CardScript>().cover();
            secondUncoveredCard.GetComponent<CardScript>().cover();
        }
        firstUncoveredCard = null;
        secondUncoveredCard = null;
        pending = false;
    }

    public void ReportCardUncover(GameObject card)
    {
        if(firstUncoveredCard == null)
        {
            firstUncoveredCard = card;
        }
        else
        {
            secondUncoveredCard = card;
            pending = true;
            Invoke("result", 1);

        }
    }

    List<Sprite> generateRandomSetSprites(int length)
    {
        if(length % 2 == 1)
        {
            return null;
        }
        List<Sprite> genSprites = new List<Sprite>();
        for(int i = 0; i < length/2; i++)
        {
            var s = sprites[UnityEngine.Random.Range(0, sprites.Length)];
            while (genSprites.Contains(s))
            {
                
                s = sprites[UnityEngine.Random.Range(0, sprites.Length)];
            }
            genSprites.Add(s);
            genSprites.Add(s);
        }
        Shuffle(genSprites);
        return genSprites;
    }

    private void generateLoop(int xVal, int yVal, float startXOffset, float startYOffset, float separatorX, float separatorY)
    {
        var sprit = generateRandomSetSprites(xVal * yVal);
        for (int x = 0; x < xVal; x++)
        {
            for (int y = 0; y < yVal; y++)
            {
                var card = GameObject.Instantiate(cardPrefab);
                card.SetActive(true);
                card.transform.SetParent(boardObject.transform);
                card.GetComponent<RectTransform>().localPosition = new Vector2(startXOffset + x * separatorX, startYOffset + y * separatorY);
                card.GetComponent<CardScript>().Initialize(sprit[0], this);
                sprit.RemoveAt(0);
            }
        }
    }

    void generate2x2()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 2;
        boardObject.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);

        generateLoop(2, 2, -50, -50, 100, 100);
        /*
        var sprit = generateRandomSetSprites(4);
        if(sprit == null)
        {
            return;//TODO
        }
        float startXOffset = -50;
        float startYOffset = -50;
        float separatorX = 100;
        float separatorY = 100; 
        for(int x = 0; x < 2; x++)
        {
            for(int y = 0; y < 2; y++)
            {
                var card = Object.Instantiate(cardPrefab);
                card.SetActive(true);
                card.transform.SetParent(boardObject.transform);
                card.GetComponent<RectTransform>().localPosition = new Vector2(startXOffset + x * separatorX, startYOffset + y * separatorY);
                card.GetComponent<CardScript>().Initialize(sprit[0], this);
                sprit.RemoveAt(0);
            }
        }
        */
    }
    void generate2x4()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 4;
        boardObject.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
        generateLoop(4, 2, -150, -50, 100, 100);

    /*
        var sprit = generateRandomSetSprites(8);
        if(sprit == null)
        {
            return;//TODO
        }
        float startXOffset = -150;
        float startYOffset = -50;
        float separatorX = 100;
        float separatorY = 100; 
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 2; y++)
            {
                var card = Object.Instantiate(cardPrefab);
                card.SetActive(true);
                card.transform.SetParent(boardObject.transform);
                card.GetComponent<RectTransform>().localPosition = new Vector2(startXOffset + x * separatorX, startYOffset + y * separatorY);
                card.GetComponent<CardScript>().Initialize(sprit[0], this);
                sprit.RemoveAt(0);
            }
        }
        */
    }
    void generate4x4()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 4;
        generateLoop(4, 4, -175, -85, 110, 50);
 /*      var sprit = generateRandomSetSprites(16);
        if(sprit == null)
        {
            return;//TODO
        }
        float startXOffset = -175;
        float startYOffset = -85;
        float separatorX = 110;
        float separatorY = 50; 
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                var card = Object.Instantiate(cardPrefab);
                card.SetActive(true);
                card.transform.SetParent(boardObject.transform);
                card.GetComponent<RectTransform>().localPosition = new Vector2(startXOffset + x * separatorX, startYOffset + y * separatorY);
                card.GetComponent<CardScript>().Initialize(sprit[0], this);
                sprit.RemoveAt(0);
            }
        }
        */
        boardObject.GetComponent<RectTransform>().localScale = new Vector2(0.5f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
