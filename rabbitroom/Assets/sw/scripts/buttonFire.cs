using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class buttonFire : MonoBehaviour
{
	public CannonBehavior cannonBehavior;
	public Text textInstructions;
	public GameObject buttonCamSwitch;

	void Start ()
	{
#if MOBILE_INPUT
		textInstructions.enabled = false;	//on mobile, hide instructions describing the keys
		//buttonCamSwitch.SetActive(false);	//does not (yet) work completely :-(
#else
		this.gameObject.SetActive(false);	//on pc, no fire button needed
#endif
	}
	
	public void onButtonClick ()
	{
		cannonBehavior.doShot();
		StartCoroutine(util.unfocus());
	}
}
