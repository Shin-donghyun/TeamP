using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movechar : MonoBehaviour
{
    public float movePower = 1.0f;
    public float jumpPower = 1.0f;
    public GameObject Tutorialimg;
    bool isPause;

    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;

    Vector3 movement;
    
    //2단점프
    bool isJumping = false;
    int JumpCount = 0;
   
    private float curTime;
    public float attack_coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        if (curTime <= 0)
        {
            //Attack
            if (Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    Debug.Log(collider.tag);
                }

                anim.SetTrigger("doAttack");
                curTime = attack_coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown("c"))
        {
            if (JumpCount < 2)
            {
                isJumping = true;
                JumpCount++;
            }
        }
        if (Input.GetButton("Horizontal"))
        {
            sprite.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        JumpCount = 0;
    }
    void FixedUpdate()
    {
        Move();
        Jump();
       
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("isMove", false);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            //anim.SetInteger("Direction", -1);
            anim.SetBool("isMove", true);    
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            //anim.SetInteger("Direction", 1);
            anim.SetBool("isMove", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;

    }
    void Dash()
    {

    }
    public void TutorialBtn()
    {
        Tutorialimg.SetActive(false);
        Time.timeScale = 1;
    }
}
