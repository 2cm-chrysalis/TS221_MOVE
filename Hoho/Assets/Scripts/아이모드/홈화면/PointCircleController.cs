using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointCircleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("전체포인트").GetComponentInChildren<TextMeshProUGUI>().text = ProgressController.totalPointContent;
        GetComponent<Slider>().value = ProgressController.progressRatio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
