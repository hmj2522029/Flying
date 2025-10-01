using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHole : MonoBehaviour
{
    [SerializeField] Transform DoorWithLock;
    private GameObject block;
    public static event System.Action<Transform> OnDoorOpen;

    private void Start()
    {
        block = Resources.Load<GameObject>("blockRed"); // Resources�t�H���_����Block�v���n�u�����[�h
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.HasKey)
            {
                player.HasKey = false; // �����g�����̂Ŏ����Ă��Ȃ���Ԃɂ���
                Instantiate(block, transform.position, Quaternion.identity); 

                OnDoorOpen?.Invoke(DoorWithLock); // �h�A���J���C�x���g�𔭉�
                Destroy(player.KeyImage);         // ����̌��A�C�R����j��
                Destroy(gameObject);              // �����I�u�W�F�N�g��j��
            }
        }
    }
}

