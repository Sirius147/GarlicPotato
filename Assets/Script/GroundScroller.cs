using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public SpriteRenderer[] tiles;  // 화면에 표시될 바닥 타일 담는 배열
    public Sprite[] groundImg;  // 여러 종류의 바닥 타일을 담는 배열
    SpriteRenderer temp;
    //public bool isEmptySpaceSpawn;
    private int front_plat_table_num;
    private int right_table_num;
    private int empty_space_num;
    private int left_table_num;
    private int back_plat_table_num;

    void Start()
    {
        temp = tiles[0];
        front_plat_table_num = 0;
        right_table_num = 0;
        empty_space_num = 0;
        left_table_num = 0;
        back_plat_table_num = 0;
    }


    void Update()
    {
        if(GameManager.instance.isPlay){
            for(int i=0; i<tiles.Length; i++){
                if(tiles[i].transform.position.x <= -7){    // 바닥 타일이 제일 왼쪽으로 갔을 때
                    for(int q=0; q<tiles.Length; q++){
                        if(temp.transform.position.x < tiles[q].transform.position.x)
                            temp = tiles[q];                // temp는 맨 뒤 타일을 가리키게 됨
                    }
                    tiles[i].transform.position = new Vector2(temp.transform.position.x + 1, -2.7f);    // 바닥 타일을 제일 오른쪽으로 보냄
                    GameManager.GameScore++;                           // 오른쪽으로 보낸 타일의 개수를 셈
                    GameManager.instance.gameSpeed += 0.01f;
                    tiles[i].GetComponent<BoxCollider2D>().enabled = true;      // 콜라이더 꺼져있으면 다시 활성화

                    // 바닥 타일에 씌울 이미지를 정하는 과정
                    // 공백 타일은 다음 규칙을 따라 나온다
                    // plat table 3개 -> right table 1개 -> 공백 칸 2~4개 -> left table 1개 -> plat table 3개
                    if(front_plat_table_num + left_table_num + empty_space_num + right_table_num + back_plat_table_num == 0){
                        GameManager.instance.isHoleSpawn = false;
                        int randomblock = Random.Range(0, 10)/9;
                        switch(randomblock){
                            case 0:      //0: 테이블이 이어지는 경우    0 1 2 3 4 5 6 7 8- 90%
                                tiles[i].sprite = groundImg[0];
                                break;
                            case 1:      //1: empty space가 나오는 경우 9 - 10%
                                GameManager.instance.isHoleSpawn = true;
                                tiles[i].sprite = groundImg[0];         //앞쪽 plat 블럭1개 나올 차례
                                front_plat_table_num = 2;
                                right_table_num = 1;
                                empty_space_num = Random.Range(2, 5);
                                left_table_num = 1;
                                back_plat_table_num = 3;
                                break;
                        }
                    }
                    else if(front_plat_table_num != 0){  //앞쪽 plat 블럭 나올 차례
                        tiles[i].sprite = groundImg[0];
                        front_plat_table_num--;
                    }
                    else if(right_table_num != 0){       //right table 블럭 나올 차례
                        tiles[i].sprite = groundImg[1];
                        right_table_num--;
                    }
                    else if(empty_space_num != 0){       //공백 블럭 나올 차례
                        tiles[i].sprite = groundImg[3];
                        empty_space_num--;
                        //콜라이더 일시적으로 비활성화
                        tiles[i].GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else if(left_table_num != 0){        //왼쪽 블럭 나올 차례
                        tiles[i].sprite = groundImg[2];
                        left_table_num--;
                    }
                    else if(back_plat_table_num != 0){   //뒤쪽 plat 블럭 나올 차례
                        tiles[i].sprite = groundImg[0];
                        back_plat_table_num--;
                    }
                }
            }
            for(int i=0; i<tiles.Length; i++){      // 바닥 타일을 왼쪽으로 움직임
                tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * GameManager.instance.gameSpeed);
            }
        }
    }
}
