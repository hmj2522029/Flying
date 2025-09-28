using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair2D : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] Transform Crosshair;
    [SerializeField] float Margin = 10f; // 画面端からのマージン

    // Update is called once per frame
    private void Awake()
    {
        Cursor.visible = false; // マウスカーソルを非表示にする
        Cursor.lockState = CursorLockMode.Confined; // マウスカーソルを画面内に制限する
    }
    void Update()
    {
        //マウス座標をスクリーン内に制限
        float x = Mathf.Clamp(Input.mousePosition.x, Margin, Screen.width - Margin);
        float y = Mathf.Clamp(Input.mousePosition.y, Margin, Screen.height - Margin);

        Vector3 mousePos = new Vector3(x, y, 10);

        // マウス座標をワールド座標に変換
        Vector3 WorldPos = MainCamera.ScreenToWorldPoint(mousePos);

        // 照準を移動
        Crosshair.position = WorldPos;
    }
}
