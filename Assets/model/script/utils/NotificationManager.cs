using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;


public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance; // Singleton �ν��Ͻ�
    public GameObject notificationPanel; // �˸�â �г�
    public TextMeshProUGUI notificationText; // �˸� �޽��� �ؽ�Ʈ
    public float displayDuration = 1.5f; // �˸�â ǥ�� �ð�

    private void Awake()
    {
        // Singleton ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� GameObject�� �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ı�
        }
    }

    private void Start()
    {
        notificationPanel.SetActive(false); // ���� �� �˸�â ��Ȱ��ȭ
    }

    public void ShowNotification(string message)
    {
        notificationText.text = message; // �޽��� ����
        notificationPanel.SetActive(true); // �˸�â Ȱ��ȭ

        // �˸�â�� ȭ�� �߾ӿ��� ���� ��Ÿ���� ��
        notificationPanel.transform.localScale = Vector3.zero; // �ʱ� ũ�� ����
        notificationPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            // ǥ�� �ð��� ���� �� �˸�â�� ������� ��
            DOVirtual.DelayedCall(displayDuration, () =>
            {
                notificationPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    notificationPanel.SetActive(false); // �˸�â ��Ȱ��ȭ
                });
            });
        });
    }

    public void ShowLocalizedNotification(string key)
    {
        // LocalizedString�� ����Ͽ� Localization Table���� �޽��� ��������
        LocalizedString localizedString = new LocalizedString
        {
            TableReference = "commentTable", // Localization Table �̸�
            TableEntryReference = key // Localization Key
        };

        // �񵿱������� ����ȭ�� ���ڿ��� ������
        localizedString.StringChanged += (localizedMessage) =>
        {
            // ������ �޽����� ����Ͽ� �˸��� ǥ����
            ShowNotification(localizedMessage);
        };
    }
}