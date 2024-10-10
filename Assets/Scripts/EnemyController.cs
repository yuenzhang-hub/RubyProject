using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///敌人移动脚本
public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // 移动速度
    public float changeDirectionTime = 2f; // 改变方向的时间间隔
    private float changeTimer; // 改变方向计时器
    private Rigidbody2D rbody;
    private Vector2 moveDirection; // 移动方向
    private Animator anim;
    private bool isFixed;//是否修复

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        changeTimer = changeDirectionTime;
        isFixed=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFixed){return;}//如果被修复就不执行
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            // 随机选择新的方向
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            moveDirection = new Vector2(randomX, randomY).normalized;
            changeTimer = changeDirectionTime;
        }

        // 移动敌人
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        // 设置动画参数
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }

    // 碰撞检测
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }
    //敌人修复
    public void Fixed(){
        isFixed=true;
        rbody.simulated=false;
        anim.SetTrigger("fix");//播放修复动画
    }
}