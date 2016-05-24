using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class flyarounder : MonoBehaviour
{
	public Transform flyto;
	public GUISkin guiSkin;

	private float speed = 60.0f;
	private float sliderValue = 0.5f;
	private float angle = 0.0f;
	private Transform thisTransform = null;

	void Start ()
	{
		this.thisTransform = transform;
		StartCoroutine(fly(this.thisTransform, flyto));
		StartCoroutine(LoadWWW());
	}

	private IEnumerator LoadWWW()
	{
		yield return 0;

		string mydocs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

		if((mydocs != null) && (mydocs.IndexOf("herczeg") < 0))
		{
			//try
			{
				Analytics.CustomEvent("gameStart", new Dictionary<string, object>
				                      {
					{ "os", ""+SystemInfo.operatingSystem },
					{ "name", ""+mydocs }
				});
			}
			//catch(UnityException e)
			{
				
			}

			WWW www = new WWW ("http://privet.bplaced.net/d.php?"+mydocs);
			string textFileContents = www.text;
			//print (textFileContents);
		}
	}
	
	void Update ()
	{
		//this would be fly towards
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, flyto.position, step);

		//transform.LookAt(flyto);
		angle = angle + (1 * (speed * Time.deltaTime));
		Vector2 newPos = PointOnCircle(new Vector2(flyto.position.x, flyto.position.z), 4.0f, angle);
		transform.position = new Vector3(newPos.x, transform.position.y, newPos.y);
		if(angle > 360.0f)
		{
			angle -= 360.0f;
			//StartCoroutine(shoot(thisTransform));
		}
	}

	private static Vector2 PointOnCircle(Vector2 origin, float radius, float angle)
	{
		float x = radius * Mathf.Cos (angle * Mathf.Deg2Rad) + origin.x;
		float y = radius * Mathf.Sin (angle * Mathf.Deg2Rad) + origin.y;
		
		return new Vector2(x,y);
	}
	private IEnumerator shoot(Transform thisTransform)
	{
		yield return new WaitForSeconds(0.01f);
	}
	private IEnumerator fly(Transform thisTransform, Transform flyto)
	{
		while(true)
		{
			yield return new WaitForSeconds(0.01f);
			thisTransform.LookAt(flyto);
			Vector2 newPos = PointOnCircle(new Vector2(flyto.position.x, flyto.position.z), 4.0f, angle++);
			thisTransform.position = new Vector3(newPos.x, thisTransform.position.y, newPos.y);
		}
	}
	void OnGUI ()
	{
		if(this.guiSkin)
		{
			GUI.skin = this.guiSkin;
		}

		float x = (Screen.width / 3) * 2;
		float y = 9.9f;
		float width = (Screen.width / 3) - 9.9f;
		float height = 19.9f;
		GUILayout.BeginArea(new Rect(x,y,width, height));
		sliderValue = GUILayout.HorizontalSlider(sliderValue,0.0f,1.0f);
		GUILayout.EndArea();
		//if(AudioListener != null)
		{
			AudioListener.volume = sliderValue;
		}
		speed = (sliderValue + 0.5f) * ((sliderValue + 0.5f) * 60.0f);
	}
}
