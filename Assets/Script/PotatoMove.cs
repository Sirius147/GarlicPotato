using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoMove : MonoBehaviour
{   
    public float jumpForce;
    public Animator anim;
    int jumpStack=0;
    public float movingSpeed =0.1f;  //제자리로 돌아가는 속도 변수 (public)
    
    Rigidbody2D rb;

    //김지은
    public GameObject player;
    public GameManager manager;
    public GroundScroller groundScroller;
    public int maxlife=3;
    int life=3;
    public bool isUnBeatTime=false;
    public bool isBoostTime = false;
    public SpriteRenderer potatoRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        
        //김지은
        potatoRenderer=GetComponent<SpriteRenderer>();
        life=maxlife;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 target=new Vector2(transform.position.x,transform.position.y);
        if (!GameManager.isPause) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((jumpStack < 2) && (!anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide")))
                {   //slide상태가 아니고 점프스택이 2회보다 작을 때
                    anim.SetTrigger("toJump");
                    rb.velocity = Vector2.up * jumpForce;
                    jumpStack += 1;
                }
                /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide"))
                {
                    anim.SetTrigger("toJump");
                    rb.velocity = Vector2.up*jumpForce;
                    jumpStack+=1;
                }*/   //슬라이드 코드 변경으로 임시 주석처리



            }

            if (Input.GetMouseButton(1)/*Input.GetMouseButtonDown(1)*/) //우클릭 홀드
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoRun"))   //run 상태일 때만 슬라이드로 전환
                {
                    anim.SetTrigger("toSlide");

                }

            }

            if(Input.GetMouseButtonUp(1)) // 홀드 해제시 
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide"))   //슬라이드 상태에서 런 상태로 전환
                {
                    anim.SetTrigger("toRun");

                }
            }
            if (transform.position.y < -3.3)
            {
                Debug.Log("game over");
                GameManager.instance.GameOver();        //game over 씬으로 전환 (지금은 임시로 play버튼 재활성화)

            }
            if (transform.position.y < -3.3)
            {
                Debug.Log("game over");
                GameManager.instance.GameOver();      //game over 씬으로 전환 (지금은 임시로 play버튼 재활성화)

            }
            if(transform.position.x <-3.5)       // player 포지션이 뒤로 밀려나는 경우에만
            { 
                transform.Translate(Vector3.right*Time.deltaTime*movingSpeed);   // 천천히 위치조정
            }
            
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)   //테이블과 충돌했을 때 실행 ( 공중에서 무한점프를 방지하고 애니메이션전환 )
    {
        if(col.gameObject.tag=="Ground")   //collider 이름이 Ground 인 물체와 부딪힐 때 실행
        {
            //Debug.Log("OnCollision worked");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoJump"))
            { //점프 상태에서 바닥에 착지할 때 점프스택을 0으로 바꾸고 애니메이션도 run으로 변경
                jumpStack=0;
                anim.SetTrigger("toRun");
            }

        }
        
        
    }
    void OnTriggerEnter2D(Collider2D col)
	{       //collider 이름이 Mob인 물체를 통과했을 때 실행
	    if (col.tag == "Mob" && !isUnBeatTime)    //OnTrigger는 tag로 줄일수있다.   OnCollision은 col.gameObject.tag  (compartag)
	    {
            //김지은
            life--;
            manager.UpdateLifeIcon(life);
            
            if (life == 0)
            {
                manager.GameOver();
            }
            else
            {
                isUnBeatTime=true;
                StartCoroutine("UnBeatTime");
               // manager.RespawnPlayer(); //플레이어 복귀
            }
            //gameObject.SetActive(false);  //비활성화
	    }

        // 아이템 먹었을 때
        if (col.tag == "Item_Heart"){
            //Debug.Log("heart touch");
            if(life<3){
            life++;
            manager.PlusLifeIcon(life);
            }
        }

        if(col.tag == "Item_Garlic"){
            //Debug.Log("garlic touch");
            isUnBeatTime = true;
            if(isBoostTime){
                StopCoroutine("BoostTime");
            }
                
            isBoostTime = true;
            StartCoroutine("BoostTime");
            isBoostTime = false;
        }
	}

    IEnumerator UnBeatTime(){
        int countTime=0;
        while(countTime<10){
            if(countTime%2==0)
                potatoRenderer.color=new Color32(255,255,255,90);
            else
                potatoRenderer.color=new Color32(255,255,255,180);
            yield return new WaitForSeconds(0.2f);
            
            countTime++;
        }
        potatoRenderer.color=new Color32(255,255,255,255);
        isUnBeatTime=false;
        yield return null;
    }
    
    IEnumerator BoostTime(){
        float temp_speed = GameManager.instance.gameSpeed;
        GameManager.instance.gameSpeed = 16;        // 부스터 속도

        yield return new WaitForSeconds(3f);
        GameManager.instance.gameSpeed = temp_speed;
        isUnBeatTime=false;
    }
}
