using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour
{
	[SerializeField] public List<UnitType> unitTypes = new List<UnitType>();
	[SerializeField] private int _paper = 0;

	[SerializeField] private List<int> _queue = new List<int>();

	[System.Serializable]
	public struct Plane
	{
		public Vector3 centre;
		public Vector3 normal;
	}

	public Plane spawnRange;

	public int paper
	{
		get
		{
			return _paper;
		}
	}

	public void queue(int i)
	{
		_queue.Add(i);
	}

	public void dequeue(int i)
	{
		_queue.Remove(i);
	}

	public void addPaper(int n)
	{
		_paper += n;
	}

	public UnitType getNext()
	{
		for (var i = 0; i < _queue.Count; i++)
		{
			int t = _queue[i];
			if (unitTypes[_queue[i]].cost <= _paper)
			{
				_queue.RemoveAt(i);
				_paper -= unitTypes[t].cost;
				return unitTypes[t];
			}
		}
		return null;
	}

	[System.Serializable]
	public class UnitType
	{
		public GameObject UnitObject;
		public int cost;
		public float time;
		public Image icon;
	}
}
