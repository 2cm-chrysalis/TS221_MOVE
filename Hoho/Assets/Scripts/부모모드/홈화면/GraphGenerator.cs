using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GraphGenerator : MonoBehaviour
{
    [Tooltip("�׷����� ��")]
    public GameObject dotPrefab;
    [Tooltip("�׷��� �� ������ ��")]
    public UILineRenderer lineRenderer;

    [Tooltip("ȣ��׷����� content")]
    public GameObject content;
   
    public GameObject xAxis;
    public GameObject yAxis;    

    [Tooltip("���̾�̽����� ������ ȣ�� ������")]
    public Dictionary<int, int> data=new Dictionary<int, int>();

    [Tooltip("�� ȭ�鿡 ���� ���� ����")]
    public int dotNum=10;


    /// <summary>
    /// �־��� �����Ϳ� ���� �׷��� �׸���.
    /// </summary>
    public void drawGraph()
    {
        //�׷��� �����.
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
        //�׷��� �׸���.
        foreach (KeyValuePair<int, int> datum in data)
        {
            
            GameObject dot=Instantiate(dotPrefab, content.transform);
            dots.Add(dot);

            float xSpacing = content.transform.parent.parent.GetComponent<RectTransform>().sizeDelta.x/dotNum;
            Debug.Log("xSpacing : " + xSpacing);


            dot.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f+yAxis.GetComponent<RectTransform>().anchoredPosition.x + datum.Key * xSpacing , xAxis.GetComponent<RectTransform>().anchoredPosition.y - 30f);
            dot.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, xAxis.GetComponent<RectTransform>().rect.yMax+yAxis.GetComponent<RectTransform>().rect.height*(0.01f+datum.Value/100f));
           
            Debug.Log("rectPosition of "+datum.Key+" : "+ dot.GetComponent<RectTransform>().anchoredPosition);
            Debug.Log(datum);

            dot.GetComponent<TextMeshProUGUI>().text = datum.Key.ToString();
            //dot.GetComponent<TextMeshProUGUI>().fontSize=
        }

        //draw lines
        List<Vector2> tempPoints =new List<Vector2>();

        for(int i=0; i<dots.Count; i++)
        {
            tempPoints.Add(dots[i].GetComponent<RectTransform>().anchoredPosition+ dots[i].transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition);
        }

        lineRenderer.Points = tempPoints.ToArray();
        
        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dots[dots.Count-1].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<100; i++)
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
