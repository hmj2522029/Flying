using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動・ジャンプ関連")]
    [SerializeField] float Speed = 1.0f;
    [SerializeField] float JumpPower = 5.0f;

    [Header("判定関連")]
    [SerializeField] float CheckDistance = 0.2f;     //接地判定用の距離(オブジェクトの右端(下)と左端(下)からの)
    [SerializeField] float WallCheckDistance = 0.05f;//壁判定用の距離(オブジェクトの左右上下の端からの)

    [Header("レイキャスト関連")]
    [SerializeField] Transform CheckBottomLeft;      //左端(下)の座標
    [SerializeField] Transform CheckBottomRight;     //右端(下)の座標
    [SerializeField] Transform CheckTopLeft;         //左端(上)の座標
    [SerializeField] Transform CheckTopRight;        //右端(上)の座標

    [Header("アニメーション関連")]
    [SerializeField] float ScaleSpeed = 0.1f;                   //大きさ変化の速さ
    [SerializeField] float BreathingAmplitude = 0.05f;          //呼吸の振幅
    [SerializeField] float BreathingFrequency = 2.0f;           //呼吸の速さ
    private float PrevVelocityY = 0f;                           //前フレームのY方向の速度
    private bool isHighestPoint = false;                        //ジャンプの最高点に到達したかどうか
    private Vector3 NormalScale = new Vector3(0.3f, 0.4f, 1.0f);//通常時の大きさ
    private Vector3 JumpScale = new Vector3(0.15f, 0.6f, 1.0f); //ジャンプ時の大きさ 

    [Header("弾関連")]
    [SerializeField] Transform BulletGenerationPosition; //弾の生成位置
    public bool isBulletCooldownTime = false;    //弾のクールダウン



    private Rigidbody2D rd; 
    private bool isJamping = false;        //ジャンプ中かどうか
    private bool LeftWallHit = false;   //左に壁があるかどうか
    private bool RightWallHit = false;  //右に壁があるかどうか
    private float MoveX = 0f;

    public Vector3 direction;
    public static event Action<Vector3, Vector3> OnShoot;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        PlayerShoot();
        Jump();
        HandleScale();
        PrevVelocityY = rd.velocity.y;
    }

    private void Move()
    {
        //状態の更新
        CheckWall();    //壁が右にあるか左にあるかを更新

        //横移動
        MoveX = Input.GetAxisRaw("Horizontal");   //左右

        if (!CheckGround())
        {
            if (LeftWallHit && MoveX < 0) MoveX = 0;
            if (RightWallHit && MoveX > 0) MoveX = 0;
        }

        rd.velocity = new Vector2(MoveX * Speed, rd.velocity.y);


    }

    private bool CheckGround()
    {
        //接地判定(レイキャスト)
        bool LeftHit = Physics2D.Raycast(CheckBottomLeft.position, Vector2.down, CheckDistance, LayerMask.GetMask("Ground"));
        bool RightHit = Physics2D.Raycast(CheckBottomRight.position, Vector2.down, CheckDistance, LayerMask.GetMask("Ground"));
        return LeftHit || RightHit;

    }

    private bool CheckWall()
    {
        //壁判定(レイキャスト)
        //オブジェクトの左端と右端からそれぞれレイを飛ばす
        LeftWallHit = Physics2D.Raycast(CheckBottomLeft.position, Vector2.left, WallCheckDistance, LayerMask.GetMask("Ground")) ||
                      Physics2D.Raycast(CheckTopLeft.position, Vector2.left, WallCheckDistance, LayerMask.GetMask("Ground"));

        RightWallHit = Physics2D.Raycast(CheckBottomRight.position, Vector2.right, WallCheckDistance, LayerMask.GetMask("Ground")) ||
                       Physics2D.Raycast(CheckTopRight.position, Vector2.right, WallCheckDistance, LayerMask.GetMask("Ground"));


        return LeftWallHit || RightWallHit;
    }

    private void Jump()
    {
        //ジャンプ 
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            rd.velocity = new Vector2(rd.velocity.x, JumpPower);
            isJamping = true;              //ジャンプ中に設定
        }

    }

    private void HandleScale()
    {


        //アニメーション
        if (!CheckGround()) 
        {
            if (!isHighestPoint && rd.velocity.y < 0) isHighestPoint = true; //最高点に到達したかどうかの更新

            //ジャンプ中
            //ジャンプアニメーション
            if (isHighestPoint) //最高点に到達した後の下降中
            {
                transform.localScale = Vector3.Lerp(transform.localScale, NormalScale, 0.3f);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, JumpScale, 0.5f);
            }


        }
        else  //接地中
        {
            isJamping = false;         //ジャンプ中フラグをfalseに
            isHighestPoint = false; //最高点に到達したかどうかのフラグをfalseに

            transform.localScale = Vector3.Lerp(transform.localScale, NormalScale, ScaleSpeed);
        }

    }

    private void PlayerShoot()
    {
        if(Input.GetMouseButtonDown(0) && !isBulletCooldownTime)
        {
            isBulletCooldownTime = true;

            Vector3 mousePos = Input.mousePosition; //マウスのスクリーン座標
            mousePos.z = 10f; //カメラからの距離

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos); //マウスのワールド座標
            direction = (worldMousePos - BulletGenerationPosition.position).normalized; //弾の発射方向を計算して正規化
            OnShoot?.Invoke(BulletGenerationPosition.position, direction);   //弾の生成位置と発射方向を引数にしてイベントを発火
        }


    }
}
