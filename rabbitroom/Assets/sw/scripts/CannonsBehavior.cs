using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Aeroplane;

public class CannonsBehavior : CannonBehavior
{
	public Rigidbody m_Rigidbody;
	public AeroplaneController aeroplaneController;
	public List<Transform> m_muzzles = new List<Transform>();

	private int m_muzzleCurrent = 0;
	private float maxX = 699.9f;

	public override void Start () 
	{
		this.m_muzzle = m_muzzles[m_muzzleCurrent];
		base.Start();
	}
	
	public override void Update () 
	{
		if(m_muzzles == null)return;
		base.Update();
	}

	public override void onShotDid(GameObject shot)
	{
		if(m_muzzles == null)return;

		shot.transform.parent = this.transform;	//=shot.setParent(me);

		m_muzzleCurrent++;
		if(m_muzzleCurrent >= m_muzzles.Count)
		{
			m_muzzleCurrent = 0;
		}
		this.m_muzzle = m_muzzles[m_muzzleCurrent];
		this.GetComponent<AudioSource>().Play();
	}

	public override void onCannonRot(Transform cannonRot, float rot)
	{
		if(m_muzzles == null)return;//

		if(Math.Abs(rot) < 0.001f)
		{
			Vector3 pos = m_Rigidbody.transform.position;
			if(Math.Abs(pos.x) > 0.001f)
			{
				if( pos.x > 0.001f)
					pos.x -= Time.deltaTime * (horizontalMoveSpeed/2);
				if( pos.x < -0.001f)
					pos.x += Time.deltaTime * (horizontalMoveSpeed/2);
				m_Rigidbody.transform.position = pos;
				m_Rigidbody.transform.rotation = Quaternion.Euler(0, 0, pos.x / 25.0f);
			}
			return;
		}
		{
			Vector3 pos = m_Rigidbody.transform.position;
			pos.x += rot;
			if(pos.x > maxX)  pos.x = maxX;
			if(pos.x < -maxX) pos.x = -maxX;
			m_Rigidbody.transform.position = pos;
			{
				m_Rigidbody.transform.rotation = Quaternion.Euler(0, 0, pos.x / 25.0f);
			}

			//TODO: rot: 0<>33
			return;
		}
	}

	public void onDied(string isDied)
	{
		m_Rigidbody = null;
		m_muzzles = null;
		aeroplaneController = null;
	}

	/*void OnGUI()
	{
		//GUI.DrawTexture(new Rect(0f, 0f, m_guiTexture.width / 2, m_guiTexture.height / 2), m_guiTexture);
	}*/
}
