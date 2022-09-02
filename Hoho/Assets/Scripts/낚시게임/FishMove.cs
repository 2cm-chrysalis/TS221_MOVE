using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public string fishName;
    public float speed=1.0f;

    public string[] fishList = { "bubble","angel", "arowana fish", "asian arowana fish", "betta fish", "calvary fish", "coelacanth fish", 
        "discus", "flower fish", "golden archer fish", "guppy", "lnflatable molly fish", "Monodactylus", 
        "piranha fish", "ramirezi", "silver shark fish", "sword tail", "wooper looper", "Yellow Cichlid" };    

    /// <summary>
    /// move��ŭ x������ �̵���Ŵ. �����ʿ��� ���� �̵��� ����.
    /// </summary>
    /// <param name="move"></param>
    public void setMove(float move)
    {
        transform.Translate(new Vector2(speed, 0f));
    }

    /// <summary>
    /// �ʼ� speed ����.
    /// </summary>
    /// <param name="speed"></param>
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    /// <summary>
    /// ����� ���� �����ϱ�.
    /// </summary>
    /// <param name="name"></param>
    public void setKind(string name)
    {
        this.name = name;
        //this.GetComponent<Animator>().runtimeAnimatorController=animationMap[name];
    }

    /// <summary>
    /// ������Ʈ�� ī�޶� �ȿ� �ִ��� Ȯ��. checkRight�� false��, ���������� ��� �ִ� �� �Ű� �� ��.
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="checkRight"></param>
    /// <param name="checkLeft"></param>
    /// <returns></returns>
    public bool CheckObjectIsInCamera(GameObject _target, bool checkRight=false, bool checkLeft=true)
    {
        Camera selectedCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Vector3 screenPoint = selectedCamera.WorldToViewportPoint(_target.transform.position);
        bool onScreen = screenPoint.z > 0 && (checkLeft? screenPoint.x > 0 : true) && (checkRight? screenPoint.x < 1 : true) && screenPoint.y > 0 && screenPoint.y < 1;

        return onScreen;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setMove(-speed * Time.deltaTime);

        if (!CheckObjectIsInCamera(gameObject))
        {
            Destroy(gameObject);
        }
    }
}
