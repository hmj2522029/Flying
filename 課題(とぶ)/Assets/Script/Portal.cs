using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal OppositePortal; //���Α��̃|�[�^��
    private Animator PortalAnimation;
    private void Awake()
    {
        PortalAnimation = GetComponent<Animator>();
    }
    public void SetDisappear(bool disappear)
    {
        PortalAnimation.SetBool("isExtinctionTransition", disappear); //���ŃA�j���[�V�����Đ�

        Destroy(gameObject, 0.5f); //0.5�b��ɃI�u�W�F�N�g��j��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //�v���C���[���|�[�^���ɓ������ꍇ
        {
            Player player = collision.GetComponent<Player>();

            //�N�[���_�E�����̓e���|�[�g���Ȃ�
            if (player != null && player.LastPortal == this) return;

            if (OppositePortal != null) //���Α��̃|�[�^�������݂���ꍇ
            {
                Vector3 exitPosition = OppositePortal.transform.position; //���Α��̃|�[�^���̈ʒu���擾

                Vector3 offset = Vector3.zero; //�I�t�Z�b�g��������(�|�[�^���̉�]�ɉ����Đݒ�)
                float distance = 0.2f; //�e���|�[�g��̃v���C���[�ƃ|�[�^���̃e���|�[�g�J��Ԃ��h�~����

                //�|�[�^���̉�]�ɉ����ăI�t�Z�b�g��ݒ�
                float angle = Mathf.Round(OppositePortal.transform.eulerAngles.z); //�|�[�^���̉�]�p�x���擾���Ďl�̌ܓ�(Mathf.Round)
                if (angle == 0) //���̕ǂɐݒu���ꂽ�|�[�^��
                {
                    offset = Vector3.left * distance;
                }
                else if (angle == 180) //�E�̕ǂɐݒu���ꂽ�|�[�^��
                {
                    offset = Vector3.right * distance;
                }
                else if (angle == 90) //���ɐݒu���ꂽ�|�[�^��
                {
                    offset = Vector3.up * distance;
                }
                else if (angle == 270) //�V��ɐݒu���ꂽ�|�[�^��
                {
                    offset = Vector3.down * distance;
                }

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                Vector2 velocityBefore = rb.velocity;                 //�e���|�[�g�O�̑��x��ۑ�
                collision.transform.position = exitPosition + offset; //�v���C���[�̈ʒu�𔽑Α��̃|�[�^���̈ʒu�ɃI�t�Z�b�g���������ʒu�ɐݒ�
                if (rb != null)
                {
                    float maxSpeed = 10f; //�ő呬�x

                    float speed = rb.velocity.magnitude; //���x�̑傫�����擾

                    //���x���ő呬�x�𒴂��Ă���ꍇ�͍ő呬�x�ɐ���
                    if (speed > maxSpeed)
                    {
                        rb.velocity = rb.velocity.normalized * maxSpeed; //���x�̑傫�����ő呬�x�ɐ���
                    }
                    else
                    {
                        rb.velocity = rb.velocity; //���x���ێ�
                    }
                }

                player.LastPortal = OppositePortal; //�Ō�ɓ������|�[�^�����X�V
                player.PortalCooldownTime = 0.2f; //�|�[�^���̃N�[���_�E�����Ԃ����Z�b�g
            }
        }
    }
}
