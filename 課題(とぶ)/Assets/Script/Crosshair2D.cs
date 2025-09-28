using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair2D : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] Transform Crosshair;
    [SerializeField] float Margin = 10f; // ��ʒ[����̃}�[�W��

    // Update is called once per frame
    private void Awake()
    {
        Cursor.visible = false; // �}�E�X�J�[�\�����\���ɂ���
        Cursor.lockState = CursorLockMode.Confined; // �}�E�X�J�[�\������ʓ��ɐ�������
    }
    void Update()
    {
        //�}�E�X���W���X�N���[�����ɐ���
        float x = Mathf.Clamp(Input.mousePosition.x, Margin, Screen.width - Margin);
        float y = Mathf.Clamp(Input.mousePosition.y, Margin, Screen.height - Margin);

        Vector3 mousePos = new Vector3(x, y, 10);

        // �}�E�X���W�����[���h���W�ɕϊ�
        Vector3 WorldPos = MainCamera.ScreenToWorldPoint(mousePos);

        // �Ə����ړ�
        Crosshair.position = WorldPos;
    }
}
