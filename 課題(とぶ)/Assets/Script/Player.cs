using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    [SerializeField] float JumpPower = 5.0f;
    [SerializeField] float CheckDistance = 0.2f;    //�ڒn����p�̋���(�I�u�W�F�N�g�̉E�[(��)�ƍ��[(��)�����)
    [SerializeField] float WallCheckDistance = 0.05f; //�ǔ���p�̋���(�I�u�W�F�N�g�̍��E�㉺�̒[�����)
    [SerializeField] Transform CheckBottomLeft;     //���[(��)�̍��W
    [SerializeField] Transform CheckBottomRight;    //�E�[(��)�̍��W
    [SerializeField] Transform CheckTopLeft;        //���[(��)�̍��W
    [SerializeField] Transform CheckTopRight;       //�E�[(��)�̍��W

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
        //��Ԃ̍X�V
        CheckWall();    //�ǂ��E�ɂ��邩���ɂ��邩���X�V

        //���ړ�
        float MoveX = Input.GetAxisRaw("Horizontal");   //���E

        if (!CheckGround())
        {
            if (LeftWallHit && MoveX < 0) MoveX = 0;
            if (RightWallHit && MoveX > 0) MoveX = 0;
        }

        rd.velocity = new Vector2(MoveX * Speed, rd.velocity.y);
    }

    private bool CheckGround()
    {
        //�ڒn����(���C�L���X�g)
        bool LeftHit = Physics2D.Raycast(CheckBottomLeft.position, Vector2.down, CheckDistance, LayerMask.GetMask("Ground"));
        bool RightHit = Physics2D.Raycast(CheckBottomRight.position, Vector2.down, CheckDistance, LayerMask.GetMask("Ground"));
        return LeftHit || RightHit;

    }

    private bool CheckWall()
    {
        //�ǔ���(���C�L���X�g)
        //�I�u�W�F�N�g�̍��[�ƉE�[���炻�ꂼ�ꃌ�C���΂�
        LeftWallHit = Physics2D.Raycast(CheckBottomLeft.position, Vector2.left, WallCheckDistance, LayerMask.GetMask("Ground")) ||
                      Physics2D.Raycast(CheckTopLeft.position, Vector2.left, WallCheckDistance, LayerMask.GetMask("Ground"));

        RightWallHit = Physics2D.Raycast(CheckBottomRight.position, Vector2.right, WallCheckDistance, LayerMask.GetMask("Ground")) ||
                       Physics2D.Raycast(CheckTopRight.position, Vector2.right, WallCheckDistance, LayerMask.GetMask("Ground"));


        return LeftWallHit || RightWallHit;
    }

    private void Jump()
    {
        //�W�����v 
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            rd.velocity = new Vector2(rd.velocity.x, JumpPower);
        }

    }


}
