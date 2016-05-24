using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class sw2_controllerController : MonoBehaviour
{
	public GameObject m_buttonFire = null;
	public GameObject m_buttonMove = null;
	public GameObject m_textControllerDescription = null;
	
	void Start ()
	{
#if MOBILE_INPUT
		InstantGuiTextArea instantGuiTextArea = m_textControllerDescription.GetComponent<InstantGuiTextArea>();
		instantGuiTextArea.rawText = instantGuiTextArea.text = "Shoot the destroyable elements of your Galaxy!";
#else
		//on desktop:
		m_buttonFire.SetActive(false);
		m_buttonMove.SetActive(false);
#endif
	}
	
	/*void Update ()
	{
	
	}*/
}
