using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setmsg : MonoBehaviour
{
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	public void setMsg(string text)
	{
		StopCoroutine(clearMsg());

		Text txt = this.GetComponent<Text>();
		txt.text = text;

		StartCoroutine(clearMsg());
	}

	IEnumerator clearMsg()
	{
		yield return new WaitForSeconds(1.5f);
		Text text = this.GetComponent<Text>();
		text.text = " ";
	}
}
