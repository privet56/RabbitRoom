using UnityEngine;
using System.Collections;

public class activator : MonoBehaviour
{
	void Awake ()
	{
#if MOBILE_INPUT
		this.gameObject.SetActive(false);
		DestroyImmediate(this.gameObject);
#endif
	}
}
