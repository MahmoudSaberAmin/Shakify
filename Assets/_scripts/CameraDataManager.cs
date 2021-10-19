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
    public ShakifyType type;
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] public List<Vector3> _eulers;

    public Vector3 GetPositionAtIndex(int index)
    {
        return _positions[index%_positions.Count];
    }
    public Vector3 GetEulerAtIndex(int index)
    {
        return _eulers[index%_eulers.Count];
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
            _hashedData[shakifyData.type] = shakifyData;
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
