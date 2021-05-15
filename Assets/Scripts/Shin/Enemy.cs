﻿using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //체력
    [SerializeField]
    private Slider Hp_slider;

    private int health = 100;
    private int CurHp = 100;

    public GameObject BlackScreen;
    public GameObject tutorialout;

    void Start()
    {
        Hp_slider.maxValue = health;
    }
    private void HandleHp()
    {
        Hp_slider.value = health;
        //Hp_slider.value = Mathf.Lerp(Hp_slider.value, (float)health, Time.deltaTime * 10);
    }
    public void TakeDamage(int damage)
    {   
        anim.SetTrigger("HitMotion");
        health -= damage;
        HandleHp(); 
        
        if (health <= 0) {

            Time.timeScale = 0;
            BlackScreen.SetActive(true);
            tutorialout.SetActive(false);
        } 
    }
    //
    //플레이어 따라다니기
    public Transform player;
    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
   
    //
    Animator anim;
    [SerializeField]
    private Transform playerTransform;

    //[SerializeField]
    //private float timer = 0.0f;

    public const float value = 0.38f;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Think());
    }
    //
    public Transform enemy_pos;
    public Vector2 TailSize;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(enemy_pos.position, TailSize);
    }
    //
    IEnumerator Spear_Wave()
    {
        SpearEnergy wave = ObjectPool.SpearGetObject();
       

        wave.transform.position = new Vector3(0.44f, 0f , 0);
        
        //wave = new SpearEnergy();//메모리에 새로 생성.
        yield return new WaitForSeconds(1.0f);
    }
    

    IEnumerator Tail_Wave()
    {
        Vector2 vec = playerTransform.position;
        float x = vec.x;
        Tail_Wave wave = ObjectPool.TailGetObject();
        if (vec.x <= -5)
        {
            wave.transform.position = new Vector3(x + 1.238f, -0.66f, 0);
        }
        else if (vec.x >= 0.35f)
        {
            wave.transform.position = new Vector3(x + 0.238f - 1.0f, -0.66f, 0);
        }
        else
        {
            wave.transform.position = new Vector3(x + 0.238f, -0.66f, 0);
        }
            yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Tail_Effect()
    {
        Vector2 vec = playerTransform.position;
        float x = vec.x;

        Tail_Effect effect = ObjectPool.EffectGetObject();
        if (vec.x <= -5)
        {
            effect.transform.position = new Vector3(x + 1.1f, -0.56f, 0);
        }
        else if (vec.x >= 0.35f)
        {
            effect.transform.position = new Vector3(x - 0.9f, -0.56f, 0);
        }
        else
        {
            effect.transform.position = new Vector3(x + 0.1f, -0.56f, 0);
        }
        StartCoroutine(Tail_Effect_Destory(effect));
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator Finger1_Wave()
    {
        Finger_Wave1 wave = ObjectPool.Finger1GetObject();
        wave.transform.position = new Vector3(0, -0.5f, 0);
        //wave = new SpearEnergy();//메모리에 새로 생성.
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Finger1_Effect()
    {
        Finger1_Effect effect = ObjectPool.f1EffectGetObject();
        effect.transform.position = new Vector3(-2.69f, -0.5f, 0);

        StartCoroutine(Effect_Destory(effect) );
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Finger2_Wave()
    {
        Finger_Wave2 wave = ObjectPool.Finger2GetObject();
        wave.transform.position = new Vector3(-2.55f, -3.58f, 0);
        //wave = new SpearEnergy();//메모리에 새로 생성.
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Finger2_Effect()
    {
        Finger2_Effect effect = ObjectPool.f2EffectGetObject();
        effect.transform.position = new Vector3(-2.54f, -0.52f, 0);

        StartCoroutine(Effect_Destory(effect));
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Tail_Effect_Destory(Tail_Effect war)
    {
        SpriteRenderer spr = war.GetComponent<SpriteRenderer>();

        Color temColor = spr.color;
        float a = spr.color.a;

        yield return null;

        while (a > 0.01f)
        {
            spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
            yield return null;
            a -= Time.deltaTime;

        }
        ObjectPool.EnQueueObject(war);
    }

    IEnumerator Effect_Destory(Finger1_Effect war)
    {
        SpriteRenderer spr = war.GetComponent<SpriteRenderer>();
        Color temColor = spr.color;
        float a = spr.color.a;

        yield return null;

        while (a > 0.01f)
        {
            spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
            yield return null;
            a -= Time.deltaTime;

        }
        ObjectPool.EnQueueObject(war);
    }

    IEnumerator Effect_Destory(Finger2_Effect war)
    {
        SpriteRenderer spr = war.GetComponent<SpriteRenderer>();
        Color temColor = spr.color;
        float a = spr.color.a;

        yield return null;

        while (a > 0.01f)
        {
            spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
            yield return null;
            a -= Time.deltaTime;

        }
        ObjectPool.EnQueueObject(war);
    }


    IEnumerator Think()
    {
        yield return new WaitForSeconds(1.5f);

        int ranAction = Random.Range(0, 3);
        switch (ranAction)
        {
            //case 0:
            case 0:
                //손톱 공격 모션
                StartCoroutine(Finger_Mot());

                break;
            case 1:
                //창 공격 모션
                StartCoroutine(SpearWield_Mot());
                break;
            case 2:
                //꼬리 공격 모션
                StartCoroutine(Tail_Mot());
                break;
        }
    }
    IEnumerator Finger_Mot()
    {
        anim.SetTrigger("doFinger");
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
    IEnumerator SpearWield_Mot()
    {
        anim.SetTrigger("doSpear_Wield");
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
    IEnumerator Tail_Mot()
    {
        anim.SetTrigger("doTail");
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
}
