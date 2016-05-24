using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gameOver : MonoBehaviour
{
	private Transform thisTransform = null;

	public GameObject canvas = null;
	public GameObject PanelMsg = null;
	public AudioClip audioWon = null;
	public AudioClip audioLost = null;
	public GameObject sounds = null;

	void Start ()
	{
		thisTransform = this.transform;
		//StartCoroutine(simulateColl());	//T.ODO: comment line aout
	}
	void Update ()
	{

	}
	IEnumerator simulateColl()
	{
		yield return new WaitForSeconds(2.5f);
		onWon(null);
	}
	public void onWon(string isWon)
	{
		showMsg("YOU WON", audioWon);
	}
	public void onDied(string isDied)
	{
		showMsg("YOU LOST", audioLost);
	}
	private void showMsg(string msg, AudioClip audio)
	{
		RectTransform rectTransform = PanelMsg.GetComponent<RectTransform>();
		Vector3 pos = thisTransform.position;
		pos.x = (Screen.width / 2) - (rectTransform.rect.width / 2);
		pos.y = -(Screen.height / 2) - (rectTransform.rect.height / 2);
		pos.z = 0.0f;

		GameObject go = GameObject.Instantiate(PanelMsg, pos, thisTransform.rotation) as GameObject;
		{
			Text text = go.GetComponentInChildren<Text>();
			text.text = "Game Over\n"+msg;
		}
		go.transform.SetParent(canvas.transform, false);
		{
			Text[] texts = go.GetComponentsInChildren<Text>();
			for(int i=0;i<texts.Length;i++)
			{
				if( texts[i].tag == "instruction")
					texts[i].enabled = false;
			}
		}
		{
			AudioSource audioSource = sounds.GetComponent<AudioSource>();
			audioSource.clip = audio;
			audioSource.Play();
		}
	}
}
