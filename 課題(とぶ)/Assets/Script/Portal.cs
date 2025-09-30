using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
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

}
