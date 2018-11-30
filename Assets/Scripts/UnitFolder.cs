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
				var spawnPoint = Vector3.ProjectOnPlane(Random.OnUnitySphere + plane.centre, plane.normal);
				Instantiate(cur.UnitObject, spawnPoint, transform.rotation);
				cur = null;
			}
		}
	}
}
