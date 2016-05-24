using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;	//PointerEventData

public class buttonfunctions : MonoBehaviour
{
	private int loadLevelOnAnimationFinished = -1;
	public Animator animator;
	public GameObject panel;
	public bool panelAnimationMinimize = true;

	void Start ()
	{
		if(animator)
		{
			//TODO: add listener?
		}
		{
			Animator myAnimator = this.GetComponent<Animator>();
			if(myAnimator)
			{
				#if !MOBILE_INPUT
				myAnimator.applyRootMotion = false;
				#else
				myAnimator.applyRootMotion = true;
				#endif
			}
		}
	}
	
	void Update ()
	{
	
	}

	public void onBack()
	{
		doAniAndLoadLevel(Application.loadedLevel - 1);
	}
	public void onRestart()
	{
		doAniAndLoadLevel(Application.loadedLevel);
	}
	public void doAniAndLoadLevel(int level2Load)
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		animator.enabled = true;
		animator.SetBool("mirror", true);
		animator.SetFloat("speed", -1.1f);
		animator.Play(state.fullPathHash, 0, /*from the end*/1);
		this.loadLevelOnAnimationFinished = level2Load;
	}
	public void onAnimationFinished(string isEndAnimation)
	{
		if(loadLevelOnAnimationFinished < 0)
			return;

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
	public void onMinimizePanel()
	{
		Animator myAnimator = this.GetComponent<Animator>();
		#if !MOBILE_INPUT
		myAnimator.applyRootMotion = false;
		#else
		myAnimator.applyRootMotion = true;
		#endif

		onMinimizePanel(this.animator, panelAnimationMinimize);
		this.panelAnimationMinimize = !this.panelAnimationMinimize;
		onMinimizePanel(myAnimator, panelAnimationMinimize);

		/*{	//panel
			animator.enabled = false;
			AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
			animator.enabled = true;
			animator.SetBool("mirror", this.panelAnimationMinimize);
			animator.SetFloat("speed", this.panelAnimationMinimize ? 1.1f : -1.1f);
			animator.Play(state.fullPathHash, 0, /*from the beginning = 0* /this.panelAnimationMinimize ? 0 : 1);

		}
		{	//me

		}*/
	}
	public static void onMinimizePanel(Animator a, bool panelAnimationMinimize)
	{
		a.enabled = false;
		AnimatorStateInfo state = a.GetCurrentAnimatorStateInfo(0);
		a.enabled = true;
		a.SetBool("mirror", panelAnimationMinimize);
		a.SetFloat("speed", panelAnimationMinimize ? 1.1f : -1.1f);
		a.Play(state.fullPathHash, 0, /*from the beginning = 0*/panelAnimationMinimize ? 0 : 1);
	}
	//StartCoroutine(enableAnimator(animator, false, 2.1f));
	/*public IEnumerator enableAnimator(Animator animator, bool enable, float wait)
	{
		yield return new WaitForSeconds(wait);
		animator.enabled = enable;
		print("animator.enabled:"+animator.enabled);
	}*/
}
