using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public GameObject menuPanel; // �޴� �г�
    public GameObject darkBackgroundPanel; // ����� ��Ӱ� �ϴ� �г�
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
            Destroy(instance); // �̹� �����ϸ� �ߺ� ������ ����
        }
    }

    public void Start()
    {
        // ��� ��Ӱ� �ϴ� �г� ��Ȱ��ȭ
        darkBackgroundPanel.SetActive(false);

        // �޴� �г� ��Ȱ��ȭ
        menuPanel.SetActive(false);
        deadMneuPanel.SetActive(false);
    }

    public void menuBtnDisable()
    {
        pasueBtn.interactable = false;
    }

    public void DeadPlayer()
    {
        // ��� ��Ӱ� �����
        darkBackgroundPanel.SetActive(true);
        // �޴� �г� Ȱ��ȭ
        deadMneuPanel.SetActive(true);
    }


    // 'Retry' ��ư�� ������ ȣ��� �Լ�
    public void OnRetryButtonClick()
    {
        // ��� ��Ӱ� �����
        darkBackgroundPanel.SetActive(true);

        // �޴� �г� Ȱ��ȭ
        menuPanel.SetActive(true);

        pasueBtn.interactable = false;
        // ������ �ð��� ����
        Time.timeScale = 0f;

    }

    // 'Resume' ��ư�� ������ ȣ��� �Լ�
    public void OnResumeButtonClick()
    {
        // ��� ��Ӱ� �ϴ� �г� ��Ȱ��ȭ
        darkBackgroundPanel.SetActive(false);

        // �޴� �г� ��Ȱ��ȭ
        menuPanel.SetActive(false);

        pasueBtn.interactable = true;

        // ������ �ð��� �ٽ� �帣�� ��
        Time.timeScale = 1f;
    }
}