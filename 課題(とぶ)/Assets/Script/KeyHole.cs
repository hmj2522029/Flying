using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour
{
    [SerializeField] Transform DoorWithLock;
    private GameObject block;
    public static event System.Action<Transform> OnDoorOpen;

    private void Start()
    {
        block = Resources.Load<GameObject>("blockRed"); // ResourcesフォルダからBlockプレハブをロード
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.HasKey)
            {
                player.HasKey = false; // 鍵を使ったので持っていない状態にする
                Instantiate(block, transform.position, Quaternion.identity); 

                OnDoorOpen?.Invoke(DoorWithLock); // ドアが開くイベントを発火
                Destroy(player.KeyImage);         // 頭上の鍵アイコンを破壊
                Destroy(gameObject);              // 鍵穴オブジェクトを破壊
            }
        }
    }
}

