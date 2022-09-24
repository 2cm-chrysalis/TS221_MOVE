using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphGenerator : MonoBehaviour
{
    [Tooltip("그래프의 점")]
    public GameObject dotPrefab;
    [Tooltip("그래프 점 사이의 선")]
    public GameObject linePrefab;

    [Tooltip("호흡그래프의 content")]
    public GameObject content;
   
    public GameObject xAxis;
    public GameObject yAxis;    

    [Tooltip("파이어베이스에서 가져온 호흡 데이터")]
    public Dictionary<int, int> data=new Dictionary<int, int>();

    [Tooltip("한 화면에 보일 점의 개수")]
    public int dotNum;


    /// <summary>
    /// 주어진 데이터에 따라 그래프 그리기.
    /// </summary>
    public void drawGraph()
    {
        //그래프 지우기.
        Transform[] transformList = content.transform.GetComponentsInChildren<Transform>();
        foreach (Transform dot in transformList)
        {
            if ((dot.name).Contains("time"))
            {
                Destroy(dot.gameObject);
            }
        }

        var rectTransform = content.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        
        List<GameObject> dots = new List<GameObject>();
        //그래프 그리기.
        foreach (KeyValuePair<int, int> datum in data)
        {
            GameObject dot=Instantiate(dotPrefab, content.transform);

            dot.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f+yAxis.GetComponent<RectTransform>().anchoredPosition.x + datum.Key * 30, xAxis.GetComponent<RectTransform>().anchoredPosition.y - 30f);
            dot.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yAxis.GetComponent<RectTransform>().rect.height*(datum.Value/100f));

           
            Debug.Log("rectPosition of "+datum.Key+" : "+ dot.GetComponent<RectTransform>().anchoredPosition);
            Debug.Log(datum);

            dot.GetComponent<TextMeshProUGUI>().text = datum.Key.ToString();
            dots.Add(dot);
        }

        for(int i=0; i<dots.Count-1; i++)
        {
            drawLine(dots[i].GetComponent<RectTransform>().anchoredPosition+ dots[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition, dots[i+1].GetComponent<RectTransform>().anchoredPosition + dots[i + 1].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition);
        }
    }

    public void drawLine(Vector2 start, Vector2 end)
    {
        Vector3 differenceVector = end - start;
        GameObject line = Instantiate(linePrefab, content.transform);
        RectTransform imageRectTransform = line.GetComponent<RectTransform>();

        imageRectTransform.anchorMin = imageRectTransform.anchorMax = new Vector2(0, 0);  //앵커 설정

        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, 10.0f);
        imageRectTransform.pivot = new Vector2(0, 0f);
        imageRectTransform.position = start;
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<10; i++)
        {
            data.Add(i, Mathf.RoundToInt(Mathf.Sin(i)*50+50 ));
        }
        drawGraph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
