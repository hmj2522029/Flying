using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    private TextMeshProUGUI Text;

    // �t�F�[�h�̏��v���ԁi�b�j
    [SerializeField] private float fadeDuration = 1.0f;

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.alpha = 0;
    }

    // �t�F�[�h�C������ (�A���t�@�l��1����0�ɕύX)
    public void FadeIn()
    {
        // �R���[�`�����J�n���Ĕ񓯊��ŏ������s��
        StartCoroutine(Fade(1, 0));
    }

    // �t�F�[�h�A�E�g���� (�A���t�@�l��0����1�ɕύX)
    public void FadeOut()
    {
        // �R���[�`�����J�n���Ĕ񓯊��ŏ������s��
        StartCoroutine(Fade(0, 1));
    }

    // ���ۂ̃t�F�[�h���� (startAlpha����endAlpha�ɏ��X�ɕω�)
    private IEnumerator Fade(float StartAlpha, float EndAlpha)
    {
        float time = 0f; // �o�ߎ��Ԃ�ǐՂ���ϐ�
        Color FadeColor = Text.color; // ���݂�Image�̐F���擾

        // �w�肵�����ԁifadeDuration�j���o�߂���܂Ń��[�v
        while (time < fadeDuration)
        {
            time += Time.deltaTime; // �o�ߎ��Ԃ����Z
            float t = time / fadeDuration; // ���Ԃ̐i�s���� (0.0�`1.0)
            FadeColor.a = Mathf.Lerp(StartAlpha, EndAlpha, t); // �A���t�@�l����`��ԂŌv�Z
            Text.color = FadeColor; // �v�Z���ʂ̃A���t�@�l��Image�ɓK�p
            yield return null; // ���̃t���[���܂ŏ������ꎞ��~
        }

        // �ŏI�I�ȃA���t�@�l�𐳊m�ɐݒ�
        FadeColor.a = EndAlpha;
        Text.color = FadeColor;
    }
}
