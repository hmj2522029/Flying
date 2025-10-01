using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLock : MonoBehaviour
{
    private GameObject OpenDoor;


    private void Start()
    {
        OpenDoor = Resources.Load<GameObject>("doorOpen"); //Resourcesフォルダから開いたドアのPrefabをロード
    }

    private void OnEnable()
    {
        KeyHole.OnDoorOpen += Switching; //KeyHandlerスクリプトのOnUseKeyイベントにSwitchingメソッドを登録
    }

    private void OnDisable()
    {
        KeyHole.OnDoorOpen -= Switching; //KeyHandlerスクリプトのOnUseKeyイベントからSwitchingメソッドを解除
    }

    private void Switching(Transform doorT)
    {
        Instantiate(OpenDoor, doorT.position, Quaternion.identity); //開いたドアを生成
        Destroy(doorT.gameObject); //鍵穴付きドアを破壊
    }

}
