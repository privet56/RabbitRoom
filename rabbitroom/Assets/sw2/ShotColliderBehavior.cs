using UnityEngine;
using System.Collections;

public class ShotColliderBehavior : MonoBehaviour
{
	private Transform thisTransform = null;

	public GameObject m_firePrefab = null;
	public GameObject m_firePrefab2 = null;

	void Start ()
	{
		thisTransform = this.transform;
	}
	
	/*void Update ()
	{
	
	}*/

	void OnCollisionEnter(Collision col)
	{
		startFire(col.gameObject);
		
		Destroy(col.gameObject);
		Destroy(this.gameObject);
	}
	void startFire(GameObject destroyedGameObject)
	{
		Vector3 pos =  thisTransform.position;
		Quaternion rot = this.thisTransform.rotation;

		{
			GameObject fire = GameObject.Instantiate(m_firePrefab, pos, rot) as GameObject;
			GameObject.Destroy(fire, 6f/*seconds*/);
		}
		{
			GameObject fire = GameObject.Instantiate(m_firePrefab2, pos, rot) as GameObject;
			GameObject.Destroy(fire, 6f/*seconds*/);
		}

		//fire.transform.localScale = destroyedGameObject.transform.lossyScale * 33.3f;
	}
}
