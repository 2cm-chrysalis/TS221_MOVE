using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Android.Data;

public class Characteristic : MonoBehaviour
{

    public static int beltValue=0;

    public TextMeshProUGUI UUID;
    public TextMeshProUGUI Value;

    public float updateTimeLimit = 0.1f;
    public static int value=0;

    private float updateTime = 0.0f;
    private string serviceUuid= "180c";
    private string uuid = "6eea5885-ed94-4731-b81a-00eb60d93b49";

    private List<string> notifiedList = new List<string>();

    public void setUuid(string DeviceAddress, string ServiceUuid, string Uuid)
    {
        serviceUuid = ServiceUuid;
        uuid = Uuid;
        UUID.text = "UUID : " + uuid;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!AndroidBLEPluginStart.isConnected) { return; }

        updateTime += Time.deltaTime;

        if(serviceUuid==null || uuid == null) { return; }

        if (updateTime > updateTimeLimit)
        {
            Debug.LogError("characteristic update start");
            List<BleCharacteristicData> characteristicDatas = AndroidBLEPluginStart.characteristicDatas;

            BleCharacteristicData characteristic = characteristicDatas.Find(x => (x.serviceUuid.Contains(serviceUuid) && (x.characteristicUuid == uuid)));
            foreach(BleCharacteristicData ch in characteristicDatas)
            {                
                if (!notifiedList.Contains(ch.serviceUuid+ch.characteristicUuid))
                {
                    Debug.Log("serviceUuid : " + ch.serviceUuid + ", " + "characteristicUuid : " + ch.characteristicUuid+"\n");
                    AndroidBLEPluginStart._bleControlObj.Call<bool>("setNotification", ch.serviceUuid, ch.characteristicUuid, true);
                    notifiedList.Add(ch.serviceUuid+ch.characteristicUuid);
                    Debug.Log("notification done.");                  
                }
            }

            //Find가 실패할 수 있음.           
            try 
            {                
                characteristic.serviceUuid.Contains("");
            }
            catch
            {
                Debug.LogError("characteristic was null");
                characteristic = characteristicDatas.Find(x => x.hasData);
            }

            if (characteristic.serviceUuid != null)
            {
                if (!characteristic.hasData)
                {
                    Debug.Log("service : "+characteristic.serviceUuid+", character : "+characteristic.characteristicUuid);
                    Value.text = "Value : No value";
                    AndroidBLEPluginStart._bleControlObj.Call<bool>("setNotification", characteristic.serviceUuid, characteristic.characteristicUuid, true);
                    return;
                }

                updateValue(uuid, characteristic.intData);
                UUID.text = "UUID : " + characteristic.characteristicUuid;
            }
            Debug.LogError("characteristic update Done.");
            updateTime = 0f;
        }
    }


    private void updateValue(string charId, int intData)
    {
        Debug.Log(charId+":\nthe value : " + value.ToString());
        Value.text = "int Value : " + intData.ToString();
        beltValue = intData;
    }

}
