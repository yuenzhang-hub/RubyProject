using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///草莓碰撞检测的类
///</summary>
public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
///<summary>
///碰撞检测
///</summary>
    void OnTriggerEnter2D(Collider2D other){
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc!=null)
        {
            if(pc.MyCurrentHealth<pc.MyMaxHealth){pc.ChangeHealth(1);
            Destroy(this.gameObject);
            }
        }
    }

}
