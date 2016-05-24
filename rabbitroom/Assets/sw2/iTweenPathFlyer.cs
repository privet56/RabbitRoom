using UnityEngine;
using System.Collections;

public class iTweenPathFlyer : MonoBehaviour
{
	public float camFlyTime = 11f;

	void Start ()
	{
		Vector3[] path = iTweenPath.GetPath("cam2path");
		GUITween.MoveTo(gameObject, GUITween.Hash("path", path , "time", camFlyTime, "easetype", GUITween.EaseType.easeInOutSine));
		//GUITween.RotateBy(gameObject, GUITween.Hash("y", 1.0, "easeType", GUITween.EaseType.easeInOutSine, "loopType", "pingPong", "delay", .0));
		GUITween.RotateBy(gameObject, GUITween.Hash("y", -0.5/*1 = rot east*/, "time", camFlyTime, "easeType", GUITween.EaseType.easeInOutSine));

		StartCoroutine(deActivateMe());
	}

	IEnumerator deActivateMe()
	{
		yield return new WaitForSeconds(camFlyTime);
		this.gameObject.SetActive(false);
	}

	void Update ()
	{
	
	}
}
