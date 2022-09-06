using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingOnGaming : MonoBehaviour
{

    public static float xScreenHalfSizeBase=8.890215f;
    public static float yScreenHalfSizeBase = 5f;
    public static float xScaler;
    public static float yScaler;

    public RectTransform clockRect;

    public GameObject background;
    public GameObject boat;


    // Start is called before the first frame update
    void Start()
    {

        float yScreenHalfSize = Camera.main.orthographicSize;
        float xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
        xScaler = xScreenHalfSize / xScreenHalfSizeBase;
        yScaler = yScreenHalfSize / yScreenHalfSizeBase;

        Vector2 clockPos = new Vector2((clockRect.position.x / (float)Screen.width - 0.5f) * xScreenHalfSize, (clockRect.position.y / Screen.height - 0.5f) * yScreenHalfSize);

        Debug.Log("y is "+yScreenHalfSize);
        Debug.Log("x is "+xScreenHalfSize);

        boat.transform.localScale = new Vector3(xScreenHalfSize/ 8.890215f, yScreenHalfSize/5.0f, 1);
        boat.transform.position = new Vector3(xScreenHalfSize / 8.890215f * -6.2f, yScreenHalfSize / 5.0f * 3.4f, 1);

        
        background.transform.localScale= new Vector3(xScreenHalfSize / xScreenHalfSizeBase * 2.897845f, yScreenHalfSize / yScreenHalfSizeBase * 2.959679f, 1);

    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
