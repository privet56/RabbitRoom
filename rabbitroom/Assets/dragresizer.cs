using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class dragresizer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool mouseDown = false;
	public RectTransform ParentRT;
	Vector2 initialMousePos;
	Vector2 initialDeltaSize;
	RectTransform rectTransform;
	Vector2 sizeDeltaOriginal;

	void Start()
	{
		rectTransform = (RectTransform)ParentRT.transform;
		sizeDeltaOriginal = rectTransform.sizeDelta;
	}
	
	
	public void OnPointerDown(PointerEventData ped) 
	{
		enableParentDrag(false);

		initialMousePos = util.inputPosition();
		initialDeltaSize = rectTransform.sizeDelta;

		mouseDown = true;
	}
	
	public void OnPointerUp(PointerEventData ped) 
	{
		enableParentDrag(true);
		mouseDown = false;
	}
	
	void Update () 
	{
		if(mouseDown)
		{
			enableParentDrag(false);
			Vector2 temp = (Vector2)initialMousePos - (Vector2)util.inputPosition();
			temp = new Vector2 (-temp.x, temp.y);
			temp += initialDeltaSize;

			temp.x = Mathf.Max(sizeDeltaOriginal.x, temp.x);
			temp.y = Mathf.Max(sizeDeltaOriginal.y, temp.y);

			rectTransform.sizeDelta = temp;
		}
	}
	public void enableParentDrag(bool enable)
	{
		SendMessageUpwards("setDragEnabled", enable, SendMessageOptions.DontRequireReceiver);
	}
}
