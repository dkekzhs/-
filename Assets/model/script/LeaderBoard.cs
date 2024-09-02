using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    void Start()
    {

        // 사용자 인증
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("사용자 인증 성공");
            }
            else
            {
                Debug.LogWarning("사용자 인증 실패");
            }
        });
    }

    // 리더보드에 점수 게시
    public void ReportScore(long score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_ranking, (bool success) => {
            if (success)
            {
                Debug.Log("점수 게시 성공");
            }
            else
            {
                Debug.LogWarning("점수 게시 실패");
            }
        });
    }

    // 리더보드 UI 표시
    public void ShowLeaderboard()
    {

        Debug.Log("????????????");
        Social.ShowLeaderboardUI();
    }

    // 특정 리더보드 UI 표시
    public void ShowSpecificLeaderboard()
    {
        Debug.Log("????????????");
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_ranking);
    }
}
