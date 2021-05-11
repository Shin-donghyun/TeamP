using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    public HP_Enemyslider Hp_Enemy;
    public Slider slider;
    void Start()
    {
        slider.maxValue = Hp_Enemy.health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Hp_Enemy.health;
    }
}
