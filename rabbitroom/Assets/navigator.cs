using UnityEngine;
using CnControls;
using System.Collections;

public class navigator : MonoBehaviour
{
	public Rigidbody _rigidbody = null;
	public Camera _camera = null;
	private float movementSpeed = 50.0f;

	void Start ()
	{
	
	}

	//
	//Mouse movement via 'Rigid Body First Person Controller' deactivated: X|Y Sensitivity = 0 + Forward Speed = 0
	//
	void FixedUpdate()
	{
		//Vector2 movement = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0f);
		float rotation = CnInputManager.GetAxis("Horizontal");
		float movement = CnInputManager.GetAxis("Vertical");

		if(Mathf.Abs(rotation) < 0.1f)
		{
			if (Input.acceleration.x < -0.22)
			{
				rotation = -0.4f;
			}
			else if (Input.acceleration.x > 0.22)
			{
				rotation = 0.4f;
			}
		}

		if(movement > 0.13f)
		{
			_rigidbody.SendMessage ("setButtonForwardMovement", movement * movementSpeed * Time.deltaTime);
		}
		else
		{
			_rigidbody.SendMessage ("resetButtonForwardMovement");
		}
		if(Mathf.Abs(rotation) > 0.3f)
		{
			_rigidbody.SendMessage ("addButtonRotation", rotation);
		}
		else
		{
			_rigidbody.SendMessage ("setButtonRotation", 2/*actually 0*/);
		}
	}
}
