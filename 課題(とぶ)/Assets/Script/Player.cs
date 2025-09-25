using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    private Rigidbody2D rd;
    private Vector2 Direction;

    private float MoveX;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Operation();
    }

    private void FixedUpdate()
    {
        //移動速度ベクトルを現在値から取得
        Direction = rd.velocity;

        //X方向の速度を入力から決定
        Direction.x = MoveX * Speed;

        rd.velocity = Direction;

    }

    private void Operation()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");   //左右
        float MoveY = Input.GetKeyDown(KeyCode.Space) ? 10 : 0; //ジャンプ 

        rd.velocity = new Vector2(rd.velocity.x, MoveY);

        if (MoveX == 1)
        {

        }
        else if (MoveX == -1)
        {
            
        }

    }

}
