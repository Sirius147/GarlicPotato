using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public bool isHoleSpawn = false;
    public static int GameScore = 0;

    public static bool isPause= false;

    void Start()
    {
        Play();
    }

    void Play()
    {
        isPause = false;
        GameScore = 0;

        isPlay = true;
        onPlay.Invoke(isPlay);
    }

    public void GameOver(){
        isPlay = false;
        onPlay.Invoke(isPlay);
        SceneManager.LoadScene("4_GameOver");
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
}
