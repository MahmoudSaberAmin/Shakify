using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakify : MonoBehaviour
{
    [SerializeField] private float _influencePosition = 1f;
    [SerializeField] private float _influenceRotation = 1f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _smoothing = 1f;
    [SerializeField] private int _frameOffset = 0;

    private const float _fps = 24f;

    private float _lastTime = 0;
    private float _duration;
    private int _index = 0;

    // Start is called before the first frame update
    void Start()
    {
        _duration = 1f / _fps;
        _index = _index + _frameOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.time - _lastTime > _duration / _speed)
        {
            _lastTime = Time.time;

            _index = ++_index % int.MaxValue;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, _influencePosition * CameraDataManager.Instance.GetPosition(ShakifyType.Investigation, _index), Time.deltaTime * _fps * _smoothing);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_influenceRotation * CameraDataManager.Instance.GetEuler(ShakifyType.Investigation, _index)), Time.deltaTime * _fps * _smoothing);
    }
}
