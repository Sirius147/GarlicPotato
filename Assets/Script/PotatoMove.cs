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
    void OnCollisionEnter2D(Collision2D col)   //테이블과 충돌했을 때 실행 ( 공중에서 무한점프를 방지하고 애니메이션전환 )
    {
        
        if(col.gameObject.tag=="Ground")   //collider 이름이 Ground 인 물체와 부딪힐 때 실행
        {
            Debug.Log("OnCollision worked");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoJump") || anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoDjump"))
            { //점프 상태에서 바닥에 착지할 때 점프상태를 false로 바꾸고 애니메이션도 run으로 변경
                isJumping=false;
                anim.SetTrigger("toRun");
            }
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D col)
	{       //collider 이름이 Mob인 물체를 통과했을 때 실행
	    if (col.tag == "Mob")    //OnTrigger는 tag로 줄일수있다.   OnCollision은 col.gameObject.tag  (compartag)
	    {
	        Debug.Log("OnTriggerEnter2D");  
	    }
	}
    
}
