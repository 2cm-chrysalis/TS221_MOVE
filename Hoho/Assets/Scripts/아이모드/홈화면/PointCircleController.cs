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
        TextMeshProUGUI rewardTitle = GameObject.Find("보상제목").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currentLevel = GameObject.Find("단계Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI pointText = GameObject.Find("현재포인트Text").GetComponent<TextMeshProUGUI>();

        rewardTitle.text = ProgressController.rewardTitle;
        currentLevel.text = ProgressController.level.ToString();
        pointText.text = ProgressController.pointString;

        GetComponent<Slider>().value = ProgressController.progressRatio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
