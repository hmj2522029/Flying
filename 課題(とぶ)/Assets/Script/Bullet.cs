using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform RayPos;
    [SerializeField] float Speed = 10f; //弾の速度
    private Rigidbody2D rb;
    private Vector3 MoveDir;
    private Player m_player;
    RaycastHit2D WallHit;

    public static event Action<Vector2, Vector2> OnBulletHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player").GetComponent<Player>();

        //プレイヤーと衝突しないようにする
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(),  //Playerタグのついたオブジェクトを取得してそのCollider2Dを取得
                                  GetComponent<Collider2D>()); //このオブジェクトのCollider2Dを取得して衝突を無視する
    }
    public void SetMoveDir(Vector3 dir)
    {
        MoveDir = dir; //移動方向を設定
    }

    void Update()
    {
        rb.velocity = MoveDir * Speed; //移動方向に速度をかけて移動


    }

    void OnBecameInvisible()
    {
        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //画面外に出たら消す
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突したときの処理
        ContactPoint2D contact = collision.contacts[0]; //衝突した点の情報を取得

        Vector2 noraml = contact.normal;                //衝突した時の弾の法線ベクトルを取得
        Vector2 hit = contact.point;                    //衝突した点の座標を取得

        OnBulletHit?.Invoke(hit, noraml);               //弾が何かに当たったときのイベントを発火

        Destroy(gameObject); //何かに当たったら消す
    }

}
