using GooglePlayGames;
using UnityEngine;
using GooglePlayGames.BasicApi;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager instance;
    private int currentBestScore = 0;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 이미 존재하면 중복 생성을 방지
        }
    }
    public void Start()
    {

    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string userName = PlayGamesPlatform.Instance.GetUserDisplayName();
            string userId = PlayGamesPlatform.Instance.GetUserId();

            NotificationManager.Instance.ShowLocalizedNotification("WELCOME");

        }
        else
        {
            NotificationManager.Instance.ShowLocalizedNotification("LOGIN_FAIL");
        }
    }






    public void ShowSpecificLeaderboard()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_ranking);
    }

    void LoadUserBestScore()
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_ranking, // 리더보드 ID
            LeaderboardStart.PlayerCentered, // 현재 플레이어를 중심으로 시작
            1, // 1명의 점수만 가져옴 (자신의 점수)
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if (data.Valid)
                {
                    // 현재 사용자의 최고 점수 설정
                    currentBestScore = (int)data.PlayerScore.value;
                    Debug.Log("현재 최고 점수: " + currentBestScore);
                }
                else
                {
                    Debug.Log("점수를 불러오는 데 실패했습니다.");
                }
            });
    }

    public void SubmitScore(int finalScore)
    {
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            ProcessAuthenticationSumbitScore(status, finalScore);
        });




    }

    internal void ProcessAuthenticationSumbitScore(SignInStatus status, int finalScore)
    {
        if (status == SignInStatus.Success)
        {
            LoadUserBestScore();

            // 현재 최고 점수보다 클 때만 제출
            if (finalScore > currentBestScore)
            {
                PlayGamesPlatform.Instance.ReportScore(finalScore, GPGSIds.leaderboard_ranking, (bool success) =>
                {
                    if (success)
                    {
                        NotificationManager.Instance.ShowLocalizedNotification("ADD_SUCCESS_SCORE");
                        currentBestScore = finalScore; // 최고 점수 갱신
                    }
                    else
                    {
                        NotificationManager.Instance.ShowLocalizedNotification("ADD_FAIL_SCORE");
                    }
                });
            }
            else
            {
                //기존 등록 안함
            }
        }
        else
        {
            NotificationManager.Instance.ShowLocalizedNotification("LOAD_FAIL_GOOGLE_LEADERBOARD");
        }
    }

}