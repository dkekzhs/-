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
            Destroy(gameObject); // �̹� �����ϸ� �ߺ� ������ ����
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
            GPGSIds.leaderboard_ranking, // �������� ID
            LeaderboardStart.PlayerCentered, // ���� �÷��̾ �߽����� ����
            1, // 1���� ������ ������ (�ڽ��� ����)
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (LeaderboardScoreData data) =>
            {
                if (data.Valid)
                {
                    // ���� ������� �ְ� ���� ����
                    currentBestScore = (int)data.PlayerScore.value;
                    Debug.Log("���� �ְ� ����: " + currentBestScore);
                }
                else
                {
                    Debug.Log("������ �ҷ����� �� �����߽��ϴ�.");
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

            // ���� �ְ� �������� Ŭ ���� ����
            if (finalScore > currentBestScore)
            {
                PlayGamesPlatform.Instance.ReportScore(finalScore, GPGSIds.leaderboard_ranking, (bool success) =>
                {
                    if (success)
                    {
                        NotificationManager.Instance.ShowLocalizedNotification("ADD_SUCCESS_SCORE");
                        currentBestScore = finalScore; // �ְ� ���� ����
                    }
                    else
                    {
                        NotificationManager.Instance.ShowLocalizedNotification("ADD_FAIL_SCORE");
                    }
                });
            }
            else
            {
                //���� ��� ����
            }
        }
        else
        {
            NotificationManager.Instance.ShowLocalizedNotification("LOAD_FAIL_GOOGLE_LEADERBOARD");
        }
    }

}