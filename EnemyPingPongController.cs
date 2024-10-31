using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPingPongController : MonoBehaviour
{
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _period = 1;
    [SerializeField, Range(0, 1)] private float _phase = 0;

    private enum Axis
    {
        X,
        Y,
        Z
    }

    [SerializeField] private Axis _axis;

    private Transform _transform;
    private Vector3 _initPosition;

    private bool isStopped = false;
    private float stopTime = 0f;

    private void Awake()
    {
        _transform = transform;
        _initPosition = _transform.localPosition;
    }

    private void Update()
    {
        if (isStopped)
        {
            stopTime -= Time.deltaTime;
            if (stopTime <= 0)
            {
                isStopped = false;
            }
            return;
        }

        var t = 4 * _amplitude * (Time.time / _period + _phase + 0.25f);
        var value = Mathf.PingPong(t, 2 * _amplitude) - _amplitude;

        var changePos = Vector3.zero;

        switch (_axis)
        {
            case Axis.X:
                changePos.x = value;
                break;
            case Axis.Y:
                changePos.y = value;
                break;
            case Axis.Z:
                changePos.z = value;
                break;
        }

        _transform.localPosition = _initPosition + changePos;
    }

    public void StopEnemy(float duration)
    {
        isStopped = true;
        stopTime = duration;
    }
}
