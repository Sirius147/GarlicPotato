using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    public float gameSpeed = 1; //게임 전체 속도 조절
    public bool isPlay = false;
    public GameObject playBtn;
    public bool isEmptySpaceSpawn = false;


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
}
