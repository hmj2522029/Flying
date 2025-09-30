using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�ړ��E�W�����v�֘A")]
    [SerializeField] float Speed = 1.0f;
    [SerializeField] float JumpPower = 5.0f;

    [Header("����֘A")]
    [SerializeField] float CheckDistance = 0.2f;     //�ڒn����p�̋���(�I�u�W�F�N�g�̉E�[(��)�ƍ��[(��)�����)
    [SerializeField] float WallCheckDistance = 0.05f;//�ǔ���p�̋���(�I�u�W�F�N�g�̍��E�㉺�̒[�����)

    [Header("���C�L���X�g�֘A")]
    [SerializeField] Transform CheckBottomLeft;      //���[(��)�̍��W
    [SerializeField] Transform CheckBottomRight;     //�E�[(��)�̍��W
    [SerializeField] Transform CheckTopLeft;         //���[(��)�̍��W
    [SerializeField] Transform CheckTopRight;        //�E�[(��)�̍��W

    [Header("�A�j���[�V�����֘A")]
    [SerializeField] float ScaleSpeed = 0.1f;                   //�傫���ω��̑���
    [SerializeField] float BreathingAmplitude = 0.05f;          //�ċz�̐U��
    [SerializeField] float BreathingFrequency = 2.0f;           //�ċz�̑���
    private float PrevVelocityY = 0f;                           //�O�t���[����Y�����̑��x
    private bool isHighestPoint = false;                        //�W�����v�̍ō��_�ɓ��B�������ǂ���
    private Vector3 NormalScale = new Vector3(0.3f, 0.4f, 1.0f);//�ʏ펞�̑傫��
    private Vector3 JumpScale = new Vector3(0.15f, 0.6f, 1.0f); //�W�����v���̑傫�� 

    [Header("�e�֘A")]
    [SerializeField] Transform BulletGenerationPosition; //�e�̐����ʒu
    public bool isBulletCooldownTime = false;    //�e�̃N�[���_�E��



    private Rigidbody2D rd; 
    private bool isJamping = false;        //�W�����v�����ǂ���
    private bool LeftWallHit = false;   //���ɕǂ����邩�ǂ���
    private bool RightWallHit = false;  //�E�ɕǂ����邩�ǂ���
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
        //��Ԃ̍X�V
        CheckWall();    //�ǂ��E�ɂ��邩���ɂ��邩���X�V

        //���ړ�
        MoveX = Input.GetAxisRaw("Horizontal");   //���E

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
            isJamping = true;              //�W�����v���ɐݒ�
        }

    }

    private void HandleScale()
    {


        //�A�j���[�V����
        if (!CheckGround()) 
        {
            if (!isHighestPoint && rd.velocity.y < 0) isHighestPoint = true; //�ō��_�ɓ��B�������ǂ����̍X�V

            //�W�����v��
            //�W�����v�A�j���[�V����
            if (isHighestPoint) //�ō��_�ɓ��B������̉��~��
            {
                transform.localScale = Vector3.Lerp(transform.localScale, NormalScale, 0.3f);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, JumpScale, 0.5f);
            }


        }
        else  //�ڒn��
        {
            isJamping = false;         //�W�����v���t���O��false��
            isHighestPoint = false; //�ō��_�ɓ��B�������ǂ����̃t���O��false��

            transform.localScale = Vector3.Lerp(transform.localScale, NormalScale, ScaleSpeed);
        }

    }

    private void PlayerShoot()
    {
        if(Input.GetMouseButtonDown(0) && !isBulletCooldownTime)
        {
            isBulletCooldownTime = true;

            Vector3 mousePos = Input.mousePosition; //�}�E�X�̃X�N���[�����W
            mousePos.z = 10f; //�J��������̋���

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos); //�}�E�X�̃��[���h���W
            direction = (worldMousePos - BulletGenerationPosition.position).normalized; //�e�̔��˕������v�Z���Đ��K��
            OnShoot?.Invoke(BulletGenerationPosition.position, direction);   //�e�̐����ʒu�Ɣ��˕����������ɂ��ăC�x���g�𔭉�
        }


    }
}
