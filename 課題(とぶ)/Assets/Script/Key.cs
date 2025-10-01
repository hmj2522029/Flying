using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] float floatAmplitude = 0.25f; // �㉺�̕�
    [SerializeField] float floatFrequency = 1f;    // ����

    private Vector3 startPosition;
    private Vector3 originalScale; // ���̑傫��
    private Player player;


    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        FloatEffect();
    }

    private void FloatEffect()
    {
        if(player.HasKey) return; // �v���C���[�����Ɍ��������Ă���ꍇ�͓������Ȃ�

        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude; // �T�C���g���g���ď㉺�ɓ�����
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.HasKey = true; // �v���C���[�����������Ă��邱�Ƃ�����

                //����̌��A�C�R����\��
                Vector3 offset = new Vector3(0, 0.5f, 0); // �A�C�R���̈ʒu�����p�I�t�Z�b�g
                player.KeyImage = Instantiate(gameObject, player.transform.position + offset, Quaternion.identity);

                player.KeyImage.AddComponent<FollowPlayer>().Follow(player.transform, offset);  // �v���C���[�̓���ɒǏ]������

                //���I�u�W�F�N�g���\��
                gameObject.SetActive(false);
            }
        }
    }
}

 
