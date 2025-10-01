using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] float floatAmplitude = 0.25f; // 上下の幅
    [SerializeField] float floatFrequency = 1f;    // 速さ

    private Vector3 startPosition;
    private Vector3 originalScale; // 元の大きさ
    private Player player;


    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        FloatEffect();
    }

    private void FloatEffect()
    {
        if(player.HasKey) return; // プレイヤーが既に鍵を持っている場合は動かさない

        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude; // サイン波を使って上下に動かす
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.HasKey = true; // プレイヤーが鍵を持っていることを示す

                //頭上の鍵アイコンを表示
                Vector3 offset = new Vector3(0, 0.5f, 0); // アイコンの位置調整用オフセット
                player.KeyImage = Instantiate(gameObject, player.transform.position + offset, Quaternion.identity);

                player.KeyImage.AddComponent<FollowPlayer>().Follow(player.transform, offset);  // プレイヤーの頭上に追従させる

                //鍵オブジェクトを非表示
                gameObject.SetActive(false);
            }
        }
    }
}

 
