using UnityEngine;
using System.Collections;

public class slowmobutton : MonoBehaviour
{
	private bool isSlowMo = false;

	void Start ()
	{
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

	public void onSlowMoButtonClick()
	{
		isSlowMo = !isSlowMo;
		Time.timeScale = isSlowMo ? 0.2f : 1.0f;
		setmsg msg = FindObjectOfType<setmsg>();
		msg.setMsg("'Slow Motion' is now "+(isSlowMo ? "" : "de")+"activated!");
	}
}
