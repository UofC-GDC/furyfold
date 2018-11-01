using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance = null; // singleton design pattern

    private int paperSupply; // current amount of paper

    // use for self init
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject); // destroy any attempts to create a duplicate
        }

        DontDestroyOnLoad(gameObject); // player data will exist throughout game

    }

    // use for init from other scripts
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addPaper(int extraPaper)
    {
        paperSupply += extraPaper;
    }

    public void removePaper(int lostPaper)
    {
        paperSupply -= lostPaper;
    }

}
