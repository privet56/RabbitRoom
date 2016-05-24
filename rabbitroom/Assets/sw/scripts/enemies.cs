using UnityEngine;
using System.Collections;

public class enemies : MonoBehaviour
{
	public GameObject[] m_enemies = new GameObject[0];
	private int currentEnemy = 0;

	public GameObject m_firePrefab = null;
	public GameObject m_shotPrefab = null;
	public GameObject startPos = null;
	public GameObject xwing = null;
	public float speed = 9.9f;
	public float fireSpeed = 3.5f;
	public float enemiesCreationSpeed = 4.9f;
	public int enemyIndex = 0;

	void Start ()
	{
		StartCoroutine(newEnemy());
	}
	
	/*void Update ()
	{
	
	}*/

	private IEnumerator newEnemy()
	{
		while(true)
		{
			yield return new WaitForSeconds(enemiesCreationSpeed);
			GameObject go = GameObject.Instantiate(m_enemies[currentEnemy], this.transform.position, m_enemies[currentEnemy].transform.rotation) as GameObject;
			{
				enemy e = go.GetComponent<enemy>();
				e.m_shotPrefab = this.m_shotPrefab;
				e.m_firePrefab = this.m_firePrefab;
				e.startPos = this.startPos;
				e.xwing = this.xwing;
				e.speed = this.speed;
				e.fireSpeed = this.fireSpeed;
				e.enemyIndex = enemyIndex++;
				//BoxCollider bc = go.GetComponent<BoxCollider>();
				//print (bc);

			}
			currentEnemy++;
			if( currentEnemy >= m_enemies.Length)
				currentEnemy = 0;
		}
	}
	public void onDied(string isDied)
	{
		this.xwing = null;
	}
}
