using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    private Sprite targetSprite;
    private long initTime = 0;
    private CardShuffler shuffler;
    // Start is called before the first frame update
    public void Initialize(Sprite sprite, CardShuffler shuffler)
    {
        this.targetSprite = sprite;
        this.GetComponent<Image>().sprite = sprite;
        this.shuffler = shuffler;
        Invoke("cover", 1);
    }

    public void cover()
    {
        this.GetComponent<Image>().sprite = shuffler.back;

    }

    public void Hide()
    {
        this.GetComponent<Image>().sprite = null;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        GetComponent<Button>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClick()
    {
        if (!this.shuffler.IsPending())
        {
            this.GetComponent<Image>().sprite = targetSprite;
            this.shuffler.ReportCardUncover(gameObject);
        }
        

    }
}
