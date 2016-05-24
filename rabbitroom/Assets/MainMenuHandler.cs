using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour
{
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	void OnQuit()
	{
		Application.Quit();
	}
	void OnStartNew()
	{
		Application.LoadLevel(1);
	}

	void OnStartStarWars()
	{
		Application.LoadLevel("sw");
	}
	void OnStartStarFighter()
	{
		Application.LoadLevel("sw2");
	}
}
