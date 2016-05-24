using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonBehavior : MonoBehaviour {

	public Transform m_cannonRot;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;
	public Texture2D m_guiTexture;

	public float horizontalMoveSpeed = 199.9f;

	public virtual void Start ()
	{
	
	}
	
	public virtual void Update () 
	{
//#if !MOBILE_INPUT
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			onCannonRot(m_cannonRot, -Time.deltaTime * horizontalMoveSpeed);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			onCannonRot(m_cannonRot, Time.deltaTime * horizontalMoveSpeed);
		}
//#else
		else if (Input.acceleration.x < -0.22)
		{
			onCannonRot(m_cannonRot, -Time.deltaTime * horizontalMoveSpeed);
		}
		else if (Input.acceleration.x > 0.22)
		{
			onCannonRot(m_cannonRot, Time.deltaTime * horizontalMoveSpeed);
		}
//#endif
		else
		{
			onCannonRot(m_cannonRot, 0f);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			doShot();
		}
	}

	public virtual void onShotDid(GameObject shot)
	{

	}

	public void doShot()
	{
		GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
		onShotDid(go);
		GameObject.Destroy(go, 3f);
	}

	public virtual void onCannonRot(Transform cannonRot, float rot)
	{
		if(cannonRot != null)
		{
			if(Math.Abs(rot) < 0.001f)
			{
				return;//
			}
			cannonRot.transform.Rotate(Vector3.up, rot);
		}
	}


	/*void OnGUI()
	{
		//GUI.DrawTexture(new Rect(0f, 0f, m_guiTexture.width / 2, m_guiTexture.height / 2), m_guiTexture);
	}*/
}
