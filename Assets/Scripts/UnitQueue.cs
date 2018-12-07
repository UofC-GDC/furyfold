using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour
{
	public List<Image> queueElements = new List<Image>();
	[SerializeField] public List<UnitType> unitTypes = new List<UnitType>();
	[SerializeField] private int _paper = 0;

	[SerializeField] private List<int> _queue = new List<int>();

	[System.Serializable]
	public struct Plane
	{
		public Transform position;
		public float localXoff;
		public float localZoff;
	}

	public void Update(){
		// foreach (Image element in queueElements){
		// 	element.color = 
		// }

		var qeIterator = queueElements.GetEnumerator();
		qeIterator.GetNext();

		var qIterator = unitTypes.GetEnumerator();
		var image = qeIterator.Current;
		var unitType = qIterator.Current;
		for (; qeIterator.GetNext() && qIterator.GetNext();){
			image = qeIterator.GetCurrent;
			unitType = qIterator.Current;

			image.color = unitType.color;
		}
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
		public Color color;
	}
}
