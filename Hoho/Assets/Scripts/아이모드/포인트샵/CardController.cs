using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardController : MonoBehaviour
{

    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public TextMeshProUGUI point;
    public Button receive;

    public ProgressController progressController;
    public PointListController pointListController;


    public void setCardInfo(string _title, string _content, int _point)
    {
        title.text = _title;
        content.text = _content;
        point.text = _point + "P";
    }

    // Start is called before the first frame update
    void Start()
    {
        receive.onClick.AddListener(getPoint);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void getPoint()
    {
        int p = System.Int32.Parse(point.text.Substring(0, point.text.Length - 1));
        progressController.addPoint(p);
        pointListController.updateList(pointListController.spriteList[0], "ÄªÂùÆ÷ÀÎÆ®", p);
    }

}
