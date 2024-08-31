using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHingeJoint2D : MonoBehaviour
{
    public GameObject[] upperBodyParts; // 연결할 상체 파트들
    public Rigidbody2D abdomenRb;

    void Start()
    {
        foreach (GameObject part in upperBodyParts)
        {
            if (part != null && part.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = part.AddComponent<Rigidbody2D>();
                rb.gravityScale = abdomenRb.gravityScale;
                rb.mass = 1f; // 질량 설정
            }
        }
        ConnectBodyParts();
    }

    void ConnectBodyParts()
    {
        for (int i = 0; i < upperBodyParts.Length - 1; i++)
        {
            HingeJoint2D hinge = upperBodyParts[i].AddComponent<HingeJoint2D>();
            hinge.connectedBody = upperBodyParts[i + 1].GetComponent<Rigidbody2D>();

            // Anchor 설정
            hinge.anchor = Vector2.zero;
            hinge.connectedAnchor = upperBodyParts[i + 1].transform.InverseTransformPoint(upperBodyParts[i].transform.position);

            // Motor 설정
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = 100f; // 속도 조절
            motor.maxMotorTorque = 1000f; // 최대 토크 조절
            hinge.motor = motor;

            // 제한 설정
            JointAngleLimits2D limits = hinge.limits;
            limits.min = -45; // 최소 각도
            limits.max = 45;  // 최대 각도
            hinge.limits = limits;
        }
    }
}