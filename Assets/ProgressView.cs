using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class ProgressView : MonoBehaviour
{
    [SerializeField] Text _textMax;
    [SerializeField] Text _textCurrent;
    [SerializeField] RectTransform _imageRectProgressBack;
    [SerializeField] RectTransform _imageRectProgressFront;

    // バーの最長の長さ
    float _maxProgressWidth;
    // バーの最大値
    int _maxValue;
    // 現在の値(目標値としても使用する)
    int _currentValue;
    // アニメーション時に使用する一時的な現在値
    int _tempCurrentValue;
    // アニメーション用
    Tween _anim;

    private void Awake()
    {
        // 最大の横の長さを覚えておく
        _maxProgressWidth = _imageRectProgressFront.GetWidth();
    }

    public void SetMax(int value)
    {
        _maxValue = value;
        _textMax.text = _maxValue.ToString();
    }

    public void SetCurrent(int newValue, bool useAnimation = false)
    {
        bool isPlus = newValue > _currentValue;

        _currentValue = newValue;
        _anim.Kill();

        // 現在値のバーの長さを更新
        _imageRectProgressFront.SetWidth(GetBarWidth(_currentValue));

        if (!useAnimation || isPlus)
        {
            _textCurrent.text = _currentValue.ToString();
            _tempCurrentValue = _currentValue;
            _imageRectProgressBack.SetWidth(GetBarWidth(newValue));
        }
        else
        {
            _anim = DOTween.To(() => _tempCurrentValue,
                value =>
                {
                    // 背景バーはアニメーションで更新
                    _tempCurrentValue = value;
                    _textCurrent.text = _tempCurrentValue.ToString();
                    _imageRectProgressBack.SetWidth(GetBarWidth(value));
                },
                _currentValue, 0.35f);
        }
    }

    // 指定した値に対応するバーの横幅を取得する
    private float GetBarWidth(int value)
    {
        float per = Mathf.InverseLerp(0, _maxValue, value);
        return Mathf.Lerp(0, _maxProgressWidth, per);
    }
}

public static class UIExtensions
{
    public static float GetWidth(this RectTransform self)
    {
        return self.sizeDelta.x;
    }

    public static void SetWidth(this RectTransform self, float width)
    {
        Vector2 s = self.sizeDelta;
        s.x = width;
        self.sizeDelta = s;
    }
}

