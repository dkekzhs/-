using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private string _adUnitId;
    private InterstitialAd _interstitialAd;

    void Start()
    {
        // 플랫폼에 따라 광고 단위 ID 설정
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

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("전면 광고 콘텐츠가 닫혔습니다.");
            LoadInterstitialAd(); // 광고가 닫히면 새로운 광고 로드
        };

        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("전면 광고 콘텐츠 열기 실패: " + error);
            LoadInterstitialAd(); // 광고 열기 실패 시 새로운 광고 로드
        };
    }

    // Play 버튼 클릭 시 호출되는 메서드
    public void OnPlayButtonClicked()
    {
        ShowInterstitialAd();
    }

    // Retry 버튼 클릭 시 호출되는 메서드
    public void OnRetryButtonClicked()
    {
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
}
