using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] hearts;

    private int lastFullIndex;

	// Use this for initialization
	void Start () {
        lastFullIndex = hearts.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MinusHearts(int number)
    {
        int endIndex = lastFullIndex - number;
        while (lastFullIndex > endIndex)
        {
            hearts[lastFullIndex].sprite = heartEmpty;
            lastFullIndex--;
        }
    }

    public void PlusHearts(int number)
    {
        int endIndex = lastFullIndex + number;
        while (lastFullIndex < endIndex)
        {
            hearts[lastFullIndex+1].sprite = heartFull;
            lastFullIndex++;
        }
    }
}
