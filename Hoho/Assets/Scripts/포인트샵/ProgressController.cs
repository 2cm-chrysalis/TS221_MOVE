using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrgressController : MonoBehaviour
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
        return row.transform.Find("����text").GetComponent<TextMeshProUGUI>().text;
    }

    private int getGoalfromRow(GameObject row)
    {
        return parsePoint(row.transform.Find("����Ʈtext").GetComponent<TextMeshProUGUI>());
    }

    private void updateProgress()
    {
        pointRatio.text = parsePoint(currentPoint)*100 / parsePoint(goalPoint)+"%";
        pointCircle.GetComponent<Slider>().value = parsePoint(pointRatio) / 100.0f;


        if (pointCircle.GetComponent<Slider>().value >= 1.0f)
        {

            var rows = GameObject.FindGameObjectsWithTag("row");
            int level = getCurrentLevel();  //level�� 1�� ���, rows�� 0��°�� �ش�.
            
            if (level > rows.Length)
            {
                Debug.Log("���� ���� ����");
            }
            else
            {
                GameObject totalPoint=GameObject.Find("��ü����Ʈ");
                totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + (level + 1) + ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level]);

                currentPoint.text = "����  0";
                goalPoint.text="�ϼ�  "+getGoalfromRow(rows[level]);
            }
            
        }
    }
}
