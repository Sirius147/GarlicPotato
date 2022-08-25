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
    //public GameObject player;
    //public GroundScroller groundScroller;
    
    //public delegate void OnPlay(bool isplay);
    //public OnPlay onPlay;
    //int maxlife=3;

    public float gameSpeed = 3; //게임 전체 속도 조절
    public bool isPlay = false;
    public bool isHoleSpawn = false;
    public static int GameScore = 0;

    public static bool isPause= false;
    public GameObject pausePopup;
    void Start()
    {
        pausePopup.SetActive(false);
        Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseBtnClick();
        }
    }

    public void Play()
    {
        Time.timeScale = 1;
        isPause = false;
        GameScore = 0;

        isPlay = true;
        Debug.Log("IsPlay true");
        //onPlay.Invoke(isPlay);
    }

    public void GameOver(){
        isPlay = false;
        StopAllCoroutines();
        //onPlay.Invoke(isPlay);
        SceneManager.LoadScene("4_GameOver");
    }

    public void PauseBtnClick()
    {
        Time.timeScale = 0;
        isPause = true;
        pausePopup.SetActive(true);
    }

    public void ContinueBtnClick()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
        isPause = false;
    }

    public void RestartBtnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("3_InGame");
    }

    public void HomeBtnClick()
    {
        SceneManager.LoadScene("2_Main");
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
   public void PlusLifeIcon(int life){
        //for(int index=life;index<=maxlife;index++){
            //lifeImage[index].SetActive(true);
            if(life<=3){
                for(int i=0;i<life;i++){
            lifeImage[i].color=new Color(1,1,1,1);
            //lifeImage[life].enabled=true;
            //color(r,g,b,a)에서 네번째 매개변수가 투명도이다.
                }
        }
    }
}
