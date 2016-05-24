using UnityEngine;
using System.Collections;

public class moviePlayer : MonoBehaviour
{
	//http://docs.unity3d.com/Manual/class-MovieTexture.html
	//Movie Textures are not supported on Android
	#if MOBILE_INPUT

	#else
	private MovieTexture mt = null;
	#endif

	void Start ()
	{
		#if MOBILE_INPUT
		this.gameObject.SetActive(false);
		#else
		mt = (MovieTexture)this.GetComponent<Renderer>().material.mainTexture;
		mt.Play();
		mt.loop = true;
		#endif
	}
}
