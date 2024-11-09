using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{

    private string _adUnitId;
    private InterstitialAd _interstitialAd;

    private AdManager instance;
    private int nextSense;

    //ca-app-pub-2900982499007975/3986401600
    //test : ca-app-pub-3940256099942544/1033173712
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //�ʱ�ȭ �Ϸ�
        });

#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-2900982499007975/2477846990";
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

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //���� �ֱ�

            Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            MoveToNextScene(nextSense); // ���� ���� �� �� �̵�
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    // Play ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnPlayButtonClicked(int nextSense)
    {
        int result = Random.Range(0, 10);
        this.nextSense = nextSense;

        if (result < 3)
        {
            ShowInterstitialAd();
        }
        else {
            ScreenManager sc = gameObject.AddComponent<ScreenManager>();
            sc.moveScene(nextSense);
        }

    }

    // Retry ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnRetryButtonClicked(int nextSense)
    {
        this.nextSense = nextSense;
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
    private void MoveToNextScene(int nextSen)
    {
        SceneManager.LoadScene(nextSen); // ���� �� �̸����� ����
    }
}
