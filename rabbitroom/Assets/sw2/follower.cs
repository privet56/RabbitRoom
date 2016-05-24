using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class follower : MonoBehaviour
{
	public Transform item2follow;
	public meMover memover;
	public List<ParticleSystem> afterBurners = new List<ParticleSystem>();

	public float rotationSpeed = 3.0f;
	public float moveSpeed = 3.0f;

	public float distMin = 0.10f;
	public float distMax = 0.50f;

	private float stop = 0.05f;

	private Transform thisTransform;
	private bool isMoving = false;

	//public iTweenPathFlyer itweenpathflyer = null;
	public sw2_flyArounder sw2_flyarounder = null;

	void Start ()
	{
		thisTransform = this.transform;
		for(int i=0;i<afterBurners.Count;i++)
		{
			ParticleSystem particleSystem = afterBurners[i];
			if( particleSystem.isPlaying)
				particleSystem.Pause();
		}
		//StartCoroutine(activateFollowing());
	}

	/*IEnumerator activateFollowing()
	{
		yield return new WaitForSeconds(itweenpathflyer.camFlyTime);
		itweenpathflyer = null;
	}*/

	void Update ()
	{
		if(sw2_flyarounder.isFlyingAround())
			return;
		
		moveSpeed = Mathf.Clamp(memover.getMoveSpeed(), 1.0F, 33.0F);
		if(true)
		{
			UpdateV1();
		}
		else
		{
			UpdateV2();
		}
	}
	void UpdateV2()
	{
		Transform target = this.item2follow;
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(target.position - thisTransform.position), rotationSpeed*Time.deltaTime);
		//move towards the player
		thisTransform.position += thisTransform.forward * moveSpeed * Time.deltaTime;
	}
	void UpdateV1()
	{
		Transform target = this.item2follow;
		//rotate to look at the player
		var distance = Vector3.Distance(thisTransform.position, target.position);

		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(target.position - thisTransform.position), rotationSpeed*Time.deltaTime);
		//print("dist:"+distance);
		//if((distance > distMin) && (distance > stop))
		if(distance > 0.5f)
		{
			thisTransform.position += thisTransform.forward * moveSpeed * Time.deltaTime;
			setMove(true);
		}
		else
			setMove(false);
	}
	void setMove(bool newMoveState)
	{
		if(this.isMoving == newMoveState)return;
		this.isMoving = newMoveState;

		for(int i=0;i<afterBurners.Count;i++)
		{
			ParticleSystem particleSystem = afterBurners[i];
			if(this.isMoving)
			{
				if( particleSystem.isPaused || particleSystem.isStopped)
				{
					particleSystem.Play();
					particleSystem.gameObject.SetActive(true);
				}
			}
			else
			{
				if( particleSystem.isPlaying)
				{
					particleSystem.Pause();
					particleSystem.gameObject.SetActive(false);
				}
			}
		}
	}
}
