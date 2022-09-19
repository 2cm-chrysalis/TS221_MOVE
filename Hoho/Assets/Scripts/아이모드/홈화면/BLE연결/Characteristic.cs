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

    private float updateTime = 0.0f;
    private string serviceUuid;
    private string uuid;

    private int value;

    public void setUuid(string DeviceAddress, string ServiceUuid, string Uuid)
    {
        serviceUuid = ServiceUuid;
        uuid = Uuid;
        UUID.text = "UUID : " + uuid;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!AndroidBLEPluginStart.isConnected) { return; }

        updateTime += Time.deltaTime;

        if(serviceUuid==null || uuid == null) { return; }


        if (updateTime > updateTimeLimit)
        {
            List<BleCharacteristicData> characteristicDatas = AndroidBLEPluginStart.characteristicDatas;

            BleCharacteristicData characteristic = characteristicDatas.Find(x => (x.serviceUuid == serviceUuid) && (x.characteristicUuid == uuid));

            //Find가 실패할 수 있음.
            try
            {
                characteristic.serviceUuid+="";
            }catch
            {
                Debug.LogError("UUID matching error");
                characteristic = characteristicDatas.FindLast(x => true);
            }

            if (characteristic.serviceUuid != null)
            {
                if (!characteristic.hasData)
                {
                    Value.text = "Value : No value";
                    AndroidBLEPluginStart._bleControlObj.Call<bool>("setNotification", serviceUuid, uuid, true);
                    return;
                }

                updateValue(uuid, characteristic.intData);
            }
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
