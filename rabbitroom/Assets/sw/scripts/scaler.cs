using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scaler : MonoBehaviour
{
	public float scaleSpeed = 1.1f;
	public float scaleMax = 1.3f;
	public float scaleMin = 0.7f;

	private bool scaleUP = true;
	private Transform thisTransform = null;

	void Start ()
	{
		thisTransform = this.transform;
		StartCoroutine(scale());
	}
	
	/*void Update ()
	{
	
	}*/
	IEnumerator scale()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);

			Vector3 scale = thisTransform.localScale;
			if (scaleUP && (scale.y >= scaleMax))
				scaleUP = false;
			if(!scaleUP && (scale.y <= scaleMin))
				scaleUP = true;

			scale = new Vector3(scaleUP ? scale.x+scaleSpeed : scale.x-scaleSpeed,
			                    scaleUP ? scale.y+scaleSpeed : scale.y-scaleSpeed,
			                    scaleUP ? scale.z+scaleSpeed : scale.z-scaleSpeed);

			thisTransform.localScale = scale;
			//thisTransform.Rotate(0, 1.1f, 0);
		}
	}
}
