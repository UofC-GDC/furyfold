using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInCircle : MonoBehaviour
{
    [Range(0,60f)]
    [SerializeField] private float spinSpeed = 1f;

    [SerializeField] private bool reverse = false;

	void Update ()
    {
        var spin = reverse ? spinSpeed * -1f : spinSpeed;
        transform.Rotate(Vector3.up, spin * Time.deltaTime, Space.Self);
	}
}
