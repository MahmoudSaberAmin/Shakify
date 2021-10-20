using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShakifyType
{
    Investigation = 0,
    Closeup = 1,
    Wedding = 2,
    WalkToTheStore  =3,
    HandyCamRun = 4,
    OutCarWindow = 5,
    BikeOnGravel2D = 6,
    SpaceShipShake2D = 7,
    Zeek2D = 8,
}


[System.Serializable]
public class ShakifyData
{
    public string Name = "Closeup";
    public ShakifyType Type;
    [SerializeField]
    private List<Vector3> _positions;
    [SerializeField] public List<Vector3> _eulers;


    public Vector3 GetPositionAtIndex(int index)
    {
        return (_positions ==null || _positions.Count==0)?Vector3.zero: _positions[index%_positions.Count];
    }
    public Vector3 GetEulerAtIndex(int index)
    {
        return _eulers[index%_eulers.Count];
    }

    public override string ToString()
    {
        return Type.ToString();
    }
}

public class CameraDataManager : MonoBehaviour
{
    public static CameraDataManager Instance;
    [SerializeField] private List<ShakifyData> _rawData;
    private Dictionary<ShakifyType, ShakifyData> _hashedData = new Dictionary<ShakifyType, ShakifyData>();// work around because Unity doesnot allow serialiazation of dictionaries and cant add odin to an open source code

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ProcessData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ProcessData()
    {
        foreach (var shakifyData in _rawData)
        {
            _hashedData[shakifyData.Type] = shakifyData;
        }
    }

    public Vector3 GetPosition(ShakifyType type, int index)
    {
        return _hashedData[type].GetPositionAtIndex(index);
    }
    public Vector3 GetEuler(ShakifyType type, int index)
    {
        return _hashedData[type].GetEulerAtIndex(index);
    }
}
