using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///伤害陷阱
///</summary>
public class DameAgeArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerStay2D(Collider2D other){
        PlayerController pc=other.GetComponent<PlayerController>();
        if(pc!=null){
            pc.ChangeHealth(-1);
        }
    }
}
