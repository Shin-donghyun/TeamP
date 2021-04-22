using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Enemyslider : MonoBehaviour
{
    
    public Slider Enemy_HealthBar;

    private float maxHP = 50;
    private float curHP = 50;

    public GameObject closeimg;
    public GameObject cav;

    private int imgCount = 0;
    void Start()
    {
        Enemy_HealthBar.value = (float)curHP / (float)maxHP;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Hp_dmg(10);
        }
        
        HandleHp();
        
        if (curHP == 0)
        {
            Time.timeScale = 0;
            closeimg.SetActive(true);
            cav.SetActive(false);
        }
    }
    public void Hp_dmg(int dmg)
    {
        curHP -= dmg;
    }
    public void HandleHp()
    {
        Enemy_HealthBar.value = (float)curHP / (float)maxHP;
    }
}
