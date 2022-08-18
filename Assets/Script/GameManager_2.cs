using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        GameManager.isPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBtnClick()
    {
        SceneManager.LoadScene("3_InGame");
    }
}
