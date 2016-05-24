using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class util : MonoBehaviour
{
	void Start ()
	{
	
	}
	
	/*void Update ()
	{
	}*/

	public static float getDistance(Transform objectTransform, Camera camera)
	{
		if (camera == null)
			camera = Camera.main;

		Vector3 heading = objectTransform.position - camera.transform.position;
		float distance = Vector3.Dot(heading, camera.transform.forward);
		return distance;
	}
	public static Vector3 inputPosition()
	{
		#if !MOBILE_INPUT
		return Input.mousePosition;
		#endif

		if (Input.touchCount < 1)
		{
			return new Vector3(0.0f,0.0f,0.0f);
		}
		Touch touch = Input.touches[0];
		return new Vector3(touch.position.x, touch.position.y, 0.0f);
	}
	public static void BroadcastAll(string fun, string msg)
	{
		GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in gos)
		{
			if (go && go.transform.parent == null)
			{
				go.gameObject.BroadcastMessage(fun, msg, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	public static void setRootMotion(Animator a)
	{
		if(a == null)return;
		#if !MOBILE_INPUT
		a.applyRootMotion = false;
		#else
		a.applyRootMotion = true;
		#endif
	}

	public static IEnumerator unfocus()
	{
		yield return new WaitForSeconds(0.1f);
		GUI.UnfocusWindow();
		EventSystem.current.SetSelectedGameObject (Camera.main.gameObject);
	}
	public static String asString(float v, int afterComma)
	{
		return v.ToString("n"+afterComma);
	}
}
