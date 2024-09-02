using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public GameObject menuPanel; // 메뉴 패널
    public GameObject darkBackgroundPanel; // 배경을 어둡게 하는 패널
    public Button pasueBtn;
    public GameObject deadMneuPanel;
    public static GamePauseManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance); // 이미 존재하면 중복 생성을 방지
        }
    }

    public void Start()
    {
        // 배경 어둡게 하는 패널 비활성화
        darkBackgroundPanel.SetActive(false);

        // 메뉴 패널 비활성화
        menuPanel.SetActive(false);
        deadMneuPanel.SetActive(false);
    }

    public void menuBtnDisable()
    {
        pasueBtn.interactable = false;
    }

    public void DeadPlayer()
    {
        // 배경 어둡게 만들기
        darkBackgroundPanel.SetActive(true);
        // 메뉴 패널 활성화
        deadMneuPanel.SetActive(true);
    }


    // 'Retry' 버튼을 누르면 호출될 함수
    public void OnRetryButtonClick()
    {
        // 배경 어둡게 만들기
        darkBackgroundPanel.SetActive(true);

        // 메뉴 패널 활성화
        menuPanel.SetActive(true);

        pasueBtn.interactable = false;
        // 게임의 시간을 멈춤
        Time.timeScale = 0f;

    }

    // 'Resume' 버튼을 누르면 호출될 함수
    public void OnResumeButtonClick()
    {
        // 배경 어둡게 하는 패널 비활성화
        darkBackgroundPanel.SetActive(false);

        // 메뉴 패널 비활성화
        menuPanel.SetActive(false);

        pasueBtn.interactable = true;

        // 게임의 시간을 다시 흐르게 함
        Time.timeScale = 1f;
    }
}