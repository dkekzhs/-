using System.Collections.Generic;
using UnityEngine;

public class AutoBoneConnector : MonoBehaviour
{
    public List<GameObject> bones = new List<GameObject>();
    public float maxTiltAngle = 60f;

    void Start()
    {
        // 모든 본 객체에 대해 HingeJoint2D를 추가하고 연결
        for (int i = 0; i < bones.Count - 1; i++)
        {
            if (bones[i] != null && bones[i + 1] != null)
            {
                HingeJoint2D joint = bones[i].AddComponent<HingeJoint2D>();
                joint.connectedBody = bones[i + 1].GetComponent<Rigidbody2D>();

                // 각도 제한 설정
                joint.useLimits = true;
                JointAngleLimits2D limits = joint.limits;
                limits.min = -maxTiltAngle;
                limits.max = maxTiltAngle;
                joint.limits = limits;
            }
        }
    }
}
