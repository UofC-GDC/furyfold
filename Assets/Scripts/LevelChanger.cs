using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public List<Level> levels;
    private IEnumerator<Level> level_tracker;
    public Text winText;
    float winTextTime;

    // Use this for initialization
    void Start()
    {
        level_tracker = levels.GetEnumerator();
        level_tracker.MoveNext();
    }

    // Update is called once per frame
    public void Update()
    {
        if (level_tracker.Current == null)
            if (GameObject.FindGameObjectsWithTag("Tower").Length == 0)
            {
                if (!winText.enabled)
                    winTextTime = Time.time;
                winText.enabled = true;
                if (Time.time >= winTextTime + 10)
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            else
                return;
        // Level Switch
        if (Time.timeSinceLevelLoad >= level_tracker.Current.length)
        {
            switchLevel();
        }
    }

    private void switchLevel()
    {
        level_tracker.MoveNext();
        level_tracker.Current.towers.SetActive(true);
    }

    [System.Serializable]
    public class Level
    {
        public float length;
        public GameObject towers;
    }
}