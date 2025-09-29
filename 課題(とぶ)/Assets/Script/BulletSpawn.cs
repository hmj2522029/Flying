using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    [SerializeField] GameObject BulletPredab; //弾のプレハブ
    [SerializeField] Rigidbody2D rb;          //弾のRigidbody2D

    private void OnEnable() //オブジェクトが有効になったときに呼ばれる
    {
        Player.OnShoot += SpawnBullet; //PlayerクラスのOnShootイベントにSpawnBulletメソッドを登録
    }

    private void OnDisable() //オブジェクトが無効になったときに呼ばれる
    {
        Player.OnShoot -= SpawnBullet; //PlayerクラスのOnShootイベントからSpawnBulletメソッドを解除
    }

    void SpawnBullet(Vector3 position, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //移動方向から角度を計算
        GameObject bullet = Instantiate(BulletPredab, position, Quaternion.Euler(0, 0, angle)); //弾の生成
        bullet.GetComponent<Bullet>().SetMoveDir(direction);                          //弾の方向を設定
    }

}
