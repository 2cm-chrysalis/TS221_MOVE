using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{

    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;

    // ������ ������ ��ü
    Firebase.Auth.FirebaseAuth auth;

    private bool isLogined=false;

    void Awake()
    {
        // ��ü �ʱ�ȭ
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void login()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� �α��� ���� ��
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
            task => {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Debug.Log(emailField.text + " �� �α��� �ϼ̽��ϴ�.");
                    isLogined = true;
                }
                else
                {
                    Debug.Log("�α��ο� �����ϼ̽��ϴ�.");
                }
            }
        );
    }
    public void register()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� ȸ������ ���� ��
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
            task => {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(emailField.text + "�� ȸ������\n");
                }
                else
                    Debug.Log("ȸ������ ����\n");
            }
            );
    }

    void Update()
    {
        Debug.Log(isLogined);
        if (isLogined)
        {            
            SceneManager.LoadScene("����ȭ��");
        }
    }
}