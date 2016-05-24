using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamSwitcher : MonoBehaviour
{
	public GameObject camera;
	public Transform cameraParent2Set = null;

	private bool canCamSwitch = true;

	private Transform cameraParentOriginal = null;
	private Vector3 cameraTransformPosOriginal = Vector3.zero;

	void Start ()
	{
		if(cameraParent2Set)
		{
#if MOBILE_INPUT
			cameraParent2Set.transform.localPosition = new Vector3(0.0f, 50.0f, -90.0f);
#endif
			//this.transform.position = cameraParent2Set.transform.position;
		}
	}
	
	void Update ()
	{
		if(!canCamSwitch)return;

		if (Input.GetKeyDown(KeyCode.F))
		{
			doSwitch();
		}
	}

	public void doSwitch()
	{
		if(!canCamSwitch)return;

		if(cameraParentOriginal == null)
		{
			cameraParentOriginal 		= camera.transform.parent;
			cameraTransformPosOriginal 	= camera.transform.position;
			camera.transform.parent 	= cameraParent2Set == null ? this.transform : cameraParent2Set;
			camera.transform.localPosition = camera.transform.position 	= Vector3.zero;
		}
		else
		{
			camera.transform.parent 	= cameraParentOriginal;
			camera.transform.position	= cameraTransformPosOriginal;
			cameraParentOriginal 		= null;
		}

		canCamSwitch = false;
		StartCoroutine(setCanCamSwitch(true));
	}

	IEnumerator setCanCamSwitch(bool _canCamSwitch)
	{
		yield return new WaitForSeconds(0.9f);
		this.canCamSwitch = _canCamSwitch;

	}
}
