using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class dragmover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool mouseDown = false;
	private Vector3 startMousePos;
	private Vector3 startPos;
	private float myWidth;
	private float myHeight;

	private Transform thisTransform;
	
	public RectTransform ParentRT;
	public RectTransform MyRect;
	
	void Start()
	{
		myWidth  = (MyRect.rect.width  + 5) / 2;
		myHeight = (MyRect.rect.height + 5) / 2;
		thisTransform = this.transform;
	}
	
	
	public void OnPointerDown(PointerEventData ped) 
	{
		mouseDown = true;
		startPos = thisTransform.position;
		startMousePos = util.inputPosition();
	}
	
	public void OnPointerUp(PointerEventData ped) 
	{
		mouseDown = false;
	}

	void Update () 
	{
		if (mouseDown)
		{
			Vector3 currentPos = util.inputPosition();
			Vector3 diff = currentPos - startMousePos;
			Vector3 pos = startPos + diff;
			thisTransform.position = pos;

			bool restrictX;
			bool restrictY;

			if(thisTransform.localPosition.x < 0 - ((ParentRT.rect.width / 2)  - myWidth) || thisTransform.localPosition.x > ((ParentRT.rect.width / 2) - myWidth))
				restrictX = true;
			else
				restrictX = false;
			
			if(thisTransform.localPosition.y < 0 - ((ParentRT.rect.height / 2)  - myHeight) || thisTransform.localPosition.y > ((ParentRT.rect.height / 2) - myHeight))
				restrictY = true;
			else
				restrictY = false;

			float fakeX;
			float fakeY;

			if(restrictX)
			{
				if(thisTransform.localPosition.x < 0)
				{
					fakeX = 0 - (ParentRT.rect.width / 2) + myWidth;
				}
				else
				{
					fakeX = (ParentRT.rect.width / 2) - myWidth;
				}
				
				Vector3 xpos = new Vector3 (fakeX, thisTransform.localPosition.y, 0.0f);
				thisTransform.localPosition = xpos;
			}
			
			if(restrictY)
			{
				if(thisTransform.localPosition.y < 0)
				{
					fakeY = 0 - (ParentRT.rect.height / 2) + myHeight;
				}
				else
				{
					fakeY = (ParentRT.rect.height / 2) - myHeight;
				}
				
				Vector3 ypos = new Vector3 (thisTransform.localPosition.x, fakeY, 0.0f);
				thisTransform.localPosition = ypos;
			}
			
		}
	}
	public void setDragEnabled(bool dragMoveEnabled) 
	{
		if( this.mouseDown)
		{
			this.mouseDown = dragMoveEnabled;
		}
	}
}
