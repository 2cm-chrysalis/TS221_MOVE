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

    /// <summary>
    /// collision object에는 rigidBody, collider 컴포넌트가 있어야 함. 그리고 OnTriggerEnter2D는 2번 호출됨.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject fish = collision.gameObject;
        Debug.Log(fish);
        if (fish.tag == "Fish" || fish.tag=="fish")
        {
            if (fish.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static) return;
            fish.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            int a=Convert.ToInt32(point.text.Split("P")[0])+100;
            point.text = a.ToString() + "P<color=black>↑";
            GameObject.Destroy(fish);
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
        //handle point
    }
}
