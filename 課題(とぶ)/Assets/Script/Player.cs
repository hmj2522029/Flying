using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    [SerializeField] float JumpPower = 5.0f;
    [SerializeField] float CheckDistance = 0.2f;    //接地判定用の距離(オブジェクトの右端(下)と左端(下)からの)
    [SerializeField] float WallCheckDistance = 0.05f; //壁判定用の距離(オブジェクトの左右上下の端からの)
    [SerializeField] Transform CheckBottomLeft;     //左端(下)の座標
    [SerializeField] Transform CheckBottomRight;    //右端(下)の座標
    [SerializeField] Transform CheckTopLeft;        //左端(上)の座標
    [SerializeField] Transform CheckTopRight;       //右端(上)の座標

    private Rigidbody2D rd;
    private bool LeftWallHit = false;
    private bool RightWallHit = false;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        //状態の更新
        CheckWall();    //壁が右にあるか左にあるかを更新

        //横移動
        float MoveX = Input.GetAxisRaw("Horizontal");   //左右

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
        }

    }


}
