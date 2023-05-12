using DG.Tweening;
using UnityEngine;

public class MessageDialog : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    Tween _anim;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(false); // �ŏ��͏����Ă���
    }

    public void ShowDialog()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
        _anim.Kill();

        // 1�b�ҋ@���Ă���1�b�����ăt�F�[�h�A�E�g
        _anim = DOTween.Sequence().
            AppendInterval(1).
            Append(_canvasGroup.DOFade(0, 1)).
            OnComplete(() => gameObject.SetActive(false));
    }
}