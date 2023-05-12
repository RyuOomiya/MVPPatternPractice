using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    // Views
    [SerializeField] Button _buttonAttack;
    [SerializeField] Button _buttonRecovery;
    [SerializeField] ProgressView _hpProgress;
    [SerializeField] ProgressView _spProgress;
    [SerializeField] MessageDialog _dialog;

    // Models
    [SerializeField] Model _progressModel;

    public void Start()
    {
        // View �� Model
        _buttonAttack.onClick.AsObservable().Subscribe(_ => _progressModel.Damage());
        _buttonRecovery.onClick.AsObservable().Subscribe(_ => _progressModel.Recovery());

        // Model �� View(HP)
        _progressModel.MaxChanged.
            Subscribe(value => _hpProgress.SetMax(value));
        _progressModel.CurrentChanged.
            First().Subscribe(value => _hpProgress.SetCurrent(value));
        _progressModel.CurrentChanged.
            Skip(1).Subscribe(value => _hpProgress.SetCurrent(value, true));

        // Model �� View(SP)
        _progressModel.MaxSpChanged. // �ő�l�̐ݒ�
            Subscribe(value => _spProgress.SetMax(value));
        _progressModel.CurrentSpChanged. // ����̏�����
            First().Subscribe(value => _spProgress.SetCurrent(value));
        _progressModel.CurrentSpChanged. // 2��ڈȍ~�̓A�j���[�V�����t����
            Skip(1).Subscribe(value => _spProgress.SetCurrent(value, true));
        _progressModel.SpEmpty. // SP����̎��̃_�C�A���O�\��
            Subscribe(_ => _dialog.ShowDialog());
    }
}