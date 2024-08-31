using System.Collections.Generic;
using UnityEngine;

public class AutoBoneConnector : MonoBehaviour
{
    public List<GameObject> bones = new List<GameObject>();
    public float maxTiltAngle = 60f;

    void Start()
    {
        // ��� �� ��ü�� ���� HingeJoint2D�� �߰��ϰ� ����
        for (int i = 0; i < bones.Count - 1; i++)
        {
            if (bones[i] != null && bones[i + 1] != null)
            {
                HingeJoint2D joint = bones[i].AddComponent<HingeJoint2D>();
                joint.connectedBody = bones[i + 1].GetComponent<Rigidbody2D>();

                // ���� ���� ����
                joint.useLimits = true;
                JointAngleLimits2D limits = joint.limits;
                limits.min = -maxTiltAngle;
                limits.max = maxTiltAngle;
                joint.limits = limits;
            }
        }
    }
}
