using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    public GameObject wooper_looper;
    public GameObject bubble;
    public GameObject fish1;

    public float upTime=4.0f;
    public float upWaitTime=1.0f;
    public float downTime=8.0f;
    public float downWaitTime=0.0f;

    public string[] fishList = { "bubble","angel", "arowana fish", "asian arowana fish", "betta fish", "calvary fish", "coelacanth fish",
        "discus", "flower fish", "golden archer fish", "guppy", "lnflatable molly fish", "Monodactylus",
        "piranha fish", "ramirezi", "silver shark fish", "sword tail", "wooper looper", "Yellow Cichlid" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 들숨 시간, 날숨 시간, 숨 참는 시간을 고려한, 시간에 따른 함숫값 반환. 사인파 형태.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private float breathPos(float x)
    {
        float period = upTime + upWaitTime + downTime + downWaitTime;
        float phase = x % period;
        if (phase<=0.0f) 
        {
            Debug.Log("Wrong period.");
            return -1.0f;
        }
        if (phase < upTime)
        {
            return -(312f-29f)/2.0f*Mathf.Cos(Mathf.PI / upTime * phase)+ (312f - 29f) / 2.0f;
        }
        else if (phase < upWaitTime)
        {
            return 312;
        }

        else if (phase < downTime)
        {
            phase = phase - upTime - upWaitTime;
            return (312f - 29f) / 2.0f * Mathf.Cos(Mathf.PI / downTime * phase) + (312f - 29f) / 2.0f;
        }

        else
        {
            return 29;
        }
    }
}
