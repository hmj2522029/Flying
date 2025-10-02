using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public TitlePortal LastPortalT;              //�^�C�g���V�[���̂Ƃ������g��
    public float PortalCooldownTime = 0.5f; //�|�[�^���̃N�[���_�E������

    private void Update()
    {
        PreventRepeatedTeleportation();
    }
    private void PreventRepeatedTeleportation() //�|�[�^���̘A���e���|�[�g��h��
    {
        if (PortalCooldownTime > 0)
        {
            PortalCooldownTime -= Time.deltaTime;

            if (PortalCooldownTime <= 0)
            {
                LastPortalT = null;         //�Ō�ɏo���|�[�^�������Z�b�g
                PortalCooldownTime = 0.5f; //�N�[���_�E�����Ԃ����Z�b�g

            }
        }
    }
}


