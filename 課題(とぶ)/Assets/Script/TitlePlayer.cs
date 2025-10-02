using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public TitlePortal LastPortalT;              //タイトルシーンのときだけ使う
    public float PortalCooldownTime = 0.5f; //ポータルのクールダウン時間

    private void Update()
    {
        PreventRepeatedTeleportation();
    }
    private void PreventRepeatedTeleportation() //ポータルの連続テレポートを防ぐ
    {
        if (PortalCooldownTime > 0)
        {
            PortalCooldownTime -= Time.deltaTime;

            if (PortalCooldownTime <= 0)
            {
                LastPortalT = null;         //最後に出たポータルをリセット
                PortalCooldownTime = 0.5f; //クールダウン時間をリセット

            }
        }
    }
}


