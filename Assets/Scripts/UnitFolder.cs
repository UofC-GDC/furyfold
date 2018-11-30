using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFolder : MonoBehaviour
{

	private UnitQueue queue;

	private UnitQueue.UnitType cur = null;

	private double startTime;

	// Use this for initialization
	void Start()
	{
		queue = FindObjectOfType<UnitQueue>();
	}

	// Update is called once per frame
	void Update()
	{
		if (cur == null)
		{
			cur = queue.getNext();
			startTime = Time.time;
		}
		else
		{
			if (Time.time - startTime >= cur.time)
			{
				Instantiate(cur.UnitObject, transform.position, transform.rotation);
				cur = null;
			}
		}
	}
}
