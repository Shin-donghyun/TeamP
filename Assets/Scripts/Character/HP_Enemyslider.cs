using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Enemyslider : MonoBehaviour
{
    
    public Slider Enemy_HealthBar;

    private int maxHP = 50;
    private int curHP = 50;

    void Start()
    {
        Enemy_HealthBar.value = (int)curHP / (int)maxHP;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hp_dmg(10);
        }
        
        HandleHp();
    }
    public void Hp_dmg(int dmg)
    {
        curHP -= dmg;
    }
    public void HandleHp()
    {
        Enemy_HealthBar.value = (int)curHP / (int)maxHP;
    }
}
