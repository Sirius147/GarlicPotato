using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager_4 : MonoBehaviour
{
    public Text UserScoreTxt;
    public Text UserRankTxt;

    List<int> scoreList = new List<int>();
    List<String> nameList = new List<String>();
    public static int nowRank;
    private String userName = "<color=\"red\">POTATO</color>";

    // Start is called before the first frame update
    void Start()
    {
        GameManager.isPause = true;
        UserScoreTxt.text = $"{GameManager.GameScore}";
        ArrayRanking();
        if (nowRank < 10)
        {
            UserRankTxt.text = $"{nowRank + 1}등";
        }
        else if (nowRank > 9)
        {
            UserRankTxt.text = $"순위권 밖";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterBtnClick();
        }
    }

    public void EnterBtnClick()
    {
        if (nowRank < 10)
        {
            SceneManager.LoadScene("5_InputName");
        }
        else if (nowRank > 9)
        {
            SceneManager.LoadScene("6_Ranking");
        }
    }

    public void ArrayRanking()
    {
        //랭킹 가져오기
        for (int i = 0; i < 10; i++)
        {
            scoreList.Insert(i, PlayerPrefs.GetInt(i + "BestScore"));
            nameList.Insert(i, PlayerPrefs.GetString(i.ToString() + "BestName"));
        }
        //순위 정렬
        for (nowRank = 0; nowRank < 10; nowRank++)
        {
            if (scoreList[nowRank] < GameManager.GameScore)
            {
                break;
            }
        }
        scoreList.Insert(nowRank, GameManager.GameScore);
        nameList.Insert(nowRank, userName);

        //순위권 밖 처리
        if (scoreList.Count > 10)
        {
            for (int i = 10; i < scoreList.Count; i++)
            {
                scoreList.RemoveAt(10);
                nameList.RemoveAt(10);
            }
        }
        //기기에 랭킹 저장
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", scoreList[i]);
            if (nameList[i] == "")
            {
                PlayerPrefs.SetString(i.ToString() + "BestName", "------");
            }
            else
            {
                PlayerPrefs.SetString(i.ToString() + "BestName", nameList[i]);
            }
        }
    }
}
