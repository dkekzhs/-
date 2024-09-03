using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private string _adUnitId;
    private InterstitialAd _interstitialAd;

    void Start()
    {
        // �÷����� ���� ���� ���� ID ����
#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        _adUnitId = "unused";
#endif

        LoadInterstitialAd();
    }

    /// <summary>
    /// ���� ���� �ε��մϴ�.
    /// </summary>
    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("���� ���� �ε� ��...");

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("���� ���� �ε� ����: " + error);
                    return;
                }

                Debug.Log("���� ���� �ε� �Ϸ�: " + ad.GetResponseInfo());
                _interstitialAd = ad;

                RegisterEventHandlers(ad);
            });
    }

    /// <summary>
    /// ���� ���� ǥ���մϴ�.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("���� ���� ǥ�� ��...");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("���� ���� �غ���� �ʾҽ��ϴ�.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("���� ���� �������� �������ϴ�.");
            LoadInterstitialAd(); // ���� ������ ���ο� ���� �ε�
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("���� ���� ������ ���� ����: " + error);
            LoadInterstitialAd(); // ���� ���� ���� �� ���ο� ���� �ε�
        };
    }

    // Play ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnPlayButtonClicked()
    {
        ShowInterstitialAd();
    }

    // Retry ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnRetryButtonClicked()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            ShowInterstitialAd();
        }
        else
        {
            Debug.Log("���� ���� ���� �غ���� �ʾҽ��ϴ�. �ٽ� �õ� ��...");
            LoadInterstitialAd(); // ���� �غ���� �ʾҴٸ� �ٽ� �ε�
        }
    }

    private void OnDestroy()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
        }
    }
}
