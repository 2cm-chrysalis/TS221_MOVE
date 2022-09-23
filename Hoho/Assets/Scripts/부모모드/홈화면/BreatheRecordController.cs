using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreatheRecordController : MonoBehaviour
{
    [Tooltip("ȣ�� ������ ����׷����� prefab")]
    public GameObject barPrefab;
    [Tooltip("�ϰ�ȣ����â�� content")]
    public GameObject content;

    [Tooltip("ȣ�����г�")]
    public GameObject breathePannel;

    /// <summary>
    /// ���븦 ������. percent�� 69%
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <param name="isAM"></param>
    public void makeBar(int percent, int hour, int minute, bool isAM=false)
    {
        Instantiate(barPrefab, content.transform);
        
        GameObject ratio=barPrefab.transform.Find("�޼���").gameObject;        
        GameObject button= barPrefab.transform.Find("button").gameObject;
        GameObject icon = barPrefab.transform.Find("icon").gameObject;
        TextMeshProUGUI timeText = barPrefab.transform.Find("�ð�").gameObject.GetComponent<TextMeshProUGUI>();

        //icon ����
        icon.GetComponent<Image>();

        //����� ����
        ratio.GetComponent<TextMeshProUGUI>().text = percent + "%";
        ratio.transform.localPosition=new Vector2(ratio.transform.localPosition.x, button.GetComponent<RectTransform>().rect.yMax+10f);

        button.GetComponent<Button>().onClick.AddListener(makeGraph);
        
        void makeGraph()
        {
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
