using UnityEngine;
using System.Collections;

public class xwinflytotarget : MonoBehaviour
{
	public Transform target;
	public float speed = 9.9f;

	private Transform thisTransform;
	private bool xwingWon = false;

	void Start ()
	{
		thisTransform = this.transform;
		//StartCoroutine(simulate());	//T.ODO: comment line aout
	}
	
	void Update ()
	{
		if(thisTransform == null)return;
		//fly towards
		float step = speed * Time.deltaTime;
		thisTransform.position = Vector3.MoveTowards(thisTransform.position, target.position, step);

		if(!xwingWon && (thisTransform.position.z >= (target.position.z - 1.1f)))
		{
			xwingWon = true;
			util.BroadcastAll("onWon", "true");
		}
	}

	public void onDied(string isDied)
	{
		thisTransform = null;
	}

	IEnumerator simulate()
	{
		yield return new WaitForSeconds(3.5f);
		xwingWon = true;
		util.BroadcastAll("onWon", "true");
	}
}
