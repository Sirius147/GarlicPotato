using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMove : MonoBehaviour
{   
    public float jumpForce;
    public Animator anim;
    int jumpStack=0;
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
        if(Input.GetMouseButtonDown(0))   
        {       
            if((jumpStack<2) && (!anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide"))) 
            {   //slide상태가 아니고 점프스택이 2회보다 작을 때
                anim.SetTrigger("toJump");
                rb.velocity = Vector2.up * jumpForce;
                jumpStack+=1;
            }
            
            
                
        }
        
        if(Input.GetMouseButtonDown(1)) //우클릭
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoRun"))   //run 상태일 때 우클릭하면 슬라이드 상태 유지
            {
                anim.SetTrigger("toSlide");  
                           
            }
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide")) // 우클릭을 한번 더 누르면 런 상태로 변경
            {
                anim.SetTrigger("toRun");
            }
            
        }
    
    }
    
    void OnCollisionEnter2D(Collision2D col)   //테이블과 충돌했을 때 실행 ( 공중에서 무한점프를 방지하고 애니메이션전환 )
    {
        
        if(col.gameObject.tag=="Ground")   //collider 이름이 Ground 인 물체와 부딪힐 때 실행
        {
            Debug.Log("OnCollision worked");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoJump"))
            { //점프 상태에서 바닥에 착지할 때 점프스택을 0으로 바꾸고 애니메이션도 run으로 변경
                jumpStack=0;
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
