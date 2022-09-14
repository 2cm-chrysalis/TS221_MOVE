using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressController : MonoBehaviour
{
    [Tooltip("����Ʈ���׶��")]
    public GameObject pointCircle;
    [Tooltip("�����")]
    public TextMeshProUGUI pointRatio;
    [Tooltip("��������")]
    public TextMeshProUGUI currentPoint;
    [Tooltip("��ǥ����")]
    public TextMeshProUGUI goalPoint;
    [Tooltip("1~6�ܰ�, ����")]
    public GameObject progressLevel;
    [Tooltip("������ǥ")]
    public GameObject rewardTable;



    public void addPoint(int point)
    {
        point += parsePoint(currentPoint);
        currentPoint.text = "����  "+point;
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
    /// lv�� 3�̸� �� 3���� ���������� ��.
    /// </summary>
    /// <param name="lv"></param>
    private void updateLevel(int lv)
    {
        for (int i=0; i<progressLevel.transform.childCount; i++)
        {
            if (i < lv)
            {
                progressLevel.transform.GetChild(i).GetComponent<Image>().color = Color.black; //lv ������ ������ ��������
            }            
            else
            {
                progressLevel.transform.GetChild(i).GetComponent<Image>().color = Color.white;  //lv ������ ������ �Ͼ������
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
                level = i+1;  //0��°�� ���, ����1
            }
        }

        Debug.Log("current level is "+level);
        return level;
    }

    private string getContentfromRow(GameObject row)
    {
        return row.transform.Find("����/����text").GetComponent<TextMeshProUGUI>().text;
    }

    private int getGoalfromRow(GameObject row)
    {
        return parsePoint(row.transform.Find("����Ʈ/����Ʈtext").GetComponent<TextMeshProUGUI>());
    }

    private void updateTotalPoint()
    {
        GameObject totalPoint = GameObject.Find("��ü����Ʈ");
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


            GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(true);
            var rows = GameObject.FindGameObjectsWithTag("row");
            GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(false);
            int level = getCurrentLevel();  //level�� 1�� ���, rows�� 0��°�� �ش�.

            if (level >= rows.Length)
            {
                Debug.Log("���� ���� ����");
            }
            else
            {
                GameObject totalPoint = GameObject.Find("��ü����Ʈ");
                currentPoint.text = "����  0";                                                           
                totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + (level + 1) + ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level]);
                
                goalPoint.text="�ϼ�  "+getGoalfromRow(rows[level]);
                pointRatio.text = "0%";
                pointCircle.GetComponent<Slider>().value = 0f;
                updateLevel(level+1);
            }
            
        }
        updateTotalPoint();
    }
}
