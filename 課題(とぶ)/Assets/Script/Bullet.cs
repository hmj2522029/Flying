using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed = 10f; //弾の速度
    private Rigidbody2D rb;
    private Vector3 MoveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //プレイヤーと衝突しないようにする
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(),  //Playerタグのついたオブジェクトを取得してそのCollider2Dを取得
                                  GetComponent<Collider2D>()); //このオブジェクトのCollider2Dを取得して衝突を無視する
    }
    public void SetMoveDir(Vector3 dir)
    {
        MoveDir = dir; //移動方向を正規化して設定
    }

    void Update()
    {
        rb.velocity = MoveDir * Speed; //移動方向に速度をかけて移動
    }

}
