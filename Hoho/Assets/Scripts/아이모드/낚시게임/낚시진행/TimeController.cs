using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [Tooltip("���� ���� �ð�. ���� ���࿡ ���� �پ��.")]
    public TextMeshProUGUI timeText;

    [Tooltip("���� ���� �� �ð��� ���� ������ �پ��� ���� �ٲ�.")]
    public GameObject timeBar;

    /// <summary>
    /// ���� ��ü �ð�. setTime �Լ��� �ʱ�ȭ.
    /// </summary>
    [Tooltip("������ ��ü �ð�")]
    public float fullTime;
    
    [SerializeField][Tooltip("���� ���� �ð�")]
    private float progressedTime;


    public void setTime(float time)
    {
        fullTime = time;
    }

    public float getProgressedTime()
    {
        return progressedTime;
    }

    public float getRemainingTime()
    {
        return fullTime - progressedTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        progressedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart.isStarted && !Pause.isPaused)
        {
            progressedTime += Time.deltaTime;
            updateTime();
            updateBar();
        }

        if (GameStart.isStarted && getRemainingTime() <= 0f)
        {
            SceneLoader.LoadScene("���̸��Ȩȭ��");
        }
    }

    private void updateTime()
    {
        int min;
        int second;
        if (fullTime > progressedTime) { 
            min = (int) (fullTime-progressedTime)/60;
            second = (int)(fullTime - progressedTime) - min * 60;
        }
        else
        {
            min = second = 0;
        }

        timeText.text = (min.ToString().Length == 2) ? min.ToString() + "  " + second.ToString() : "0"+min.ToString() + "  " + second.ToString();
    }

    private void updateBar()
    {
        Slider timeSlider = timeBar.GetComponent<Slider>();
        Image fill=GameObject.Find("Fill").GetComponent<Image>();

        timeSlider.value = (fullTime - progressedTime) / fullTime;
        if (timeSlider.value<0.3f)
        {
            fill.color = Color.red;
        }

        else if (timeSlider.value < 0.6f)
        {    
            fill.color = Color.yellow;
        }

        else
        {
            fill.color = Color.green;
        }

    }

}
