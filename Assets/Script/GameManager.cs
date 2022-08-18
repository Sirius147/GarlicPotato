using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  
    
    #region instance
    public static GameManager instance;
    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

      //김지은
    public Image[] lifeImage; 
    public GameObject player;
    public GroundScroller groundScroller;
    
    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    public float gameSpeed = 1; //게임 전체 속도 조절
    public bool isPlay = false;
    public GameObject playBtn;
    public bool isHoleSpawn = false;
    public int GameScore = 0;

    public void PlayBtnClick(){
        playBtn.SetActive(false);
        isPlay = true;
        onPlay.Invoke(isPlay);
    }

    public void GameOver(){
        playBtn.SetActive(true);
        isPlay = false;
        onPlay.Invoke(isPlay);
    }
    //김지은
    public void UpdateLifeIcon(int life){
        //UI Life 모두 안 보이게 함
        for(int index=0;index<3;index++){
            //lifeImage[index].SetActive(false);
            lifeImage[index].color=new Color(1,1,1,0);
        }
        //UI Life active  남아있는 개수대로만 다시 킴
        for(int index=0;index<life;index++){
            //lifeImage[index].SetActive(true);
            lifeImage[index].color=new Color(1,1,1,1);
            //color(r,g,b,a)에서 네번째 매개변수가 투명도이다.
        }
    }

    //public void RespawnPlayer(){
        
        //groundScroller.tiles.SetActive(false);

        //Invoke("RespawnPlayerExe",2f);  //2초 뒤에 이 로직 실행
    //}

    //void RespawnPlayerExe(){  //장애물 부딪히고 실행
        //groundScroller.tiles.SetActive(true);
       // player.SetActive(true);
       //투명, 무적
       //Debug.Log("잠시 멈춤");
    //}
}
