using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;


public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance; // Singleton 인스턴스
    public GameObject notificationPanel; // 알림창 패널
    public TextMeshProUGUI notificationText; // 알림 메시지 텍스트
    public float displayDuration = 1.5f; // 알림창 표시 시간

    private void Awake()
    {
        // Singleton 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 이 GameObject는 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }

    private void Start()
    {
        notificationPanel.SetActive(false); // 시작 시 알림창 비활성화
    }

    public void ShowNotification(string message)
    {
        notificationText.text = message; // 메시지 설정
        notificationPanel.SetActive(true); // 알림창 활성화

        // 알림창을 화면 중앙에서 위로 나타나게 함
        notificationPanel.transform.localScale = Vector3.zero; // 초기 크기 설정
        notificationPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            // 표시 시간이 지난 후 알림창을 사라지게 함
            DOVirtual.DelayedCall(displayDuration, () =>
            {
                notificationPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    notificationPanel.SetActive(false); // 알림창 비활성화
                });
            });
        });
    }

    public void ShowLocalizedNotification(string key)
    {
        // LocalizedString을 사용하여 Localization Table에서 메시지 가져오기
        LocalizedString localizedString = new LocalizedString
        {
            TableReference = "commentTable", // Localization Table 이름
            TableEntryReference = key // Localization Key
        };

        // 비동기적으로 지역화된 문자열을 가져옴
        localizedString.StringChanged += (localizedMessage) =>
        {
            // 가져온 메시지를 사용하여 알림을 표시함
            ShowNotification(localizedMessage);
        };
    }
}