using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public UnitQueue queuer;
    float gameoverTime;
    public Text gameoverText;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if (queuer.paper<10 && GameObject.FindGameObjectsWithTag("Cat").Length == 0)
        {
            print("GAMEOVER");
            if (!gameoverText.enabled)
                gameoverTime = Time.time;
            gameoverText.enabled = true;
            if (Time.time >= gameoverTime + 10)
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
	}
}
