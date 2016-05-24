using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class sw2_msg : MonoBehaviour
{
	public Texture2D tCoin;
	//public RawImage rawiCoin;
	public GUISkin guiSkin;

	private int m_livingDestroyables = 0;
	private int m_allDestroyables = 0;
	private bool m_bWon = false;

	void Start ()
	{
		StartCoroutine(countDestroyables());
		playWonSound();
	}

	IEnumerator countDestroyables()
	{
		while(true)
		{
			BoxCollider[] destroyables = GameObject.FindObjectsOfType<BoxCollider>();
			int iDestroyables = 0;
			{
				for(int i=0;i<destroyables.Length;i++)
				{
					if(destroyables[i].tag == "shot")continue;
					iDestroyables++;
				}
			}
			{
				if (this.m_allDestroyables < 1)
					this.m_allDestroyables = iDestroyables;
				this.m_livingDestroyables = iDestroyables;
			}
			yield return new WaitForSeconds(1.5f);
		}
	}
	
	/*void Update ()
	{
	
	}*/

	void OnGUI ()
	{
		if(this.guiSkin)
		{
			GUI.skin = this.guiSkin;
		}

		Rect r = new Rect(3,3,Screen.width, Screen.height);
		GUILayout.BeginArea(r);

		if(this.m_allDestroyables > 0)
		{
			bool bWon = (this.m_livingDestroyables < 1);

			if(bWon && !m_bWon)
			{
				m_bWon = true;
				playWonSound();
			}

			String msg = bWon ?
				" ALL "+this.m_allDestroyables+" items destroyed! " :
				"  "+(this.m_allDestroyables - this.m_livingDestroyables)+" of "+this.m_allDestroyables+" items destroyed! ";

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(msg);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			ImagesForInteger(this.m_allDestroyables - this.m_livingDestroyables);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	}
	private void ImagesForInteger(int total)
	{
		for(int i=0; i < total; i++)
		{
			GUILayout.Label(tCoin);
		}
	}

	private void playWonSound()
	{
		this.GetComponent<AudioSource>().Play();
	}
}
