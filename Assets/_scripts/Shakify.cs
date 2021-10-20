using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakifyParameters
{
    public ShakifyType _type;
    public float _influencePosition = 1f;
    public float _influenceRotation = 1f;
    public float _speed = 1f;
    public int _frameOffset = 0;

    [HideInInspector]
    public int _index = 0;
    [HideInInspector]
    public float _lastTime = 0;
    [HideInInspector]
    public float _duration { get { return 1 / _fps; } }
    public const float _fps = 24f;

}
public class Shakify : MonoBehaviour
{
    [SerializeField] private List<ShakifyParameters> _parametersList;
    [SerializeField] private float _smoothing = 1f;
    
    private const float _fps = 24f;

    private Vector3 pos;
    private Vector3 rot;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var parameter in _parametersList)
        {
            parameter._index = parameter._index + parameter._frameOffset;
        }

        pos = transform.localPosition;
        rot = transform.localEulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (var parameter in _parametersList)
        {
            if (Time.time - parameter._lastTime > parameter._duration / parameter._speed)
            {
                parameter._lastTime = Time.time;

                parameter._index = ++parameter._index % int.MaxValue;

                pos = pos -transform.localPosition + parameter._influencePosition * CameraDataManager.Instance.GetPosition(parameter._type, parameter._index);
                rot = rot -transform.localEulerAngles + parameter._influenceRotation * CameraDataManager.Instance.GetEuler(parameter._type, parameter._index);
            }
        }


        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime * _fps * _smoothing);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(rot), Time.deltaTime * _fps * _smoothing);
    }
}
