using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bkg : MonoBehaviour
{
	public List<Material> skyboxMaterials = null;
	public Skybox skybox = null;
	private bool forward = true;
	private int currentSkyBoxMaterial = -1;
	public float speed = 0.09f;

	void Start ()
	{
		if (skybox == null)
		{
			skybox = gameObject.GetComponent<Skybox>();
		}
		StartCoroutine(updateSkyBoxMaterial());	//T.ODO: comment line aout
	}
	IEnumerator updateSkyBoxMaterial()
	{
		while(true)
		{
			yield return new WaitForSeconds(9.5f);
			currentSkyBoxMaterial++;
			if (currentSkyBoxMaterial >= skyboxMaterials.Count)
				currentSkyBoxMaterial = 0;
			{
				/*if(skybox.material.color != null)
					StartCoroutine(FadeTo(0.9f));
				else*/
					skybox.material = skyboxMaterials[currentSkyBoxMaterial];
			}
		}
	}

	IEnumerator FadeTo(float aTime)
	{
		{
			float alpha = skybox.material.color.a;			//error: Material doesn't have a color property '_Color'
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(skybox.material.color.r, skybox.material.color.g, skybox.material.color.b, Mathf.Lerp(alpha,0.3f,t));
				skybox.material.color = newColor;
				yield return null;
			}
		}
		{
			skybox.material = skyboxMaterials[currentSkyBoxMaterial];
			Color newColor = new Color(skybox.material.color.r, skybox.material.color.g, skybox.material.color.b, 0.3f);
			skybox.material.color = newColor;
		}
		{
			float alpha = skybox.material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(skybox.material.color.r, skybox.material.color.g, skybox.material.color.b, Mathf.Lerp(alpha,1.0f,t));
				skybox.material.color = newColor;
				yield return null;
			}
		}
	}
	void Update ()
	{
		MathScroll();
	}

	void MathScroll()
	{
		float all = forward ? speed : -speed;

		{
			float offset = Time.time * speed;
			this.skybox.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
			return;
		}

		float HorizSpeed = all;
		float VertSpeed  = all;
		float HorizUVMin = all-0.15f;
		float HorizUVMax = all+990.1f;
		float VertUVMin  = all-0.15f;
		float VertUVMax  = all+990.1f;
		
		//Scrolls texture between min and max
		Vector2 Offset = new Vector2((this.skybox.material.mainTextureOffset.x > HorizUVMax) ?
		                             	HorizUVMin : this.skybox.material.mainTextureOffset.x + Time.deltaTime * HorizSpeed,
		                             (this.skybox.material.mainTextureOffset.y > VertUVMax) ?
		                             	VertUVMin : this.skybox.material.mainTextureOffset.y + Time.deltaTime * VertSpeed);
		//Update UV coordinates
		this.skybox.material.mainTextureOffset = Offset;
		/*
		if((this.skybox.material.mainTextureOffset.x > HorizUVMax) && forward)
			forward = false;
		if((this.skybox.material.mainTextureOffset.x < 0.0f) && !forward)
			forward = true;
		*/
	}
	void onAnimationStart()
	{
		util.BroadcastAll("onAnimationFinished", /*isEndAnimation*/"false");
	}
	void onAnimationEnd()
	{
		util.BroadcastAll("onAnimationFinished", /*isEndAnimation*/"true");		
	}
}
