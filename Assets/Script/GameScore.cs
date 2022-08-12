using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScore : MonoBehaviour
{
    public TextMeshProUGUI score;

    void Update(){
        score.text = GameManager.instance.GameScore.ToString();
    }
}
