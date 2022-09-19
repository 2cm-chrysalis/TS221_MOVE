using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointListController : MonoBehaviour
{
    //��Ͽ� ����� �����ܵ�.
    public List<Sprite> spriteList;

    public ProgressController progressController;
    
    //���1, ���2, ���3, ���4
    private List<GameObject> pointList = new List<GameObject>();
    

    public void updateList(Sprite icon, string content, int point)
    {
        for (int i = pointList.Count - 2; i >=0; i--)
        {
            var currentObject = pointList[i];
            var targetObject = pointList[i + 1];
            
            setIcon(targetObject, currentObject.transform.GetChild(0).GetComponent<Image>().sprite);
            setContent(targetObject, currentObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
            setPoint(targetObject, currentObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
        }

        setIcon(pointList[0], icon);
        setContent(pointList[0], content);
        setPoint(pointList[0], point);
    }

    // Start is called before the first frame update
    void Start()
    {
        //���� �� pointList�� ��ϵ� �ֱ�.
       Transform[] childList = GetComponentsInChildren<RectTransform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i].name.Contains("���"))
                {
                    pointList.Add(childList[i].gameObject);
                }
                    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    /// <summary>
    /// �������� �ش� sprite�� ��������.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="icon"></param>
    private void setIcon(GameObject target, Sprite icon)
    {
        if (icon != null)
        {
            target.transform.GetChild(0).GetComponent<Image>().sprite = icon;
        }
    }

    /// <summary>
    /// content�� Ī������Ʈ,�⼮����Ʈ. �ڿ� ��¥�� )�� �������� �ƴ��� Ȯ���ؼ� �˾Ƽ� �ٿ���.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="content"></param>
    private void setContent(GameObject target, string content)
    {
        if (!content.EndsWith(")"))
        {
            content += "(" + DateTime.Now.Month + "/" + DateTime.Now.Day + ")";
        }
        target.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = content;
    }

    /// <summary>
    /// ������ ǥ������. ���� ����
    /// </summary>
    /// <param name="target"></param>
    /// <param name="point"></param>
    private void setPoint(GameObject target, int point)
    {
        int accumPoint=progressController.getCurrentPoint();
        string content = "+"+point+"P\n"+"("+accumPoint+"P)";
        target.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = content;
    }

    /// <summary>
    /// ������ string���� ���޵� ���, �״�� ������.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="content"></param>
    private void setPoint(GameObject target, string content)
    {
        target.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = content;
    }
}
