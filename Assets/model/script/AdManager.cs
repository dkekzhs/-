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
            //초기화 완료
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
    /// 전면 광고를 로드합니다.
    /// </summary>
    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("전면 광고 로드 중...");

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("전면 광고 로드 실패: " + error);
                    return;
                }

                Debug.Log("전면 광고 로드 완료: " + ad.GetResponseInfo());
                _interstitialAd = ad;

                RegisterEventHandlers(ad);
            });
    }

    /// <summary>
    /// 전면 광고를 표시합니다.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("전면 광고 표시 중...");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("전면 광고가 준비되지 않았습니다.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //보상 주기

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
            MoveToNextScene(nextSense); // 광고가 닫힌 후 씬 이동
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    // Play 버튼 클릭 시 호출되는 메서드
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

    // Retry 버튼 클릭 시 호출되는 메서드
    public void OnRetryButtonClicked(int nextSense)
    {
        this.nextSense = nextSense;
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            ShowInterstitialAd();
        }
        else
        {
            Debug.Log("전면 광고가 아직 준비되지 않았습니다. 다시 시도 중...");
            LoadInterstitialAd(); // 광고가 준비되지 않았다면 다시 로드
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
        SceneManager.LoadScene(nextSen); // 실제 씬 이름으로 변경
    }
}
