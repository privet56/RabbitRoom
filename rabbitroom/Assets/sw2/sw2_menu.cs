using UnityEngine;
using System.Collections;

public class sw2_menu : MonoBehaviour
{
	public meMover m_mover = null;
	public meShooter m_shooter = null;

	public InstantGuiButton m_moverB1 = null;
	public InstantGuiButton m_moverB2 = null;

	void Start ()
	{
	
	}
	
	void Update ()
	{
#if MOBILE_INPUT
		bool pressed = m_moverB1.pressed || m_moverB2.pressed;
		m_mover.doMove(pressed);
#endif
	}

	public void OnRestart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	public void OnBack()
	{
		Application.LoadLevel(0);
	}
	public void OnFire()
	{
		m_shooter.doShoot();
	}
	public void OnMove()
	{
		
	}
}
