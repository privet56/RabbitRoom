using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class xwingCollider : MonoBehaviour
{
	public List<Transform> m_firePlaces = new List<Transform>();

	public GameObject m_firePrefab = null;
	public GameObject m_livesPlaceHolder = null;
	public GameObject m_heart = null;
	private List<GameObject> m_hearts = new List<GameObject>();
	public RectTransform panel = null;
	public int maxLives = 4;

	private Rigidbody thisRigidBody = null;
	private Transform thisTransform = null;

	private int IamHit = 0;

	void Start ()
	{
		thisTransform = this.transform;
		thisRigidBody = this.GetComponent<Rigidbody>();
		if (maxLives < 1)
		{
			maxLives = m_firePlaces.Count;
		}
		DrawLives();
		//StartCoroutine(simulateColl());	//T.ODO: comment line aout
	}
	
	/*void Update ()
	{

	}*/
	void OnCollisionEnter(Collision col)
	{
		Physics.IgnoreCollision(col.collider, this.GetComponent<Collider>());
		this.thisRigidBody.velocity = this.thisRigidBody.angularVelocity = Vector3.zero;

		//if((col.gameObject.tag == "shot") || (col.gameObject.tag == "enemy"))
		{
			Vector3 cp = col.contacts[0].point;	//later: position fire on coll!
			//print("coll on xwing ... hit#"+IamHit+" "+col.gameObject.tag+" point:"+cp.x+"x"+cp.y+"x"+cp.z);
			doOnColl(col);
		}
	}
	IEnumerator simulateColl()
	{
		yield return new WaitForSeconds(2.5f);
		doOnColl(null);
		yield return new WaitForSeconds(2.5f);
		doOnColl(null);
		yield return new WaitForSeconds(2.5f);
		doOnColl(null);
	}
	void doOnColl(Collision col)
	{
		IamHit++;
		{
			if(m_hearts.Count > 0)
			{
				GameObject lastHeart = m_hearts[m_hearts.Count - 1];
				setDied(lastHeart);
				m_hearts.Remove(lastHeart);
			}
		}
		if(this.isGameLost())
		{
			//print("XWING died(IamHit:"+IamHit+") coll:"+col.gameObject.tag);
			{
				BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider>();
				boxCollider.enabled = false;	//=no more collision!
			}
			util.BroadcastAll("onDied", "true");
			//Destroy(this.gameObject);
			return;
		}
		//print("XWING collided with:"+col.gameObject.tag+" IamHit:"+IamHit);
		Transform fireParent = m_firePlaces[IamHit-1];
		Vector3 pos = fireParent.position;
		pos.y += 33.3f;	//further upwards
		pos.z += 33.3f;	//further forward
		GameObject fire = GameObject.Instantiate(m_firePrefab, pos, fireParent.rotation) as GameObject;
		//GameObject fire = GameObject.Instantiate(m_firePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		fire.transform.parent = fireParent.transform;	//=fire.setParent(fireParent);
	}
	public bool isGameLost()
	{
		if(IamHit >= maxLives)//m_firePlaces.Count)
		{
			return true;
		}
		return false;
	}
	public void onWon(string isWon)
	{
		//print ("xwinCollider:onWon: no more collision");
		BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider>();
		boxCollider.enabled = false;	//=no more collision!
	}
	public void DrawLives()
	{
		{
			//Vector3 pos = m_livesPlaceHolder.transform.position;
			//pos.x = ((-(Screen.width / 2)) + panel.rect.width);
			//m_livesPlaceHolder.transform.position = pos;
		}
		for(int i=0;i<maxLives;i++)
		{
			Vector3 pos = this.m_livesPlaceHolder.transform.position;
			pos.x += (i * 66);
			GameObject heart = GameObject.Instantiate(m_heart, pos, this.m_livesPlaceHolder.transform.rotation) as GameObject;
			heart.transform.parent = this.m_livesPlaceHolder.transform;	//=.setParent(parent);
			heart.SetActive(true);
			heart.transform.localScale = new Vector3(1f, 1f, 1f);	//set scale AFTER setting parent!
			m_hearts.Add(heart);
		}
		{
			Transform heart = this.m_livesPlaceHolder.transform.GetChild(0);
			Transform skull = this.m_livesPlaceHolder.transform.GetChild(1);
			heart.gameObject.SetActive(false);
			skull.gameObject.SetActive(false);
		}
	}
	void setDied(GameObject live)
	{
		Transform heart = live.transform.GetChild(0);
		Transform skull = live.transform.GetChild(1);
		heart.gameObject.SetActive(false);
		skull.gameObject.SetActive(true);
		UnityStandardAssets.Utility.AutoMoveAndRotate autoMoveAndRotate = live.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>();
		UnityStandardAssets.Utility.AutoMoveAndRotate.Vector3andSpace vector3andSpace = autoMoveAndRotate.rotateDegreesPerSecond;
		//vector3andSpace.value.y *= 6.0f;
	}
}
