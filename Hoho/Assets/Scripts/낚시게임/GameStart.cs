using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStart : MonoBehaviour
{

    /// <summary>
    /// 게임 시작 시 false, 3초 후 start로 바뀜. 
    /// </summary>
    public static bool isStarted=false;

    [Tooltip("화면 중앙의 3, 2, 1, start 글씨")]
    public TextMeshProUGUI startText;
    
    private float countTime;


    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStarted)
        {
            countTime += Time.deltaTime;
            updateTime();
        }
    }

    /// <summary>
    /// 시간에 따라 3, 2, 1을 보여주고 
    /// </summary>
    private void updateTime()
    {
        if (countTime < 1.0f)
        {
            startText.text = "3";
        }

        else if (countTime < 2.0f)
        {
            startText.text = "2";
        }
        else if (countTime < 3.0f)
        {
            startText.text = "1";
        }

        else
        {
            startText.text = "Start!";
            Invoke("disableText", 1.0f);
            isStarted = true;
        }

        return; 
    }

    /// <summary>
    /// text 안 보이게 하기.
    /// </summary>
    private void disableText()
    {
        startText.gameObject.SetActive(false);
    }
}
