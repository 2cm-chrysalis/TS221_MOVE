using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;

public class ParentCardController : MonoBehaviour
{

    public TMP_InputField cardMessage;
    public TMP_InputField cardPoint;

    /// <summary>
    /// 파이어베이스에 메시지와 포인트 정보를 보냄.
    /// </summary>
    public void sendMessage()
    {
        string content = cardMessage.text;

        int point = 0;
        if (System.Int32.TryParse(cardPoint.text, out point) && point!=0)
        {
            cardPoint.text = cardMessage.text = "";
            //데이터 보내기.
        }
        Debug.Log("Please set point");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
