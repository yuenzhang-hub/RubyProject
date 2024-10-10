using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30f; // 移动速度

    private int maxHealth = 5; // 最大生命值
    private int currentHealth; // 当前生命值

    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f; // 无敌时间
    private float invincibleTimer; // 无敌时间计时器
    private bool isInvincible; // 是否是无敌状态

    public GameObject bulletPrefab;//获取子弹

    //玩家方向
    private Vector2 lookDirection = new Vector2(1,0);
    Rigidbody2D rbody; // 刚体属性
    Animator anim;

    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector= new Vector2(moveX,moveY);
        if(moveVector.x !=0||moveVector.y!=0 ){
            lookDirection=moveVector;
        }
        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y",lookDirection.y);
        anim.SetFloat("Speed",moveVector.magnitude);

        Vector2 position = rbody.position;
        //position.x += moveX * speed * Time.deltaTime;
        //position.y += moveY * speed * Time.deltaTime;
        position += moveVector*speed*Time.deltaTime;
        rbody.MovePosition(position);

        // 无敌计时器
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false; // 倒计时结束后,取消无敌状态。
            }
        }
        //按下j键进行攻击
        if(Input.GetKeyDown(KeyCode.J)){
            anim.SetTrigger("Launch");//播放攻击动画
            GameObject bullet=Instantiate(bulletPrefab,rbody.position+Vector2.up*0.5f,Quaternion.identity);
            BulletController bc=bullet.GetComponent<BulletController>();
            if(bc!=null){
                bc.Move(lookDirection,300);
            }
        }
    }



    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
        // 修正生命值计算方式
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}