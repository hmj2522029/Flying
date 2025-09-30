using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform RayPos;
    [SerializeField] float Speed = 10f; //�e�̑��x
    private Rigidbody2D rb;
    private Vector3 MoveDir;
    private Player m_player;
    RaycastHit2D WallHit;

    public static event Action<Vector2, Vector2> OnBulletHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_player = GameObject.Find("Player").GetComponent<Player>();

        //�v���C���[�ƏՓ˂��Ȃ��悤�ɂ���
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(),  //Player�^�O�̂����I�u�W�F�N�g���擾���Ă���Collider2D���擾
                                  GetComponent<Collider2D>()); //���̃I�u�W�F�N�g��Collider2D���擾���ďՓ˂𖳎�����
    }
    public void SetMoveDir(Vector3 dir)
    {
        MoveDir = dir; //�ړ�������ݒ�
    }

    void Update()
    {
        rb.velocity = MoveDir * Speed; //�ړ������ɑ��x�������Ĉړ�


    }

    void OnBecameInvisible()
    {
        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //��ʊO�ɏo�������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˂����Ƃ��̏���
        ContactPoint2D contact = collision.contacts[0]; //�Փ˂����_�̏����擾

        Vector2 noraml = contact.normal;                //�Փ˂������̒e�̖@���x�N�g�����擾
        Vector2 hit = contact.point;                    //�Փ˂����_�̍��W���擾

        OnBulletHit?.Invoke(hit, noraml);               //�e�������ɓ��������Ƃ��̃C�x���g�𔭉�

        Destroy(gameObject); //�����ɓ������������
    }

}
