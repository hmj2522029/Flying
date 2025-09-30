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


        Ray2D ray = new Ray2D(RayPos.position, m_player.direction);
        WallHit = Physics2D.Raycast(ray.origin, ray.direction, 0.2f);

        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.green, 0.015f);

        if(WallHit.collider)
        {
            if (WallHit.collider.gameObject.layer == 3 || //下壁
                WallHit.collider.gameObject.layer == 6 || //上壁
                WallHit.collider.gameObject.layer == 7 || //左壁
                WallHit.collider.gameObject.layer == 8 )  //右壁
            {
                Debug.Log("壁のどれかに当たった");
            }

            //Debug.Log(WallHit.collider.gameObject.layer);
            //Debug.Log("当たったオブジェクトのレイヤー名: " + LayerMask.LayerToName(collision.gameObject.layer));
            Debug.DrawRay(WallHit.point, WallHit.normal * 0.2f, Color.green, 0.015f);
        }
    }

    void OnBecameInvisible()
    {
        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //画面外に出たら消す
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(WallHit.collider.gameObject.layer);
        Debug.Log("当たったオブジェクトのレイヤー名: " + LayerMask.LayerToName(collision.gameObject.layer)); // レイヤー名

        Debug.Log(collision.gameObject);

        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //何かに当たったら消す
    }

}
