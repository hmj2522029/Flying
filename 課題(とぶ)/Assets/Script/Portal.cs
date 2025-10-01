using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal OppositePortal; //反対側のポータル
    private Animator PortalAnimation;
    private void Awake()
    {
        PortalAnimation = GetComponent<Animator>();
    }
    public void SetDisappear(bool disappear)
    {
        PortalAnimation.SetBool("isExtinctionTransition", disappear); //消滅アニメーション再生

        Destroy(gameObject, 0.5f); //0.5秒後にオブジェクトを破棄
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //プレイヤーがポータルに入った場合
        {
            Player player = collision.GetComponent<Player>();

            //クールダウン中はテレポートしない
            if (player != null && player.LastPortal == this) return;

            if (OppositePortal != null) //反対側のポータルが存在する場合
            {
                Vector3 exitPosition = OppositePortal.transform.position; //反対側のポータルの位置を取得

                Vector3 offset = Vector3.zero; //オフセットを初期化(ポータルの回転に応じて設定)
                float distance = 0.2f; //テレポート後のプレイヤーとポータルのテレポート繰り返し防止距離

                //ポータルの回転に応じてオフセットを設定
                float angle = Mathf.Round(OppositePortal.transform.eulerAngles.z); //ポータルの回転角度を取得して四捨五入(Mathf.Round)
                if (angle == 0) //左の壁に設置されたポータル
                {
                    offset = Vector3.left * distance;
                }
                else if (angle == 180) //右の壁に設置されたポータル
                {
                    offset = Vector3.right * distance;
                }
                else if (angle == 90) //床に設置されたポータル
                {
                    offset = Vector3.up * distance;
                }
                else if (angle == 270) //天井に設置されたポータル
                {
                    offset = Vector3.down * distance;
                }

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                Vector2 velocityBefore = rb.velocity;                 //テレポート前の速度を保存
                collision.transform.position = exitPosition + offset; //プレイヤーの位置を反対側のポータルの位置にオフセットを加えた位置に設定
                if (rb != null)
                {
                    float maxSpeed = 10f; //最大速度

                    float speed = rb.velocity.magnitude; //速度の大きさを取得

                    //速度が最大速度を超えている場合は最大速度に制限
                    if (speed > maxSpeed)
                    {
                        rb.velocity = rb.velocity.normalized * maxSpeed; //速度の大きさを最大速度に制限
                    }
                    else
                    {
                        rb.velocity = rb.velocity; //速度を維持
                    }
                }

                player.LastPortal = OppositePortal; //最後に入ったポータルを更新
                player.PortalCooldownTime = 0.2f; //ポータルのクールダウン時間をリセット
            }
        }
    }
}
