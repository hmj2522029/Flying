using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;// ���X�|�[���|�C���g
    [SerializeField] Player player;         // �v���C���[�I�u�W�F�N�g
    [SerializeField] Key key;               // ���I�u�W�F�N�g

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = RespawnPoint.position; // �v���C���[�����X�|�[���|�C���g�Ɉړ�

            // �������Z�b�g
            player.HasKey = false;
            if (player.KeyImage != null)
            {
                Destroy(player.KeyImage); // ����̌��A�C�R����j��
            }
            if (key != null)
            {
                key.gameObject.SetActive(true); // ���I�u�W�F�N�g���ĕ\��
            }
        }

    }
}

