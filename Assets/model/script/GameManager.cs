using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI; // UI 요소를 사용하기 위해 필요
using System.Collections;
using System;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴을 사용하여 어디서든 접근 가능하게 함
    private float speed;
    public TextMeshProUGUI scoreText; // 스코어를 표시할 텍스트 UI
    private float score; // 현재 스코어
    public GameObject player; // 캐릭터의 최상위 오브젝트를 참조
    private Boolean playerDead;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 중복 생성 방지
        }
    }

    void Start()
    {
        score = 0;
        UpdateScoreText(); // 처음 실행 시 스코어 텍스트 업데이트

    }

    void Update()
    {
        if(!playerDead)
        score += Time.deltaTime;
        UpdateScoreText();

    }

    void UpdateScoreText()
    {
        // 정수형으로 변환하여 텍스트에 표시
        scoreText.text = "Score : " + (int)score  + " M";
    }

    public void EndGame()
    {

        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        DeadPlayer(); // 플레이어 사망 처리
        GamePauseManager.instance.menuBtnDisable();
        LeaderBoardManager.instance.SubmitScore((int)score);
        AudioEffectManager.Instance.AudioClipPlay(0);

        yield return new WaitForSeconds(2f);

        GamePauseManager.instance.DeadPlayer();




        Debug.Log("Game Over");
        Time.timeScale = 0; // 게임 시간을 멈춤
    }

    public string getName()
    {
        if (!PlayerPrefs.HasKey("nickname"))
        {
            PlayerPrefs.SetString("name", initName());
        }
        return PlayerPrefs.GetString("nickname");
    }

    public string initName()
    {
        return "run" + UnityEngine.Random.Range(0, 210000000);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public float getSpeed()
    {
        return this.speed;
    }

    public void setPlayerDead(Boolean isDead)
    {
        this.playerDead = isDead;
    }
    public Boolean getPlayerDead()
    {
        return this.playerDead;
    }


    public int getScore()
    {
        return (int)this.score;
    }

    private void DeadPlayer()
    {
        // 캐릭터 오브젝트 내의 모든 RigidBody 컴포넌트를 가져옴
        Transform[] allChildren = player.GetComponentsInChildren<Transform>();
        // 캐릭터 오브젝트 내의 모든 RigidBody2D 컴포넌트를 가져옴
        Rigidbody2D[] rigidbodies = player.GetComponentsInChildren<Rigidbody2D>();
        HingeJoint2D[] hingeJoints = player.GetComponentsInChildren<HingeJoint2D>();
        // 현재 오브젝트의 모든 자식 뼈를 찾음

        // 모든 게임 오브젝트를 가져옵니다.
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (Transform bone in allChildren)
        {
            if (bone.name.Contains("bone"))
            {
                Destroy(bone.Find(bone.name));
            }
            else
            {
                // SpriteSkin 컴포넌트가 있는 경우 삭제
                SpriteSkin spriteSkin = bone.GetComponent<SpriteSkin>();
                if (spriteSkin != null)
                {
                    Destroy(spriteSkin);
                }
            }

            // Collider2D 추가 (BoxCollider2D를 예로 사용)
            if (bone.gameObject.GetComponent<Collider2D>() == null)
            {
                bone.gameObject.AddComponent<BoxCollider2D>();
            }

            // Rigidbody2D 추가
            if (bone.gameObject.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = bone.gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 1; // 중력 설정
                rb.isKinematic = false; // 물리적 상호작용을 가능하게 함
            }
        }

        // 모든 HingeJoint2D를 해체
        foreach (HingeJoint2D hinge in hingeJoints)
        {
            hinge.enabled = false; // 조인트 비활성화
        }

        // 캐릭터의 Animator 컴포넌트를 가져옴
        Animator animator = player.GetComponent<Animator>();

        if (animator != null)
        {
            // 현재 애니메이션 상태를 유지하도록 애니메이터를 비활성화
            animator.enabled = false;
        }




    }
}