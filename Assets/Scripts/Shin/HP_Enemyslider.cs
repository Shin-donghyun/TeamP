using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Enemyslider : MonoBehaviour
{
	[SerializeField]
	private Slider Hp_slider;

	private float health = 100;
	private float CurHp = 100;
	public bool isInvulnerable = false;

	public GameObject closeimg;
	public GameObject cav;

    void Start()
    {
		Hp_slider.value = (float)CurHp / (float)health;
    }

    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
			if (health > 0)
			{
				CurHp -= 10;
			}
            else
            {
				CurHp = 0;
            }
        }
		HandleHp();
		if (CurHp <= 0)
		{
			Time.timeScale = 1;
			closeimg.SetActive(true);
			cav.SetActive(false);
		}
	}
	private void HandleHp()
    {
		Hp_slider.value = Mathf.Lerp(Hp_slider.value, (float)CurHp / (float)health, Time.deltaTime * 10);
    }
    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		CurHp -= damage;

		HandleHp();
		if (health <= 0)
		{
			Time.timeScale = 1;
			closeimg.SetActive(true);
			cav.SetActive(false);
		}
	}
}

