using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class meShooter : MonoBehaviour
{
	private Transform thisTransform = null;
	private int m_muzzleCurrent = 0;

	public AudioSource shootAudioSource;
	public List<Transform> m_muzzles = new List<Transform>();

	public GameObject m_shotPrefab;

	void Start ()
	{
		thisTransform = this.transform;	
	}

	/*void Update ()
	{
	
	}*/

	public void doShoot()
	{
		shootAudioSource.Play();

		{
			Transform muzzle = m_muzzles[m_muzzleCurrent];
			GameObject go = GameObject.Instantiate(m_shotPrefab, muzzle.position, muzzle.rotation) as GameObject;
			go.transform.parent = muzzle;
			GameObject.Destroy(go, 3f/*seconds*/);
		}
		{
			m_muzzleCurrent++;
			if(m_muzzleCurrent >= m_muzzles.Count)
			{
				m_muzzleCurrent = 0;
			}
		}
	}
}
