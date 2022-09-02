using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_2 : MonoBehaviour
{
    public GameObject escPopup;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        GameManager.isPause = true;
        escPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscBtnClick();
        }
    }

    public void EscBtnClick()
    {
        escPopup.SetActive(true);
    }

    public void YesBtnClick()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NoBtnClick()
    {
        escPopup.SetActive(false);
    }

    public void PlayBtnClick()
    {
        SceneManager.LoadScene("3_InGame");
    }

    public void RankingBtnClick()
    {
        SceneManager.LoadScene("6_Ranking");
    }

    public void HowToBtnClick()
    {
        SceneManager.LoadScene("7_HowTo");
    }
}
