using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardShuffler : MonoBehaviour
{


    public Sprite[] sprites;
    public Sprite back;

    public GameObject boardObject;
    public GameObject cardPrefab;
    public TextMeshProUGUI winText, scoreText;
    public GameObject endToHide;

    private List<GameObject> InternalCards = new List<GameObject>();
    private bool endless;

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

    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    internal bool IsPending()
    {
        return pending;
    }

    private GameObject firstUncoveredCard, secondUncoveredCard;
    private bool pending = true;

    public void AfterGameCleanup()
    {
        ClearCardObjects();
        Score = 0;
        pending = false;
        winText.gameObject.SetActive(false);
    }

    void Result()
    {
        if (firstUncoveredCard.GetComponent<Image>().sprite == secondUncoveredCard.GetComponent<Image>().sprite)
        {
            firstUncoveredCard.GetComponent<CardScript>().Hide();
            secondUncoveredCard.GetComponent<CardScript>().Hide();
            Score += 6;
        }
        else
        {
            firstUncoveredCard.GetComponent<CardScript>().cover();
            secondUncoveredCard.GetComponent<CardScript>().cover();
            Score -= 2;
        }
        firstUncoveredCard = null;
        secondUncoveredCard = null;
        pending = false;
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if(Score <= -6)
        {
            pending = true;
            winText.gameObject.SetActive(true);
            winText.text = "GAME OVER";
            winText.color = Color.red;
            //YOU LOST!
        }
        if(CheckIfBoardIsSolved())
        {

            //Game Won
            pending = true;
            winText.gameObject.SetActive(true);
            winText.text = "You won!";
            winText.color = Color.blue;
            endToHide.SetActive(false);
            
        }
        else
        {
            //GAME CONTINUES
        }
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
            Invoke("Result", 1);

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
        
    private void NoPending()
    {
        this.pending = false;
    }

    private void ClearCardObjects()
    {
        foreach(var go in InternalCards)
        {
            Destroy(go);
        }
        InternalCards.Clear();
    }

    private void generateLoop(int xVal, int yVal, float startXOffset, float startYOffset, float separatorX, float separatorY)
    {
       // boardObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 8, Screen.height / 4);
       // boardObject.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 10, Screen.height / 12);

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
                InternalCards.Add(card);
            }
        }
        Invoke("NoPending", 1);
    }

    internal void Generate2x2()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 2;
        

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
    internal void Generate2x4()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 4;
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
    internal void Generate4x4()
    {
        boardObject.GetComponent<GridLayoutGroup>().constraintCount = 4;
        boardObject.GetComponent<GridLayoutGroup>().spacing = new Vector2(10, 10);
        boardObject.GetComponent<GridLayoutGroup>().padding.bottom = 0;
        generateLoop(4, 4, -175, -85, 110, 50);
      //  boardObject.GetComponent<RectTransform>().localScale = new Vector2(0.5f, 0.5f);

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

    }

    
    public void StartGame(int type)
    {
        winText.gameObject.SetActive(false);
        endToHide.SetActive(true);
        this.Score = 0;
        if (type == 0)
        {
            Generate2x2();
        }
        else if(type == 1)
        {
            Generate2x4();
        }
        else
        {
            Generate4x4();
        }
    }

    private bool CheckIfBoardIsSolved()
    {
        foreach(var go in InternalCards)
        {
            if(!go.GetComponent<CardScript>().IsSolved())
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Score;
    }
}
