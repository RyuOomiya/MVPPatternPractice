using System;
using UniRx;
using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField] int _debugMaxHp; // 300
    [SerializeField] int _debugCurrentHp; // 300
    [SerializeField] int _debugMaxSp; // 50
    [SerializeField] int _debugCurrentSp; // 50

    public int MaxHp { get => _maxHp.Value; set => _maxHp.Value = value; }
    public IObservable<int> MaxChanged => _maxHp;
    private readonly ReactiveProperty<int> _maxHp = new();

    public int CurrentHp { get => _currentHp.Value; set => _currentHp.Value = value; }
    public IObservable<int> CurrentChanged => _currentHp;
    private readonly ReactiveProperty<int> _currentHp = new();

    public int MaxSp { get => _maxSp.Value; set => _maxSp.Value = value; }
    public IObservable<int> MaxSpChanged => _maxSp;
    private readonly ReactiveProperty<int> _maxSp = new();

    public int CurrentSp { get => _currentSp.Value; set => _currentSp.Value = value; }
    public IObservable<int> CurrentSpChanged => _currentSp;
    private readonly ReactiveProperty<int> _currentSp = new();

    public IObservable<int> SpEmpty => _spEmpty;
    private readonly Subject<int> _spEmpty = new();

    public void Start()
    {
        // デバッグ用の初期値を設定
        _maxHp.Value = _debugMaxHp;
        _currentHp.Value = _debugCurrentHp;
        _maxSp.Value = _debugMaxSp;
        _currentSp.Value = _debugCurrentSp;
    }

    public void Damage()
    {
        int _next = _currentHp.Value;
        _next -= UnityEngine.Random.Range(10, 30);
        if (_next < 0)
        {
            _next = 0;
        }
        _currentHp.Value = _next;
    }

    public void Recovery()
    {
        int _spNext = _currentSp.Value - 5; // 固定値
        if (_spNext < 0)
        {
            _spEmpty.OnNext(0);
            return;
        }
        _currentSp.Value = _spNext;

        int _next = _currentHp.Value;
        _next += UnityEngine.Random.Range(10, 30);
        if (_next > _maxHp.Value)
        {
            _next = _maxHp.Value;
        }
        _currentHp.Value = _next;
    }
}