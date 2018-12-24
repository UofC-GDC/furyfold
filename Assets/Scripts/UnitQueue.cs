using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour
{
	public List<Image> queueElements = new List<Image>();
	[SerializeField] public List<UnitType> unitTypes = new List<UnitType>();
	[SerializeField] private int _paper = 0;

	[SerializeField] public List<int> _queue = new List<int>();

    public Text paperText;

	[System.Serializable]
	public struct Plane
	{
		public Transform position;
		public float localXoff;
		public float localZoff;
	}

	//Colour queue
	public void Update()
    {
		var qeIterator = queueElements.GetEnumerator();
		qeIterator.MoveNext();

		var qIterator = unitTypes.GetEnumerator();
		var image = qeIterator.Current;
		var unitType = qIterator.Current;

        for (int i = 0; i < queueElements.Count; i++)
        {
            Image queueElementImage = queueElements[i];
            Color color;
            try
            {
                color = unitTypes[_queue[i]].color;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                color = new Color(0, 0, 0, 0);
            }
            queueElementImage.color = color;
        }

        //for (; qeIterator.MoveNext() && qIterator.MoveNext();){
        //	image = qeIterator.Current;
        //	unitType = qIterator.Current;

        //	image.color = unitType.color;
        //}
        //for(; qeIterator.MoveNext();){
        //	Color transparent = new Color(0,0,0,0);
        //	qeIterator.Current.color = transparent;
        //}

        paperText.text = paper.ToString();

        //foreach (UnitType u in q) Console.Write(c + " ");
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


    void OnDrawGizmos()
    {
        Gizmos.DrawCube(spawnRange.position.position, new Vector3(spawnRange.localXoff, spawnRange.localZoff, 1f));
    }
}
