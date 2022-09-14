using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressController : MonoBehaviour
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



    public void addPoint(int point)
    {
        point += parsePoint(currentPoint);
        currentPoint.text = "현재  "+point;
        updateProgress();
    }

    public int getCurrentPoint()
    {
        return parsePoint(currentPoint);
    }

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

    /// <summary>
    /// lv가 3이면 점 3개가 검은색으로 됨.
    /// </summary>
    /// <param name="lv"></param>
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
        Debug.Log("progressLevel's child# is "+progressLevel.transform.childCount);
        for (int i = 0; i < progressLevel.transform.childCount; i++)
        {
            if (progressLevel.transform.GetChild(i).GetComponent<Image>().color == Color.black)
            {
                level = i+1;  //0번째인 경우, 레벨1
            }
        }

        Debug.Log("current level is "+level);
        return level;
    }

    private string getContentfromRow(GameObject row)
    {
        return row.transform.Find("내용/내용text").GetComponent<TextMeshProUGUI>().text;
    }

    private int getGoalfromRow(GameObject row)
    {
        return parsePoint(row.transform.Find("포인트/포인트text").GetComponent<TextMeshProUGUI>());
    }

    private void updateTotalPoint()
    {
        GameObject totalPoint = GameObject.Find("전체포인트");
        string currentText = totalPoint.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(currentText.IndexOf("\n") + 1);
        Debug.Log(currentText.LastIndexOf("\n"));
        Debug.Log("Before : " + currentText);
        string changeText = currentText.Substring(currentText.IndexOf("\n") + 1);
        changeText=changeText.Substring(0, changeText.IndexOf("P")+1);

        totalPoint.GetComponent<TextMeshProUGUI>().text = currentText.Replace(changeText, getCurrentPoint()+"P");
    }

    private void updateProgress()
    {
        pointRatio.text = parsePoint(currentPoint)*100 / parsePoint(goalPoint)+"%";
        pointCircle.GetComponent<Slider>().value = parsePoint(pointRatio) / 100.0f;    

        if (pointCircle.GetComponent<Slider>().value >= 1.0f)
        {


            GameObject.Find("Canvas").transform.Find("보상목록").gameObject.SetActive(true);
            var rows = GameObject.FindGameObjectsWithTag("row");
            GameObject.Find("Canvas").transform.Find("보상목록").gameObject.SetActive(false);
            int level = getCurrentLevel();  //level이 1인 경우, rows의 0번째에 해당.

            if (level >= rows.Length)
            {
                Debug.Log("레벨 상한 도달");
            }
            else
            {
                GameObject totalPoint = GameObject.Find("전체포인트");
                currentPoint.text = "현재  0";                                                           
                totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + (level + 1) + ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level]);
                
                goalPoint.text="완성  "+getGoalfromRow(rows[level]);
                pointRatio.text = "0%";
                pointCircle.GetComponent<Slider>().value = 0f;
                updateLevel(level+1);
            }
            
        }
        updateTotalPoint();
    }
}
