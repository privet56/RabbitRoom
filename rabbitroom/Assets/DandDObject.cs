using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;	//PointerEventData, IDragHandler

public class DandDObject : MonoBehaviour//, IDragHandler
{
	public Vector3 scale2setWhenStartDragOutOfPanel = Vector3.zero;
	public Vector3 rotation2setWhenStartDragOutOfPanel = Vector3.zero;
	public float boxColliderSizeY   = 0.0f;
	public float boxColliderCenterY = 0.0f;
	public Quaternion rotation2Keep = Quaternion.identity;
	public bool lookAtMainCamera = false;
	private Transform thisTransform = null;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rigidbodyFirstPersonController = null;
	private Rigidbody thisRigidBody = null;
	private Renderer thisRenderer = null;
	private Color thisRendererMaterialColor = Color.white;


	Animator animator = null;

	void init()
	{
		//this way I will be much faster!
		thisTransform = this.transform;
		thisRigidBody = gameObject.GetComponent<Rigidbody>();
		thisRenderer = gameObject.GetComponentInChildren<Renderer>();
		if(thisRenderer)
		{
			thisRendererMaterialColor = thisRenderer.material.color;
		}
	}


	void Start ()
	{
		init();
	}

	public void onStartDragOutOfPanel()
	{
		if( GetComponent<GUIEffects>())
		{
			GetComponent<GUIEffects>().enabled = false;
		}

		init();

		rigidbodyFirstPersonController = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();

		if(scale2setWhenStartDragOutOfPanel != Vector3.zero)
		{
			thisTransform.localScale = scale2setWhenStartDragOutOfPanel;
		}
		if(rotation2setWhenStartDragOutOfPanel != Vector3.zero)
		{
			thisTransform.Rotate(rotation2setWhenStartDragOutOfPanel);
		}
		rotation2Keep = thisTransform.rotation;
	}
	public void onEndDragOutOfPanel()
	{
		{
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			{
				Vector3 size = boxCollider.size;
				size.y = boxColliderSizeY;
				boxCollider.size = size;
			}
			{
				Vector3 center = boxCollider.center;
				center.y = boxColliderCenterY;
				boxCollider.center = center;
			}
		}
		if(!thisRigidBody)
		{
			thisRigidBody = gameObject.AddComponent<Rigidbody>();
			thisRigidBody.mass = 5;
		}

		animator = GetComponent<Animator>();
		if( animator)
		{
			StartCoroutine(AnimateAnimator());
		}

		StartCoroutine(ShakeMainCam());

		if(lookAtMainCamera)
		{
			StartCoroutine(TrackRotation(rigidbodyFirstPersonController.transform));
		}
	}

/// DRAG & DROP -------- BEGIN
	private bool dragging = false;
	private float distance;

	void OnMouseEnter()
	{
		if (thisRenderer)
		{
			thisRenderer.material.color = Color.red;
		}
	}
	
	void OnMouseExit()
	{
		if (thisRenderer)
		{
			thisRenderer.material.color = thisRendererMaterialColor;
			thisRenderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
	}
	
	void OnMouseDown()
	{
		//if(Input.GetMouseButtonDown(1/*RIGHT MOUSE BUTTON*/)) ...	//OnMouseDown is only trigger by a left mouse click

		distance = Vector3.Distance(thisTransform.position, Camera.main.transform.position);
		dragging = true;
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButton(1/*RIGHT MOUSE BUTTON*/))
		{
			float speed = 5.0f; // default speed
			this.thisRigidBody.angularVelocity = Vector3.zero;
			this.thisRigidBody.velocity = Camera.main.transform.forward * speed;
			return;
		}
	}

	void OnMouseUp()
	{
		dragging = false;
		StartCoroutine(ShakeMainCam());
	}
	void Update ()
	{
		if (dragging)
		{
			Ray ray = Camera.main.ScreenPointToRay(util.inputPosition());
			Vector3 rayPoint = ray.GetPoint(distance);
			thisTransform.position = rayPoint;
		}

		if(thisRenderer && (thisRenderer.material.color == Color.red))	//if(mouseOver)
		{
			MathScroll();
		}

		{
			Vector3 currentRotation = thisTransform.rotation.eulerAngles;
			if((currentRotation.z > 1.1f) || (currentRotation.z < -1.1f))
		   	{
				if(!lookAtMainCamera)
				{
					thisTransform.rotation = rotation2Keep;
				}

				if(thisRigidBody != null)
				{
					thisRigidBody.velocity = Vector3.zero;
					thisRigidBody.angularVelocity = Vector3.zero;
				}
			}
		}

		{

		}
	}
	/// DRAG & DROP -------- END

	private IEnumerator AnimateAnimator()
	{
		while(true)
		{
			yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
			animator.Play("Punch", -1, 0f);
		}
	}

	IEnumerator TrackRotation(Transform Target)
	{
		float RotateSpeed = 100f;

		while(true)	//Loop forever and track target
		{
			if(thisTransform != null && Target != null)
			{
				//Get direction to target
				Vector3 relativePos = Target.position - thisTransform.position;
				//Calculate rotation to target
				Quaternion NewRotation = Quaternion.LookRotation(relativePos);
				NewRotation.z = rotation2setWhenStartDragOutOfPanel.z;
				//Rotate to target by speed
				thisTransform.localRotation = Quaternion.RotateTowards(thisTransform.localRotation, NewRotation, RotateSpeed * Time.deltaTime);
			}
			//wait for next frame
			yield return null;
		}
	}

	void MathScroll()
	{
		float all = 0.2f;
		float HorizSpeed = all;
		float VertSpeed  = all;
		float HorizUVMin = all-0.15f;
		float HorizUVMax = all+0.1f;
		float VertUVMin  = all-0.15f;
		float VertUVMax  = all+0.1f;

		//Scrolls texture between min and max
		Vector2 Offset = new Vector2((thisRenderer.material.mainTextureOffset.x
		                              > HorizUVMax) ? HorizUVMin : thisRenderer.material.mainTextureOffset.x +
		                             Time.deltaTime * HorizSpeed,
		                             (thisRenderer.material.mainTextureOffset.y > VertUVMax) ?
		                             VertUVMin : thisRenderer.material.mainTextureOffset.y + Time.deltaTime *
		                             VertSpeed);
		//Update UV coordinates
		thisRenderer.material.mainTextureOffset = Offset;
	}

	public IEnumerator ShakeMainCam()
	{
		yield return new WaitForSeconds(0.5f);
		float ShakeTime = 1.0f;
		//Shake amount - distance to offset in any direction
		float ShakeAmount = 1.0f;
		//Speed of camera moving to shake points
		float ShakeSpeed = 1.0f;

		Transform camTransform = Camera.main.transform;
		//Store original camera position
		Vector3 OrigPosition = camTransform.localPosition;
		//Count elapsed time (in seconds)
		float ElapsedTime = 0.0f;
		//Repeat for total shake time
		while(ElapsedTime < ShakeTime)
		{
			//Pick random point on unit sphere
			Vector3 RandomPoint = OrigPosition + Random.insideUnitSphere * ShakeAmount;
			//Update Position
			camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, RandomPoint, Time.deltaTime * ShakeSpeed);
			//Break for next frame
			yield return null;
			//Update time
			ElapsedTime += Time.deltaTime;
		}
		//Restore camera position
		camTransform.localPosition = OrigPosition;
	}
}
