using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public List<Level> levels;
    private IEnumerator<Level> level_tracker;

    // Use this for initialization
    void Start()
    {
        level_tracker = levels.GetEnumerator();
        level_tracker.MoveNext();
    }

    // Update is called once per frame
    public void Update()
    {
        // Level Switch
        if (Time.time >= level_tracker.Current.length)
        {
            switchLevel();
        }
    }

    private void switchLevel()
    {
        level_tracker.Current.towers.SetActive(true);
        level_tracker.MoveNext();
    }

    [System.Serializable]
    public class Level
    {
        public float length;
        public GameObject towers;
    }
}