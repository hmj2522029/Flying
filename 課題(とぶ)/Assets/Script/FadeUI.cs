using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    private TextMeshProUGUI Text;

    // フェードの所要時間（秒）
    [SerializeField] private float fadeDuration = 1.0f;

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.alpha = 0;
    }

    // フェードイン処理 (アルファ値を1から0に変更)
    public void FadeIn()
    {
        // コルーチンを開始して非同期で処理を行う
        StartCoroutine(Fade(1, 0));
    }

    // フェードアウト処理 (アルファ値を0から1に変更)
    public void FadeOut()
    {
        // コルーチンを開始して非同期で処理を行う
        StartCoroutine(Fade(0, 1));
    }

    // 実際のフェード処理 (startAlphaからendAlphaに徐々に変化)
    private IEnumerator Fade(float StartAlpha, float EndAlpha)
    {
        float time = 0f; // 経過時間を追跡する変数
        Color FadeColor = Text.color; // 現在のImageの色を取得

        // 指定した時間（fadeDuration）が経過するまでループ
        while (time < fadeDuration)
        {
            time += Time.deltaTime; // 経過時間を加算
            float t = time / fadeDuration; // 時間の進行割合 (0.0～1.0)
            FadeColor.a = Mathf.Lerp(StartAlpha, EndAlpha, t); // アルファ値を線形補間で計算
            Text.color = FadeColor; // 計算結果のアルファ値をImageに適用
            yield return null; // 次のフレームまで処理を一時停止
        }

        // 最終的なアルファ値を正確に設定
        FadeColor.a = EndAlpha;
        Text.color = FadeColor;
    }
}
