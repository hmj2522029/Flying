using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{

    [SerializeField] GameObject BulletPredab; //�e�̃v���n�u
    [SerializeField] Rigidbody2D rb;          //�e��Rigidbody2D

    private void OnEnable() //�I�u�W�F�N�g���L���ɂȂ����Ƃ��ɌĂ΂��
    {
        Player.OnShoot += SpawnBullet; //Player�N���X��OnShoot�C�x���g��SpawnBullet���\�b�h��o�^
    }

    private void OnDisable() //�I�u�W�F�N�g�������ɂȂ����Ƃ��ɌĂ΂��
    {
        Player.OnShoot -= SpawnBullet; //Player�N���X��OnShoot�C�x���g����SpawnBullet���\�b�h������
    }

    void SpawnBullet(Vector3 position, Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //�ړ���������p�x���v�Z
        GameObject bullet = Instantiate(BulletPredab, position, Quaternion.Euler(0, 0, angle)); //�e�̐���
        bullet.GetComponent<Bullet>().SetMoveDir(direction);                          //�e�̕�����ݒ�
    }

}
