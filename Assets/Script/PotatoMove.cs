using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMove : MonoBehaviour
{   
    public float jumpForce;
    public float dJumpForce;
    public Animator anim;
    bool isJumping=false;
    //int countCall=0;
    
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
                
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))    //Left mouse button is clicked
        {
            if(isJumping==false)
            {
                anim.SetTrigger("toJump");
                isJumping=true;
                rb.velocity = Vector2.up * jumpForce;
            }
            
            
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isJumping ==false)
            {
                anim.SetTrigger("toDjump");
                isJumping=true;
                rb.velocity = Vector2.up*dJumpForce;
            }
            
        }
        if(Input.GetMouseButtonDown(1)) //우클릭
        {
            if(isJumping==false)
            {
                anim.SetTrigger("toSlide");  
                anim.SetTrigger("toRun");
                           
            }
        }
    
    }
    /*
    void OnCollisionEnter2D(Collision2D col) 
    {
        countCall+=1;
    if ((col.transform.name == "Map_table1") || (col.transform.name =="Map_table2") || (col.transform.name=="Map-table3")) 
    {

        isJumping = false;
        if(countCall>1)
        {   
            anim.SetTrigger("toRun");
        }

    }
    }*/
    void OnCollisionEnter2D(Collision2D col)
    {
        
        // if(col.gameObject.tag=="Ground")   콜리젼 함수 실행시 그라운드에 의해서인지
        // 장애물에 의해서인지 구분하기 위한 조건  --> 동작이 안되서 보류
        
            
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoJump") || anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoDjump"))
        {
            isJumping=false;
            anim.SetTrigger("toRun");
        }
        
        
    }
    
}
