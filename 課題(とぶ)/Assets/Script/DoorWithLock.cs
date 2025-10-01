using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLock : MonoBehaviour
{
    private GameObject OpenDoor;


    private void Start()
    {
        OpenDoor = Resources.Load<GameObject>("doorOpen"); //Resources�t�H���_����J�����h�A��Prefab�����[�h
    }

    private void OnEnable()
    {
        KeyHole.OnDoorOpen += Switching; //KeyHandler�X�N���v�g��OnUseKey�C�x���g��Switching���\�b�h��o�^
    }

    private void OnDisable()
    {
        KeyHole.OnDoorOpen -= Switching; //KeyHandler�X�N���v�g��OnUseKey�C�x���g����Switching���\�b�h������
    }

    private void Switching(Transform doorT)
    {
        Instantiate(OpenDoor, doorT.position, Quaternion.identity); //�J�����h�A�𐶐�
        Destroy(doorT.gameObject); //�����t���h�A��j��
    }

}
