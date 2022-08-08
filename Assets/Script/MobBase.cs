using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBase : MonoBehaviour
{
    public Vector2 StartPosition;  
    private void OnEnable() {       //오브젝트가 생성되면 활성화 됨
        transform.position = StartPosition;     //Mob이 나타나기 전 시작할 위치 지정
    }

    void Update()
    {
        if(GameManager.instance.isPlay){
            transform.Translate(Vector2.left * Time.deltaTime * GameManager.instance.gameSpeed);

            if(transform.position.x < -10){      //제일 왼쪽으로 이동 시 비활성화
                gameObject.SetActive(false);
            }
        }   
    }
}
