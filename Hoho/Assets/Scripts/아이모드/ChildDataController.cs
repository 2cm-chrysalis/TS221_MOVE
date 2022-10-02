using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //This template can be customized at C:\Program Files\Unity\Hub\Editor\2021.3.8f1\Editor\Data\Resources\ScriptTemplates\81-C# Script-NewBehaviourScript.cs.txt
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;


/// <summary>
/// ���̸���� ������ �ۼ����� å����. 
/// </summary>
public class ChildDataController : MonoBehaviour
{


    static FirebaseFirestore db= FirebaseFirestore.DefaultInstance;

    static bool canSend = false;

    /// <summary>
    /// ��ü���� ���̴� point
    /// </summary>
    static int point=0;

    /// <summary>
    /// ���� ������ ��� ���� ��ǥ ����.
    /// </summary>
    static int goalPoint=1;

    /// <summary>
    /// ���� ���� ���� �ܰ�. PointShop���� ������ ���� ����.
    /// </summary>
    static int level=1;
    /// <summary>
    /// �� �ȿ� �ִ� �ؽ�Ʈ. ����, "���̰���".  
    /// </summary>
    static string rewardTitle="���̰���";

    /// <summary>
    /// ���� ����/��ǥ ����
    /// </summary>
    static float progressRatio=point/(float) goalPoint;


    /// <summary>
    /// ���� ID.
    /// </summary>
    static string childID = "001";


    /// <summary>
    /// ȣ�� ����� ���⿡ ����ؼ� SendGameResult�� ���� ��. timestamp�� 0~1 ������ ������ ��. 
    /// </summary>
    public static Dictionary<Timestamp, float> BreatheResult = new Dictionary<Timestamp, float>();

    /// <summary>
    /// point, level, rewardTitle, goalPoint, progressRatio, childID�� ���� Dictionary ��ȯ.
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, object> getValues()
    {

        Dictionary<string, object> A = new Dictionary<string, object>();
        A.Add("canSend", canSend);
        A.Add("point", point);
        A.Add("level", level);
        A.Add("goalPoint", goalPoint);
        A.Add("rewardTitle", rewardTitle);        
        A.Add("progressRatio", progressRatio);
        A.Add("childID", childID);        

        return A;
    }

    /// <summary>
    /// true�� �����ؾ� ���� �� ����. 
    /// </summary>
    /// <param name="canSend"></param>
    public static void setPrepared(bool canSend)
    {
       ChildDataController.canSend=canSend;
    }

    /// <summary>
    /// ��ü���� ���̴� point
    /// </summary>
    public static void setPoint(int pt)
    {
        point = pt;
    }


    /// <summary>
    /// ���� ������ ��� ���� ��ǥ ����.
    /// </summary>
    public static void setGoalPoint(int pt)
    {
        goalPoint = pt;
    }


    /// <summary>
    /// ���� ���� ���� �ܰ�. PointShop���� ������ ���� ����.
    /// </summary>
    public static void setLevel(int lv)
    {
        level = lv;
    }

    public static void setRewardTitle(int lv)
    {
        level = lv;
    }

    /// <summary>
    /// ���� ����/��ǥ ����
    /// </summary>
    public static void setProgressRatio(float ratio)
    {
        progressRatio = ratio;
    }


    /// <summary>
    /// ���� ID.
    /// </summary>
    public static void setChildID(string id)
    {
        childID = id;
    }


    /// <summary>
    /// ������ ����Ʈ, level, rewardTitle, pointString, �����, childID�� ����. 
    /// </summary>
    public static void SendPoint()
    {
        if (!canSend)
        {
            Debug.Log("Not yet prepared.");
        }

        Debug.Log("Send point.");
        DocumentReference docRef = db.Collection("ChildrenUsers").Document(childID).Collection("Point").Document("CurrentPoint");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "���� �� ����Ʈ", point},
                { "���� ����", level },
                { "���� ����", rewardTitle },
                { "��ǥ ����", goalPoint },
        };

        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            
            if (task.IsCompleted)
            {
                Debug.Log("Added data to the document in the users collection.");
            }
            else { Debug.Log("Failed"); }
        });
    }


    public static void SendFishResult()
    {

    }

    public void UpdateData()
    {
        DocumentReference docRef = db.Collection("users").Document("aturing");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the aturing document in the users collection.");
        });
    }

    public void ReadData()
    {
        Debug.Log("SSSS Firebase Read Data.");
        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(String.Format("First: {0}", documentDictionary["First"]));
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(String.Format("Middle: {0}", documentDictionary["Middle"]));
                }

                Debug.Log(String.Format("Last: {0}", documentDictionary["Last"]));
                Debug.Log(String.Format("Born: {0}", documentDictionary["Born"]));
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    // Start is called before the first frame update
    void Start()
    {        
        if (db == null)
        {
            db = FirebaseFirestore.DefaultInstance;
        }
        DontDestroyOnLoad(gameObject);        
    }
}
