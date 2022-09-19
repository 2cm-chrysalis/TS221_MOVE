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
    public void setPosition(float y)
    {
        y = Mathf.Clamp(y, -2.3f* ScalingOnGaming.yScaler, 3.75f*ScalingOnGaming.yScaler);
        this.transform.parent.transform.localPosition = new Vector2(this.transform.parent.localPosition.x, y);
        Debug.Log("����� ���� pos :" + transform.parent.localPosition);
        Debug.Log("�罽�� y : "+y);
    }

    // Start is called before the first frame update
    void Start()
    {
        setPosition(-2.3f * ScalingOnGaming.yScaler);
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
