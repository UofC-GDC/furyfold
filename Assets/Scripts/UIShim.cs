using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShim : MonoBehaviour
{

	readonly char[] deleteKeys = new char[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };

	private UnitQueue queue;
	// Use this for initialization
	void Start()
	{
		queue = FindObjectOfType<UnitQueue>();
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 1; i < 10 && i <= queue.unitTypes.Count; i++)
			if (Input.GetKeyDown(i.ToString()))
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					queue.dequeue(i - 1);
				}
				else
				{
					queue.queue(i - 1);
				}
			}
	}
}
