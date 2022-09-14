using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrgressController : MonoBehaviour
{
    [Tooltip("포인트동그라미")]
    public GameObject pointCircle;
    [Tooltip("진행률")]
    public TextMeshProUGUI pointRatio;
    [Tooltip("현재점수")]
    public TextMeshProUGUI currentPoint;
    [Tooltip("목표점수")]
    public TextMeshProUGUI goalPoint;
    [Tooltip("1~6단계, 점들")]
    public GameObject progressLevel;
    [Tooltip("보상목록표")]
    public GameObject rewardTable;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int parsePoint(TextMeshProUGUI point)
    {
        string tempNum = null;

        for (int i=0; i<point.text.Length; i++)
        {
            if (char.IsDigit(point.text[i]))
                tempNum += point.text[i];
        }
        return Int32.Parse(tempNum);
    }

    private void updateLevel(int lv)
    {
        for (int i=0; i<progressLevel.transform.childCount; i++)
        {
            if (i < lv)
            {
                progressLevel.transform.GetChild(i).GetComponent<Image>().color = Color.black; //lv 이하의 점들은 검정으로
            }
            
            else
            {
                progressLevel.transform.GetChild(i).GetComponent<Image>().color = Color.white;  //lv 이후의 점들은 하얀색으로
            }
        }
    }

    private int getCurrentLevel()
    {
        int level = 0;
        for (int i = 0; i < progressLevel.transform.childCount; i++)
        {
            if (progressLevel.transform.GetChild(i).GetComponent<Image>().color == Color.black)
            {
                level = i;
            }
        }
        return level;
    }

    private string getContentfromRow(GameObject row)
    {
        return row.transform.Find("내용text").GetComponent<TextMeshProUGUI>().text;
    }

    private int getGoalfromRow(GameObject row)
    {
        return parsePoint(row.transform.Find("포인트text").GetComponent<TextMeshProUGUI>());
    }

    private void updateProgress()
    {
        pointRatio.text = parsePoint(currentPoint)*100 / parsePoint(goalPoint)+"%";
        pointCircle.GetComponent<Slider>().value = parsePoint(pointRatio) / 100.0f;


        if (pointCircle.GetComponent<Slider>().value >= 1.0f)
        {

            var rows = GameObject.FindGameObjectsWithTag("row");
            int level = getCurrentLevel();  //level이 1인 경우, rows의 0번째에 해당.
            
            if (level > rows.Length)
            {
                Debug.Log("레벨 상한 도달");
            }
            else
            {
                GameObject totalPoint=GameObject.Find("전체포인트");
                totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + (level + 1) + ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level]);

                currentPoint.text = "현재  0";
                goalPoint.text="완성  "+getGoalfromRow(rows[level]);
            }
            
        }
    }
}
