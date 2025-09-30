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
        PortalAnimation.SetBool("isExtinctionTransition", disappear); //消滅アニメーション再生

        Destroy(gameObject, 0.5f); //0.5秒後にオブジェクトを破棄
    }

}
