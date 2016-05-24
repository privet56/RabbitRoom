using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class meMover : MonoBehaviour
{
	public Image speedImage;
	
	private float moveSpeed 	= 0.0f;
	private Transform thisTransform = null;

	public float rotSpeed 	= 20.0f;

	public float moveAcceleration 	= 0.05f;
	public float moveSpeedMax = 13.3f;//max speed
	public float posMax = 99.9f;

	public Transform xwingTransform = null;
	public float xwingRotSpeed 	= 0.06f;
	public float xwingRotMax 	= 0.22f;

	public AudioClip audioSpeedUp = null;
	public AudioClip audioSpeedDown = null;

	public meShooter meshooter = null;

	public InstantGuiElement upDown = null;
	public sw2_flyArounder sw2_flyarounder = null;

	public List<ParticleSystem> afterBurners = new List<ParticleSystem>();

	void Start ()
	{
		thisTransform = this.transform;

		for(int i=0;i<afterBurners.Count;i++)
		{
			ParticleSystem particleSystem = afterBurners[i];
			if( particleSystem.isPlaying)
				particleSystem.Pause();
		}
	}
	
	void Update ()
	{
//rot
		if(!sw2_flyarounder.isFlyingAround())
		{
			bool isRotated = false;
			if (Input.GetKey(KeyCode.LeftArrow) || (Input.acceleration.x < -0.22))
			{
				isRotated = doRot(KeyCode.LeftArrow);
			}
			if (Input.GetKey(KeyCode.RightArrow)|| (Input.acceleration.x > 0.22))
			{
				isRotated = doRot(KeyCode.RightArrow);
			}
			if (Input.GetKey(KeyCode.UpArrow)	|| ((Input.acceleration.z > -0.32/*fast senkrecht*/) && (Input.acceleration.z < -0.000000001)))
			{
				isRotated = doRot(KeyCode.UpArrow);
			}
			if (Input.GetKey(KeyCode.DownArrow)	|| (Input.acceleration.z < -0.7/*fast horizontal liegend*/))
			{
				isRotated = doRot(KeyCode.DownArrow);
			}
			if(!isRotated)
			{
				doRot(KeyCode.None);
			}
		}
//shot
		if (Input.GetKeyDown(KeyCode.Space))
		{
			doShot();
		}
//move
		if(!sw2_flyarounder.isFlyingAround())
		{
			if (Input.GetKey(KeyCode.LeftControl))
			{
				bool bWorldBoundaryReached = doMove(true);
			}
			else
			{
				#if !MOBILE_INPUT
				doMove(false);
				#endif
			}
		}
	}

	bool doRot(KeyCode rotDirection)
	{
		if(m_bTurningAround)return true;

		if(upDown != null)
		{
			int afterComma = 1;
			String txtUpDown = "";
			#if MOBILE_INPUT
			txtUpDown = ""+util.asString(Input.acceleration.x, afterComma)+" x "+util.asString(Input.acceleration.y, afterComma)+" x "+util.asString(Input.acceleration.z, afterComma)+" \n ";
			#endif
			txtUpDown += " Warp "+util.asString(moveSpeed, afterComma)+"";
			String arrow = rotDirection == KeyCode.UpArrow ? " ˄˄ " : (rotDirection == KeyCode.DownArrow ? " ˅˅ " : "");
			upDown.text = arrow + txtUpDown;
			if(rotDirection == KeyCode.UpArrow)
				upDown.style.main.textColor = Color.green;
			else if(rotDirection == KeyCode.DownArrow)
				upDown.style.main.textColor = Color.red;
			else
				upDown.style.main.textColor = Color.white;
		}

		Vector3 rot = new Vector3();
		Quaternion rotXwing = this.xwingTransform.localRotation;

		if(rotDirection == KeyCode.LeftArrow)				//x
		{
			rot.y  = -rotSpeed * Time.deltaTime;
			rotXwing.z += xwingRotSpeed * Time.deltaTime;
		}
		else if(rotDirection == KeyCode.RightArrow)
		{
			rot.y  = rotSpeed * Time.deltaTime;
			rotXwing.z -= xwingRotSpeed * Time.deltaTime;
		}
		else if(rotDirection == KeyCode.UpArrow)			//y
		{
			rot.x  = -rotSpeed * Time.deltaTime;
			rotXwing.x -= xwingRotSpeed * Time.deltaTime;
		}
		else if(rotDirection == KeyCode.DownArrow)
		{
			rot.x  = rotSpeed * Time.deltaTime;
			rotXwing.x += xwingRotSpeed * Time.deltaTime;
		}
		else if(rotDirection == KeyCode.None)
		{
			bool rotXwinChanged = false;
			if( rotXwing.z > 0.000001f)
			{
				rotXwinChanged = true;
				rotXwing.z -= xwingRotSpeed * Time.deltaTime;
				if (rotXwing.z < -0.000001f)
					rotXwing.z = 0.0000000000f;
			}
			else if( rotXwing.z < -0.000001f)
			{
				rotXwinChanged = true;
				rotXwing.z += xwingRotSpeed * Time.deltaTime;
				if (rotXwing.z > 0.000001f)
					rotXwing.z = 0.0000000000f;
			}

			if( rotXwing.x > 0.000001f)
			{
				rotXwinChanged = true;
				rotXwing.x -= xwingRotSpeed * Time.deltaTime;
				if (rotXwing.x < -0.000001f)
					rotXwing.x = 0.0000000000f;
			}
			else if( rotXwing.x < -0.000001f)
			{
				rotXwinChanged = true;
				rotXwing.x += xwingRotSpeed * Time.deltaTime;
				if (rotXwing.x > 0.000001f)
					rotXwing.x = 0.0000000000f;
			}
			if(rotXwinChanged)
				xwingTransform.localRotation = rotXwing;
			return false;
		}
		else
		{
			print("meMover:doRot WRN: !rotDir("+rotDirection+")");
			return false;
		}
		
		thisTransform.Rotate(rot);
		{
			if( rotXwing.z > xwingRotMax) rotXwing.z = xwingRotMax;
			if( rotXwing.z < -xwingRotMax)rotXwing.z = -xwingRotMax;
			if( rotXwing.x > xwingRotMax) rotXwing.x = xwingRotMax;
			if( rotXwing.x < -xwingRotMax)rotXwing.x = -xwingRotMax;

			xwingTransform.localRotation = rotXwing;
		}
		return true;
	}
	public void doShot()
	{
		meshooter.doShoot();
	}
	public bool doMove(bool accelerating)
	{
		{	//TODO: check if this is working also on mobile correctly
			speedImage.fillAmount = (this.moveSpeed / this.moveSpeedMax);
			speedImage.color = Color.Lerp(Color.green, Color.red, speedImage.fillAmount);
		}
		
		if(accelerating)
		{
			if (moveSpeed < moveSpeedMax)
			{
				moveSpeed += (moveAcceleration * (Time.deltaTime/*0.05 - 0.25*/ * 20f));
			}
			
			for(int i=0;i<afterBurners.Count;i++)
			{
				ParticleSystem particleSystem = afterBurners[i];
				if( particleSystem.isPaused || particleSystem.isStopped)
				{
					if(i == 0)
					{
						AudioSource audioSource = this.GetComponent<AudioSource>();
						audioSource.clip = this.audioSpeedUp;
						audioSource.Play();
					}

					particleSystem.Play();
					particleSystem.gameObject.SetActive(true);
				}
			}
		}
		else//=if(!accelerating)
		{
			for(int i=0;i<afterBurners.Count;i++)
			{
				ParticleSystem particleSystem = afterBurners[i];
				if( particleSystem.isPlaying)
				{
					if(i == 0)
					{
						AudioSource audioSource = this.GetComponent<AudioSource>();
						audioSource.clip = this.audioSpeedDown;
						audioSource.Play();
					}
					particleSystem.Pause();
					particleSystem.gameObject.SetActive(false);
				}
			}

			moveSpeed -= (moveAcceleration*3.0f);
			if(moveSpeed < 0.0f)
			{
				moveSpeed = 0.0f;
				return false;
			}
		}

		if(!m_bTurningAround)
		{
			thisTransform.position += thisTransform.forward * Time.deltaTime * moveSpeed;
			{
				Vector3 pos = thisTransform.position;

				if( pos.x > posMax)
					pos.x = posMax;
				if( pos.x < -posMax)
					pos.x = -posMax;
				
				if( pos.y > posMax)
					pos.y = posMax;
				if( pos.y < -posMax)
					pos.y = -posMax;
				
				if( pos.z > posMax)
					pos.z = posMax;
				if( pos.z < -posMax)
					pos.z = -posMax;

				if(!thisTransform.position.Equals(pos))
				{
					thisTransform.position = pos;
					turnAround(pos);
					return true;
				}
			}
		}
		return false;
	}
	private bool m_bTurningAround = false;
	private float turnAroundTime = 3f;
	private void turnAround(Vector3 pos)
	{
		if(m_bTurningAround)return;
		m_bTurningAround = true;
		
		//thisTransform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));	//that is too instant

		Vector3[] path = new Vector3[2];
		path[0] = thisTransform.position;
		path[1] = thisTransform.position * 0.9f;
		GUITween.MoveTo(gameObject, GUITween.Hash("path", path , "time", turnAroundTime, "easetype", GUITween.EaseType.easeInOutSine));
		GUITween.RotateBy(gameObject, GUITween.Hash("y", -0.5/*1 = pos: rot east, 1=360*/, "time", turnAroundTime, "easeType", GUITween.EaseType.easeInOutSine));
		//GUITween.RotateBy(gameObject, GUITween.Hash("x", -0.5/*1 = pos: rot east, 1=360*/, "time", turnAroundTime, "easeType", GUITween.EaseType.easeInOutSine));
		
		StartCoroutine(enableManualRotAsync(pos));
	}
	private IEnumerator enableManualRotAsync(Vector3 pos)
	{
		yield return new WaitForSeconds(turnAroundTime);
		m_bTurningAround = false;
	}
	private IEnumerator turnAroundAsync(Vector3 pos)
	{
		Vector3 rotFrom = thisTransform.eulerAngles;
		Vector3 rotTo = -rotFrom;
		float delta = 2.2f;

		print("turnAroundAsync START");
		while(m_bTurningAround)
		{
			//thisTransform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
			delta += 2.2f;
			Vector3 rot = new Vector3(
				Mathf.LerpAngle(rotFrom.x, rotTo.x, delta),
				Mathf.LerpAngle(rotFrom.y, rotTo.y, delta),
				Mathf.LerpAngle(rotFrom.z, rotTo.z, delta));
			
			thisTransform.eulerAngles = rot;

			float diffx = Math.Abs(rotFrom.x - rotTo.x);
			float diffy = Math.Abs(rotFrom.y - rotTo.y);
			float diffz = Math.Abs(rotFrom.z - rotTo.z);

			float diff = diffx + diffy + diffz;

print("turnAroundAsync IN diff:"+diff);
			if(diff < 0.1f)
			{
				m_bTurningAround = false;
			}
			else
			{
				yield return new WaitForSeconds(0.15f);
			}
		}
	}

	public float getMoveSpeed()
	{
		return moveSpeed;
	}

	/*void OnGUI ()
	{
		GUILayout.Label(speedImage.sprite);
	}*/
}
