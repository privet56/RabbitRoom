using System;
using System.Collections;
using UnityEngine;

public class explode : MonoBehaviour
{
	public Transform explosionPrefab;
	private bool m_Exploded;

	void Start ()
	{
		StartCoroutine(simulate());
	}
	public void doexplode()
	{
		return;	//TODO! transform missing?
		print("doexplode");
		GameObject go = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation) as GameObject;
		go.transform.parent = this.transform;	//=.setParent(parent);
		go.SetActive(true);
		go.transform.localScale = new Vector3(1f, 1f, 1f);	//set scale AFTER setting parent!
		m_Exploded = true;
	}

	IEnumerator simulate()
	{
		yield return new WaitForSeconds(2.5f);
		doexplode();
	}
}
