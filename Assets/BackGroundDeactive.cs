using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundDeactive : MonoBehaviour
{
    public SpriteRenderer BG;
    void Start()
    {
        BG = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GameManager.GameScore > 1000){      //1000점 넘으면 배경 비활성화
                BG.color = new Color32(255,255,255,0);
            }
    }
}
