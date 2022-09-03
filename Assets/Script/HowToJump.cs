using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToJump : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;

    public float jumpForce;
    int jumpStack = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (jumpStack < 1)
            {   //slide상태가 아니고 점프스택이 2회보다 작을 때
                anim.SetTrigger("toJump");
                rb.velocity = Vector2.up * jumpForce;
                jumpStack += 1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)   //테이블과 충돌했을 때 실행 ( 공중에서 무한점프를 방지하고 애니메이션전환 )
    {
        if (col.gameObject.tag == "Ground")   //collider 이름이 Ground 인 물체와 부딪힐 때 실행
        {
            //Debug.Log("OnCollision worked");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoJump"))
            { //점프 상태에서 바닥에 착지할 때 점프스택을 0으로 바꾸고 애니메이션도 run으로 변경
                jumpStack = 0;
                anim.SetTrigger("toRun");
            }
        }
    }
}
