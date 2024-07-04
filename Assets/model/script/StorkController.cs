using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkController : MonoBehaviour
{
    public GameObject head;
    public float walkSpeed = 2f;
    public float tiltForce = 100f;
    private float speedIncreaseInterval = 10f;
    private Rigidbody2D headRb;
    public float gravityScale = 1f;
    public float maxTiltAngle = 60f;

    private float currentTiltForce = 0f;
    private int lastScoreCheckpoint = 0;

    void Start()
    {
        if (head != null)
        {
            headRb = head.GetComponent<Rigidbody2D>();
            if (headRb == null)
            {
                headRb = head.AddComponent<Rigidbody2D>();
            }

            headRb.gravityScale = gravityScale;
            headRb.AddTorque(Random.Range(-100, 100) * 0.1f);
        }
        else
        {
            Debug.LogError("Head GameObject is not assigned.");
        }
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        IncreaseSpeedOverDistance();
        ApplyTilt();
        RotateHead();
    }

    void HandleInput()
    {
        if (headRb != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentTiltForce = Random.Range(tiltForce, tiltForce / 2f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currentTiltForce = Random.Range(-tiltForce / 2f, -tiltForce);
            }
        }
    }

    void ApplyTilt()
    {
        if (headRb != null && currentTiltForce != 0)
        {
            headRb.AddTorque(currentTiltForce * Time.fixedDeltaTime);
            currentTiltForce = 0; // Reset force after applying to ensure one-time application per key press
        }
    }

    void IncreaseSpeedOverDistance()
    {
        //이전 값 같으면 리턴
        int score = GameManager.instance.getScore();
        if (lastScoreCheckpoint == score) return;
        lastScoreCheckpoint = score;

        if (score % 10 == 0 && score != 0)
        {
            walkSpeed += 2f;
            tiltForce += 1000f;
        }
        GameManager.instance.setSpeed(walkSpeed);
    }

    void RotateHead()
    {
        if (headRb != null)
        {
            // 머리의 회전 각도를 제한하여 반원 형태로 움직이도록 함
            float currentAngle = headRb.rotation;
            if (currentAngle > maxTiltAngle)
            {
                headRb.rotation = maxTiltAngle;
            }
            else if (currentAngle < -maxTiltAngle)
            {
                headRb.rotation = -maxTiltAngle;
            }

            // 머리의 회전 속도를 줄여 부드럽게 회전하도록 함
            headRb.angularVelocity *= 0.9f;
        }
    }

}
