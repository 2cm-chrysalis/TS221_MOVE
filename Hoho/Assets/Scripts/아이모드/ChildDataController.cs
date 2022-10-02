using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //This template can be customized at C:\Program Files\Unity\Hub\Editor\2021.3.8f1\Editor\Data\Resources\ScriptTemplates\81-C# Script-NewBehaviourScript.cs.txt
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;


/// <summary>
/// 아이모드의 데이터 송수신을 책임짐. 
/// </summary>
public class ChildDataController : MonoBehaviour
{


    static FirebaseFirestore db= FirebaseFirestore.DefaultInstance;

    static bool canSend = false;

    /// <summary>
    /// 전체에서 쓰이는 point
    /// </summary>
    static int point=0;

    /// <summary>
    /// 현재 보상을 얻기 위한 목표 점수.
    /// </summary>
    static int goalPoint=1;

    /// <summary>
    /// 현재 진행 보상 단계. PointShop에서 검은색 점의 개수.
    /// </summary>
    static int level=1;
    /// <summary>
    /// 원 안에 있는 텍스트. 가령, "놀이공원".  
    /// </summary>
    static string rewardTitle="놀이공원";

    /// <summary>
    /// 현재 점수/목표 점수
    /// </summary>
    static float progressRatio=point/(float) goalPoint;


    /// <summary>
    /// 유저 ID.
    /// </summary>
    static string childID = "001";


    /// <summary>
    /// 호흡 결과는 여기에 기록해서 SendGameResult로 보낼 것. timestamp와 0~1 사이의 넣으면 됨. 
    /// </summary>
    public static Dictionary<Timestamp, float> BreatheResult = new Dictionary<Timestamp, float>();

    /// <summary>
    /// point, level, rewardTitle, goalPoint, progressRatio, childID를 담은 Dictionary 반환.
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
    /// true로 설정해야 보낼 수 있음. 
    /// </summary>
    /// <param name="canSend"></param>
    public static void setPrepared(bool canSend)
    {
       ChildDataController.canSend=canSend;
    }

    /// <summary>
    /// 전체에서 쓰이는 point
    /// </summary>
    public static void setPoint(int pt)
    {
        point = pt;
    }


    /// <summary>
    /// 현재 보상을 얻기 위한 목표 점수.
    /// </summary>
    public static void setGoalPoint(int pt)
    {
        goalPoint = pt;
    }


    /// <summary>
    /// 현재 진행 보상 단계. PointShop에서 검은색 점의 개수.
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
    /// 현재 점수/목표 점수
    /// </summary>
    public static void setProgressRatio(float ratio)
    {
        progressRatio = ratio;
    }


    /// <summary>
    /// 유저 ID.
    /// </summary>
    public static void setChildID(string id)
    {
        childID = id;
    }


    /// <summary>
    /// 서버에 포인트, level, rewardTitle, pointString, 진행률, childID를 보냄. 
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
                { "현재 총 포인트", point},
                { "현재 레벨", level },
                { "보상 제목", rewardTitle },
                { "목표 점수", goalPoint },
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
