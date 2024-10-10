using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子弹移动
public class BulletController : MonoBehaviour
{

    Rigidbody2D rbody;
    // Start is called before the first frame update
    void Awake()
    {
        rbody=GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//子弹移动
    public void Move(Vector2 moveDirection,float moveForce){
        rbody.AddForce(moveDirection*moveForce);
    }

    //碰撞检测
    void OnCollisionEnter2D(Collision2D other){
        EnemyController ec=other.gameObject.GetComponent<EnemyController>();
        if(ec!=null){
            ec.Fixed();//修复敌人
        }
        Destroy(this.gameObject);
    }
}
