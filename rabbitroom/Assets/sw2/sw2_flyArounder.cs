using UnityEngine;
using System.Collections;

public class sw2_flyArounder : MonoBehaviour
{
	private Transform thisTransform = null;

	public Transform flyto;

	[Range(0.00001f, 0.1f)]
	public float wait = 0.01f;
	public float radius = 1.0f;
	public float startAngle = 0.0f;
	public float angleIncr = 2.0f;
	public float yInrc = 0f;
	public float yStart = 0f;
	private Vector3 thisOriginalTransformPos;
	private Quaternion thisOriginalTransformRot;
	//TODO: m_bLoop;

	void Start ()
	{
		this.thisTransform = transform;
		this.thisOriginalTransformPos = thisTransform.position;
		this.thisOriginalTransformRot = thisTransform.rotation;
		StartCoroutine(fly(thisTransform, flyto));
		thisTransform.position = new Vector3(thisTransform.position.x, yStart, thisTransform.position.z);
	}

	private static Vector2 PointOnCircle(Vector2 origin, float radius, float angle)
	{
		float x = radius * Mathf.Cos (angle * Mathf.Deg2Rad) + origin.x;
		float y = radius * Mathf.Sin (angle * Mathf.Deg2Rad) + origin.y;

		return new Vector2(x,y);
	}

	private IEnumerator fly(Transform thisTransform, Transform flyto)
	{
		bool roundComplete = false;
		float angle = startAngle;
		while(!roundComplete)
		{
			if((angle - startAngle) > 350f)
			{
				angle = startAngle;
				roundComplete = true;
			}

			yield return new WaitForSeconds(wait);

			{
				thisTransform.LookAt(flyto);
				//thisTransform.Rotate(0f, angle / 360f, 0f);
				//print(" angle:"+angle+" roty:"+(angle / 360f)+" radius:"+radius);
			}

			angle += angleIncr;
			Vector2 newPos = PointOnCircle(new Vector2(flyto.position.x, flyto.position.z), radius, angle);
			float y = thisTransform.position.y + this.yInrc;
			thisTransform.position = new Vector3(newPos.x, y, newPos.y);
		}

		thisTransform.position = this.thisOriginalTransformPos;
		thisTransform.rotation = this.thisOriginalTransformRot;
		this.flyto = null;
	}
	public bool isFlyingAround()
	{
		return (this.flyto != null);
	}
}
