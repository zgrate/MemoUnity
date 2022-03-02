using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelector : MonoBehaviour
{

    public CardShuffler cardShuffler;
    public Toggle toggle;
    public GameObject[] objectToHide;
    public GameObject screen;

    private void HideAll()
    {
        foreach(var go in objectToHide)
        {
            go.SetActive(false);
        }
    }

    private void EnableAll()
    {
        foreach (var go in objectToHide)
        {
            go.SetActive(true);
        }
    }

    public void gamemodeSelect2x2()
    {
        EnableAll();
        screen.SetActive(false);
        cardShuffler.StartGame(0);
    }

    public void gamemodeSelect2x4()
    {
        EnableAll();
        screen.SetActive(false);
        cardShuffler.StartGame(1);
    }

    public void gamemodeSelect4x4()
    {
        EnableAll();
        screen.SetActive(false);
        cardShuffler.StartGame( 2);
    }

    public void GoBackToMenu()
    {
        cardShuffler.AfterGameCleanup();
        HideAll();
        screen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideAll();
        screen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
