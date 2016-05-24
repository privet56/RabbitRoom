using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class statevisualizer : MonoBehaviour
{
	private GameObject draggeds = null;
	public Texture2D bunnies2D;
	public RawImage bunniesRaw;
	private int bunnyCountOnYard = 0;
	private int bunnyCountOnYardAsMoney = 0;
	public GUISkin guiSkin;

	void Start ()
	{
		draggeds = GameObject.Find ("draggeds");
		StartCoroutine(countBunniesOnYard());
	}
	
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		if(this.guiSkin)
		{
			GUI.skin = this.guiSkin;
		}

		Rect r = new Rect(3,3,Screen.width, Screen.height);
		GUILayout.BeginArea(r);

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		ImagesForInteger(bunnyCountOnYard, bunnies2D);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(" ("+getMoneyCount()+" RabbiTalers)");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}
	private void ImagesForInteger(int total, Texture2D icon2D)
	{
		for(int i=0; i < total; i++)
		{
			//GUILayout.Label(icon2D);
			GUILayout.Label(bunniesRaw.texture);
		}
	}
	IEnumerator countBunniesOnYard()
	{
		while(true)
		{
			yield return new WaitForSeconds(1.9f);
			this.bunnyCountOnYard = draggeds.transform.childCount;
		}
	}
	private int getMoneyCount()
	{
		int maxMoney = this.bunnyCountOnYard * 100;
		if(bunnyCountOnYardAsMoney < maxMoney)
		{
			bunnyCountOnYardAsMoney++;
		}
		return bunnyCountOnYardAsMoney;
	}
}
