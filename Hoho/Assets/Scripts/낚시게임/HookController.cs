using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{

    public TextMeshProUGUI point;

    /// <summary>
    /// 낚싯대의 y좌표를 조절. y값이 너무 작거나 큰 경우에 대비해 Mathf.Clamp 사용.
    /// </summary>
    /// <param name="y"></param>
    public void setPosition(int y)
    {
        y = Mathf.Clamp(y, 29, 312);
        this.transform.position = new Vector2(transform.position.x, y);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject fish = collision.gameObject;
        if (fish.tag == "fish")
        {
            int a=Convert.ToInt32(point.text.Split("P")[0])+100;
            point.text = a.ToString() + "P<color=black>↑";
            GameObject.Destroy(fish);
        }
        //handle point
    }


}
