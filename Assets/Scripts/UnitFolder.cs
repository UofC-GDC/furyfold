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
				UnitQueue.Plane plane = queue.spawnRange;
				var spawnPoint =  
					plane.position.position +
					plane.position.right * Random.Range(-plane.localXoff, plane.localXoff) +
					plane.position.forward * Random.Range(-plane.localZoff, plane.localZoff);
				Instantiate(cur.UnitObject, spawnPoint, transform.rotation);
				cur = null;
			}
		}
	}
}
