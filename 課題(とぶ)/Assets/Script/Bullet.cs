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


        Ray2D ray = new Ray2D(RayPos.position, m_player.direction);
        WallHit = Physics2D.Raycast(ray.origin, ray.direction, 0.2f);

        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.green, 0.015f);

        if(WallHit.collider)
        {
            if (WallHit.collider.gameObject.layer == 3 || //����
                WallHit.collider.gameObject.layer == 6 || //���
                WallHit.collider.gameObject.layer == 7 || //����
                WallHit.collider.gameObject.layer == 8 )  //�E��
            {
                Debug.Log("�ǂ̂ǂꂩ�ɓ�������");
            }

            //Debug.Log(WallHit.collider.gameObject.layer);
            //Debug.Log("���������I�u�W�F�N�g�̃��C���[��: " + LayerMask.LayerToName(collision.gameObject.layer));
            Debug.DrawRay(WallHit.point, WallHit.normal * 0.2f, Color.green, 0.015f);
        }
    }

    void OnBecameInvisible()
    {
        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //��ʊO�ɏo�������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(WallHit.collider.gameObject.layer);
        Debug.Log("���������I�u�W�F�N�g�̃��C���[��: " + LayerMask.LayerToName(collision.gameObject.layer)); // ���C���[��

        Debug.Log(collision.gameObject);

        m_player.isBulletCooldownTime = false;
        Destroy(gameObject); //�����ɓ������������
    }

}
