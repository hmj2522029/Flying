using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;// リスポーンポイント
    [SerializeField] Player player;         // プレイヤーオブジェクト
    [SerializeField] Key key;               // 鍵オブジェクト

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = RespawnPoint.position; // プレイヤーをリスポーンポイントに移動

            // 鍵をリセット
            player.HasKey = false;
            if (player.KeyImage != null)
            {
                Destroy(player.KeyImage); // 頭上の鍵アイコンを破壊
            }
            if (key != null)
            {
                key.gameObject.SetActive(true); // 鍵オブジェクトを再表示
            }
        }

    }
}

