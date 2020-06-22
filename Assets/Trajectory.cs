using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] LineRenderer line;

    [SerializeField] int lineSegment = 10;
    [SerializeField] Transform muzzle;
    [SerializeField] Vector3 vo;

    private void Start()
    {
        line.positionCount = lineSegment;
    }

    void Update()
    {
        Visulaize(vo);
    }

    Vector3 CalculatePositionInTime(Vector3 vo, float time)
    {
        Vector3 vxz = vo;
        vxz.y = 0f;

        Vector3 result = muzzle.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + muzzle.position.y;

        result.y = sY;

        return result;
    }

    void Visulaize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePositionInTime(vo, i / (float)lineSegment);

            line.SetPosition(i, pos);
        }
    }
}
