using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class flythrough : MonoBehaviour
{
	public List<GameObject> m_flyThroughGOs;

	void Start ()
	{
		//Vector3[] path = iTweenPath.GetPath("Camera Fly");
		Vector3[] path = new Vector3[m_flyThroughGOs.Count];	//TODO: use linq
		for(int i=0;i<m_flyThroughGOs.Count; i++)
		{
			path[i] = m_flyThroughGOs[i].transform.position;
			//path[i].z -= 60f;
		}

		GUITween.MoveTo(gameObject, GUITween.Hash("path", path , "time", 4f, "easetype", GUITween.EaseType.easeInOutSine));
		/*
		LTSpline spline = new LTSpline( path );
		LeanTween.moveSplineLocal( this.gameObject, path, 9.0f ).setEase(LeanTweenType.easeInQuad).setOrientToPath(true).setRepeat(0);
		*/
	}
	
	void Update ()
	{
	
	}
}
