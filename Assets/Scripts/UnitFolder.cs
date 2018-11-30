using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFolder : MonoBehaviour
{

	public UnitQueue queue;

	private UnitQueue.UnitType cur = null;

	private double startTime;

	// Use this for initialization
	void Start()
	{

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
				UnitQueue.Plane plane = ref queue.spawnRange;
				var spawnPoint = new Vector3(
					plane.left + Random.Range(-plane.localXOff, plane.localXoff), 
					plane.up, 
					plane.forward + Random.Range(-plane.localZoff, plane.localZoff));
				Instantiate(cur.UnitObject, spawnPoint, transform.rotation);
				cur = null;
			}
		}
	}
}
