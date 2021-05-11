using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Enemyslider : MonoBehaviour
{

    public int health = 50;
	public bool isInvulnerable = false;

	public GameObject closeimg;
	public GameObject cav;

    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
			health -= 10;
        }
    }
    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 0)
		{
			closeimg.SetActive(true);
			cav.SetActive(false);
		}
	}
}

