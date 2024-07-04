using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위해 필요

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴을 사용하여 어디서든 접근 가능하게 함
    public float speed;
    public Text scoreText; // 스코어를 표시할 텍스트 UI
    private float score; // 현재 스코어

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않도록 함
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
        score += Time.deltaTime;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // 정수형으로 변환하여 텍스트에 표시
        scoreText.text = "Score: " + (int)score;
    }

    public void EndGame()
    {
        // 게임 오버 로직 구현
        Debug.Log("Game Over");
        Time.timeScale = 0; // 게임 시간을 멈춤
        // 여기에 게임 오버 시 보여줄 UI나 로직을 추가할 수 있습니다.
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

    public int getScore()
    {
        return (int)this.score;
    }


}