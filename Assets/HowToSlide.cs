using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToSlide : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)/*Input.GetMouseButtonDown(1)*/) //우클릭 홀드
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoRun"))   //run 상태일 때만 슬라이드로 전환
            {
                anim.SetTrigger("toSlide");

            }

        }

        if (Input.GetMouseButtonUp(1)) // 홀드 해제시 
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PotatoSlide"))   //슬라이드 상태에서 런 상태로 전환
            {
                anim.SetTrigger("toRun");

            }
        }
    }
}
