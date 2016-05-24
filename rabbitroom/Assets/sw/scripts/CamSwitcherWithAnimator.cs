using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamSwitcherWithAnimator : MonoBehaviour
{
	public Animator animator;
	public Animation animation;

	void Start ()
	{

	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			print("ani!!!");
			//animation.CrossFadeQueued("camCockpit", 0.3f, QueueMode.PlayNow);
			animator.Play(Animator.StringToHash("camCockpit"));
		}
	}
}
