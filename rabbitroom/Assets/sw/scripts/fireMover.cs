using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fireMover : MonoBehaviour
{
	private Transform thisTransform = null;
	
	void Start ()
	{
		thisTransform = this.transform;
		//StartCoroutine(move());
	}
	
	/*void Update ()
	{

	}*/
	IEnumerator move()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			Vector3 pos = thisTransform.position;
			pos.x += 1.1f;
			thisTransform.position = pos;

		}
	}
}
