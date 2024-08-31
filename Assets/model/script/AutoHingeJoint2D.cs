using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHingeJoint2D : MonoBehaviour
{
    public GameObject[] upperBodyParts; // ������ ��ü ��Ʈ��
    public Rigidbody2D abdomenRb;

    void Start()
    {
        foreach (GameObject part in upperBodyParts)
        {
            if (part != null && part.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = part.AddComponent<Rigidbody2D>();
                rb.gravityScale = abdomenRb.gravityScale;
                rb.mass = 1f; // ���� ����
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

            // Anchor ����
            hinge.anchor = Vector2.zero;
            hinge.connectedAnchor = upperBodyParts[i + 1].transform.InverseTransformPoint(upperBodyParts[i].transform.position);

            // Motor ����
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = 100f; // �ӵ� ����
            motor.maxMotorTorque = 1000f; // �ִ� ��ũ ����
            hinge.motor = motor;

            // ���� ����
            JointAngleLimits2D limits = hinge.limits;
            limits.min = -45; // �ּ� ����
            limits.max = 45;  // �ִ� ����
            hinge.limits = limits;
        }
    }
}