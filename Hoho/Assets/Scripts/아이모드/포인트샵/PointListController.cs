using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointListController : MonoBehaviour
{
    //목록에 사용할 아이콘들.
    public List<Sprite> spriteList;

    public ProgressController progressController;
    
    //목록1, 목록2, 목록3, 목록4
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
        //시작 시 pointList에 목록들 넣기.
       Transform[] childList = GetComponentsInChildren<RectTransform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i].name.Contains("목록"))
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
    /// 아이콘을 해당 sprite로 설정해줌.
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
    /// content는 칭찬포인트,출석포인트. 뒤에 날짜는 )로 끝나는지 아닌지 확인해서 알아서 붙여줌.
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
    /// 점수를 표시해줌. 현재 점수
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
    /// 점수가 string으로 전달된 경우, 그대로 적어줌.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="content"></param>
    private void setPoint(GameObject target, string content)
    {
        target.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = content;
    }
}
