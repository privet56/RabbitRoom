using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CamSwitcherAnimated : MonoBehaviour
{
	public float speed = 3.9f;
	public GameObject camera;
	public Transform cameraParentNew = null;
	public Vector3 fly2localPosition = new Vector3(0, 122, -300);

	private bool isSwitching = false;
	private Transform cameraParentOriginal = null;
	private Vector3 cameraPositionOriginal = Vector3.zero;

	void Start ()
	{
		cameraParentOriginal = camera.transform.parent;
		cameraPositionOriginal = camera.transform.position;

#if MOBILE_INPUT
		//fly2localPosition = new Vector3(0, fly2localPosition.y / 2.0f, fly2localPosition.z / 2.0f);
		//CamWithinXWing working on pc: 0, -254, 225
		CamSwitcher camSwitcher = this.gameObject.AddComponent<CamSwitcher>();
		camSwitcher.camera = this.camera;
		camSwitcher.cameraParent2Set = cameraParentNew;
#endif
	}

	public void doSwitch()
	{
#if MOBILE_INPUT
		CamSwitcher camSwitcher = this.gameObject.GetComponent<CamSwitcher>();
		camSwitcher.doSwitch();
		StartCoroutine(util.unfocus());
		return;
#endif
		isSwitching = true;
		StartCoroutine(util.unfocus());
	}
#if !MOBILE_INPUT
	void Update ()
	{
		if(isSwitching)
		{
			doFly2();
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				doSwitch();
			}
		}
	}
	private void doFly2()
	{
		if(!isSwitching)return;

		float step = speed * Time.deltaTime;
		camera.transform.localPosition = Vector3.MoveTowards(camera.transform.localPosition, fly2localPosition, step);
		//print("I'm flying campos:"+camera.transform.position.x+"x"+camera.transform.position.y+"x"+camera.transform.position.z);

		if(camera.transform.localPosition.Equals(fly2localPosition))
		{
			isSwitching = false;
			bool fly2New = (camera.transform.parent == cameraParentOriginal);
			camera.transform.parent = (camera.transform.parent == cameraParentOriginal) ? cameraParentNew : cameraParentOriginal;
			camera.transform.localPosition = Vector3.zero;
			camera.transform.rotation = camera.transform.localRotation = Quaternion.identity;
			//print("I was flying to "+(fly2New ? "NEW":"BACK")+": campos:"+camera.transform.position.x+"x"+camera.transform.position.y+"x"+camera.transform.position.z);
			fly2localPosition = new Vector3(-fly2localPosition.x, -fly2localPosition.y, -fly2localPosition.z);
		}
	}
#endif
}
