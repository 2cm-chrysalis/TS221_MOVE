using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class HookController : MonoBehaviour
{

    public TextMeshProUGUI point;

    /// <summary>
    /// ���˴��� y��ǥ�� ����. y���� �ʹ� �۰ų� ū ��쿡 ����� Mathf.Clamp ���.
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
    /// collision object���� rigidBody, collider ������Ʈ�� �־�� ��. �׸��� OnTriggerEnter2D�� 2�� ȣ���.
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
            point.text = a.ToString() + "P<color=black>��";
            GameObject.Destroy(fish);
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
        //handle point
    }
}
