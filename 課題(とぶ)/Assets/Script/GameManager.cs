using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)] // ���̃X�N���v�g������Ɏ��s�����悤�ɂ���
public class GameManager : MonoBehaviour
{
    [Header("�|�[�^���֘A")]
    [SerializeField] GameObject PortalPrefabGreen;
    [SerializeField] GameObject PortalPrefabPurple;
    private Portal CurrentPortal;
    private GameObject[] Portal = new GameObject[2]; //0:�� 1:��
    private int PortalIndex = 0;                     //���ɏo���|�[�^���̐F(0:�� 1:��)
    private Quaternion rotation = Quaternion.identity;


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //�V�[�����ς���Ă��j������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Bullet.OnBulletHit += SpawnPortal; //Player�X�N���v�g��OnShoot�C�x���g��CreatePortal���\�b�h��o�^
    }

    private void OnDisable()
    {
        Bullet.OnBulletHit -= SpawnPortal; //Player�X�N���v�g��OnShoot�C�x���g����CreatePortal���\�b�h������
    }

    void SpawnPortal(Vector2 hit, Vector2 noraml)
    {
        if (Mathf.Abs(noraml.x) > Mathf.Abs(noraml.y)) //�ǂɑ΂��Đ����ɓ��������ꍇ
        {
            if (noraml.x > 0) //�E�̕ǂɓ��������ꍇ
            {
                rotation = Quaternion.Euler(0, 0, 180);
            }
            else //���̕ǂɓ��������ꍇ
            {
                rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else //�����V��ɑ΂��Đ����ɓ��������ꍇ
        {
            if (noraml.y > 0) //���ɓ��������ꍇ
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
            else //�V��ɓ��������ꍇ
            {
                rotation = Quaternion.Euler(0, 0, 270);
            }
        }

        if (Portal[PortalIndex] != null) //���łɃ|�[�^�������݂���ꍇ�͍폜
        {
            Portal[PortalIndex].GetComponent<Portal>().SetDisappear(true); //������A�j���[�V�������Đ�
        }


        if (PortalIndex == 0) //�΂̃|�[�^�����o��
        {
            Portal[PortalIndex] = Instantiate(PortalPrefabGreen, hit, rotation);
            PortalIndex = 1; //���͎��̃|�[�^�����o��
        }
        else //���̃|�[�^�����o��
        {
            Portal[PortalIndex] = Instantiate(PortalPrefabPurple, hit, rotation);
            PortalIndex = 0; //���͗΂̃|�[�^�����o��
        }

        //���݃����N�̐ݒ�
        if (Portal[0] != null && Portal[1] != null)
        {
            Portal[0].GetComponent<Portal>().OppositePortal = Portal[1].GetComponent<Portal>();
            Portal[1].GetComponent<Portal>().OppositePortal = Portal[0].GetComponent<Portal>();
        }
    }

}
  