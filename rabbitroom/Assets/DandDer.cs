using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;	//PointerEventData

public class DandDer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	public GameObject DandDObjectTemplate = null;
	public Camera mainCamera = null;
	private GameObject DandDObjectNew = null;
	private RectTransform canvasRectTransform = null;

	void Awake ()
	{
		Canvas canvas = GetComponentInParent <Canvas>();
		if (canvas != null)
		{
			canvasRectTransform = canvas.transform as RectTransform;
		}
	}

	void Start ()
	{
	
	}
	void Update ()
	{
	
	}
	public void OnEndDrag (PointerEventData eventData)
	{
		if(DandDObjectNew == null)
			return;
		DandDObjectNew.GetComponent<DandDObject>().onEndDragOutOfPanel();
		DandDObjectNew = null;
		this.GetComponent<AudioSource>().Play();
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		DandDObjectNew = (GameObject) GameObject.Instantiate(DandDObjectTemplate, this.transform.position, Quaternion.identity);
		DandDObjectNew.transform.SetParent(GameObject.Find ("draggeds").transform);
		DandDObjectNew.GetComponent<DandDObject>().onStartDragOutOfPanel();

		/*
		UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rigidbodyFirstPersonController = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
		Vector3 rot = new Vector3(rigidbodyFirstPersonController.transform.rotation.x, rigidbodyFirstPersonController.transform.rotation.y, rigidbodyFirstPersonController.transform.rotation.z);
		print ("rigidbody.rot.y:"+rot.y);
		DandDObjectNew.transform.Rotate(rot);
		*/
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(DandDObjectNew == null)
			return;

		Vector3 inputPosition = util.inputPosition();
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, mainCamera.nearClipPlane));

		float z = worldPos.z;
		Vector3 newpos = new Vector3(worldPos.x, worldPos.y, z/*mainCamera.transform.position.z + 1*/);
		newpos = newpos + mainCamera.transform.forward;
        //print("pos:"+eventData.position.x+"x"+eventData.position.y+"  ---> newpos:"+newpos.x+"x"+newpos.y+"x"+newpos.z);
		DandDObjectNew.transform.position = newpos;
	}
	Vector2 ClampToWindow(Vector3 pos)
	{
		Vector2 rawPointerPosition = new Vector2(pos.x, pos.y);
		
		Vector3[] canvasCorners = new Vector3[4];
		canvasRectTransform.GetWorldCorners (canvasCorners);
		
		float clampedX = Mathf.Clamp (rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
		float clampedY = Mathf.Clamp (rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);
		
		Vector2 newPointerPosition = new Vector2 (clampedX, clampedY);
		return newPointerPosition;
	}
}
