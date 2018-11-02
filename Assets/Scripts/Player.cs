using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance = null; // singleton design pattern

    private int paperSupply; // current amount of paper


    public static Player Instance {
        get { return instance; }
    }


    // use for self init
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject); // player data will exist throughout game
        }
        else if (instance != this)
        {
            Destroy(gameObject); // destroy any attempts to create a duplicate
        }
    }

    public void addPaper(int paperGain)
    {
        paperSupply += paperGain;
    }

    public void removePaper(int paperLoss)
    {
        paperSupply -= paperLoss;
    }

}
