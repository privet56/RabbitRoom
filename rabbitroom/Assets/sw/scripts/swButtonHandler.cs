using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class swButtonHandler : MonoBehaviour
{
	private int loadLevelOnAnimationFinished = -1;
	public Animator animator;

	void Start ()
	{
		util.setRootMotion(this.GetComponent<Animator>());
		util.setRootMotion(this.animator);
	}
	
	void Update ()
	{

	}
	
	public void onClickBack()
	{
#if MOBILE_INPUT
		Application.LoadLevel(0);
#else
		doAniAndLoadLevel(0);
#endif
	}
	public void onClickRestart()
	{
#if MOBILE_INPUT
		Application.LoadLevel(Application.loadedLevel);
#else
		doAniAndLoadLevel(Application.loadedLevel);
#endif
	}
	public void doAniAndLoadLevel(int level2Load)
	{
		this.loadLevelOnAnimationFinished = level2Load;
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		animator.enabled = true;
		animator.SetBool("mirror", true);
		animator.SetFloat("speed", -0.2f);
		animator.Play(state.fullPathHash, 0, /*from the end*/1);
		//onAnimationFinished(null);
		{
			//hide parent panel
			//not good... I don't receive msg if deactivated!
			//this.transform.parent.gameObject.SetActive(false);
			this.transform.parent.localScale = new Vector3(0.0f,0.0f,0.0f);
		}
	}
	public void onAnimationFinished(string isEndAnimation)
	{
		if(this.loadLevelOnAnimationFinished < 0)
		{
			return;
		}

		{
			bool bIsEndAnimation = true;
			if(!bool.TryParse(isEndAnimation, out bIsEndAnimation))
				return;
			
			if(!bIsEndAnimation)	//=we are at the beginning again, after reversing the startAnimation
			{
				int _loadLevelOnAnimationFinished = loadLevelOnAnimationFinished;
				this.loadLevelOnAnimationFinished = -1;
				Application.LoadLevel(_loadLevelOnAnimationFinished);
			}
		}
	}
}
