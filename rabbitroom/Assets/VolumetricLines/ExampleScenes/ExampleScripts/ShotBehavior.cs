using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{
	public float speed = 11f;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		transform.position += transform.forward * Time.deltaTime * speed;
	}
}
