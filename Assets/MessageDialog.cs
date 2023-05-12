using DG.Tweening;
using UnityEngine;

public class MessageDialog : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    Tween _anim;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(false); // 最初は消しておく
    }

    public void ShowDialog()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
        _anim.Kill();

        // 1秒待機してから1秒かけてフェードアウト
        _anim = DOTween.Sequence().
            AppendInterval(1).
            Append(_canvasGroup.DOFade(0, 1)).
            OnComplete(() => gameObject.SetActive(false));
    }
}