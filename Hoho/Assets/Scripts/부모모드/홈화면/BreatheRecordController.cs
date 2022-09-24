using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreatheRecordController : MonoBehaviour
{
    [Tooltip("호흡 측정용 막대그래프의 prefab")]
    public GameObject barPrefab;
    [Tooltip("일간호흡기록창의 content")]
    public GameObject content;

    [Tooltip("호흡기록패널")]
    public GameObject breathePannel;

    /// <summary>
    /// 막대를 생성함. percent는 69%
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <param name="isAM"></param>
    public void makeBar(int percent, int hour, int minute, bool isAM=false)
    {
        Instantiate(barPrefab, content.transform);
        
        GameObject ratio=barPrefab.transform.Find("달성률").gameObject;        
        GameObject button= barPrefab.transform.Find("button").gameObject;
        GameObject icon = barPrefab.transform.Find("icon").gameObject;
        TextMeshProUGUI timeText = barPrefab.transform.Find("시간").gameObject.GetComponent<TextMeshProUGUI>();

        //icon 변경
        icon.GetComponent<Image>();

        //버튼 조절
        button.GetComponent<Button>().onClick.AddListener(makeGraph);
        button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, percent*3);

        //달성률 변경
        ratio.GetComponent<TextMeshProUGUI>().text = percent + "%";
        ratio.transform.localPosition=new Vector2(ratio.transform.localPosition.x, button.GetComponent<RectTransform>().rect.yMax+0.1f);

        //시간 변경
        timeText.text = hour + ":" + minute + (isAM ? "AM" : "PM"); 
        
        void makeGraph()
        {
            GameObject.Find("호흡기록패널").SetActive(true);
            GameObject.Find("홈화면패널").SetActive(false);
            return;
        }
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
