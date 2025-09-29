using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed = 10f; //�e�̑��x
    private Rigidbody2D rb;
    private Vector3 MoveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //�v���C���[�ƏՓ˂��Ȃ��悤�ɂ���
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(),  //Player�^�O�̂����I�u�W�F�N�g���擾���Ă���Collider2D���擾
                                  GetComponent<Collider2D>()); //���̃I�u�W�F�N�g��Collider2D���擾���ďՓ˂𖳎�����
    }
    public void SetMoveDir(Vector3 dir)
    {
        MoveDir = dir; //�ړ������𐳋K�����Đݒ�
    }

    void Update()
    {
        rb.velocity = MoveDir * Speed; //�ړ������ɑ��x�������Ĉړ�
    }

}
