using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressController : MonoBehaviour
{
    /// <summary>
    /// ��ü���� ���̴� point
    /// </summary>
    public static int point;
    /// <summary>
    /// ���� ���� ���� �ܰ�. PointShop���� ������ ���� ����.
    /// </summary>
    public static int level;
    /// <summary>
    /// �� �ȿ� �ִ� �ؽ�Ʈ. ����, <1>2370P ���̰���.  
    /// </summary>
    public static string totalPointContent;
    /// <summary>
    /// ���� ����/��ǥ ����
    /// </summary>
    public static float progressRatio;





    public int pointTest;
    public int levelTest;
    public string totalPointContentTest;








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


    /// <summary>
    /// pointShop�� point�� ����. static ������ ����. ���߿� ���̾�̽��� �����⵵ �ؾ���. 
    /// </summary>
    /// <param name="point"></param>
    public void addPoint(int point)
    {
        point += parsePoint(currentPoint);
        ProgressController.point = point;
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
        initProgress();
    }

    // Update is called once per frame
    void Update()
    {
        pointTest=point; 
        levelTest=level;
        totalPointContentTest=totalPointContent;
    }

    /// <summary>
    /// ���� ����, ���� ���� ��Ȳ, �����, 
    /// </summary>
    private void initProgress()
    {
        currentPoint.text = "����  " + ProgressController.point;
        ProgressController.level = Math.Max(1, level);
        updateLevel(level);
        GameObject totalPoint = GameObject.Find("��ü����Ʈ");        

        GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(true);
        var rows = GameObject.FindGameObjectsWithTag("row");
        GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(false);

        if (level >= rows.Length)
        {
            level = rows.Length-1;
            Debug.Log("���� ���� ����");
        }
        
        totalPointContent = totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + level + ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level-1]);
        goalPoint.text = "�ϼ�  " + getGoalfromRow(rows[level-1]);

        Debug.Log(goalPoint.text);

        updateProgress();
        
    }


    /// <summary>
    /// �� ��ũ��Ʈ�� ���ǵ� Text ������Ʈ�� �־�����, �� text���� int�� �� P���� ��ȯ. (����, "1143P"�� 1143 ��ȯ.)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
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
    /// progressLevel�� ���� �� lv ������ ������ ��������, �������� �Ͼ������ �ٲ�.
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

    /// <summary>
    /// progressLevel�� ǥ�õ� ����(���� �� ����)�� ��ȯ��.
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// ���� ����� �࿡�� ������ ��ȯ.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private string getContentfromRow(GameObject row)
    {
        return row.transform.Find("����/����text").GetComponent<TextMeshProUGUI>().text;
    }

    /// <summary>
    /// ���� ����� �࿡�� point ��ǥ�� int�� ��ȯ.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private int getGoalfromRow(GameObject row)
    {
        return parsePoint(row.transform.Find("����Ʈ/����Ʈtext").GetComponent<TextMeshProUGUI>());
    }

    /// <summary>
    /// ���׶�� �� text�� point �κ��� ���� ������ ���� ������Ʈ��.
    /// </summary>
    private void updateTotalPoint()
    {
        GameObject totalPoint = GameObject.Find("��ü����Ʈ");  //��ü����Ʈ�� �����Ȳ����/����Ʈ���׶��/innerBoder/ContentArea�� ���� ���.
        string currentText = totalPoint.GetComponent<TextMeshProUGUI>().text;
        string changeText = currentText.Substring(currentText.IndexOf("\n") + 1);
        changeText=changeText.Substring(0, changeText.IndexOf("P")+1);

        totalPointContent=totalPoint.GetComponent<TextMeshProUGUI>().text = currentText.Replace(changeText, getCurrentPoint()+"P");
    }

    /// <summary>
    /// ���� ������ �������� lv(�� ����), ���� ��ĥ�� ����, ������� ����. ������� 100% �̻��̸� (������ ���) �״��� ���� �ܰ�� �Ѿ.
    /// </summary>
    private void updateProgress()
    {
        pointRatio.text = parsePoint(currentPoint)*100 / parsePoint(goalPoint)+"%";
        progressRatio = pointCircle.GetComponent<Slider>().value = parsePoint(pointRatio) / 100.0f;

        if (pointCircle.GetComponent<Slider>().value >= 1.0f)
        {
            levelUp();
        }

        updateTotalPoint();

    }

    /// <summary>
    /// //�� ���� level�� �ش��ϴ� ���� ����� ������ �����ͼ� �����, ��������, ��ǥ����, TotalPoint�� text�� ������.
    /// </summary>
    private void levelUp()
    {
        GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(true);
        var rows = GameObject.FindGameObjectsWithTag("row");
        GameObject.Find("Canvas").transform.Find("������").gameObject.SetActive(false);  

        if (level >= rows.Length)   
        {
            Debug.Log("���� ���� ����");
        }
        else    //level�� 1�� ���, rows�� 0��°�� �ش�.
        {
            GameObject totalPoint = GameObject.Find("��ü����Ʈ");
            currentPoint.text = "����  0";

            level += 1;
            totalPointContent = totalPoint.GetComponent<TextMeshProUGUI>().text = "<" + level+ ">\n" + parsePoint(currentPoint) + "P\n" + getContentfromRow(rows[level-1]);

            goalPoint.text = "�ϼ�  " + getGoalfromRow(rows[level-1]);
            pointRatio.text = "0%";
            pointCircle.GetComponent<Slider>().value = 0f;
            ProgressController.point = 0;
            updateLevel(level);
        }
    }
}
