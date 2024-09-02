using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    void Start()
    {

        // ����� ����
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("����� ���� ����");
            }
            else
            {
                Debug.LogWarning("����� ���� ����");
            }
        });
    }

    // �������忡 ���� �Խ�
    public void ReportScore(long score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_ranking, (bool success) => {
            if (success)
            {
                Debug.Log("���� �Խ� ����");
            }
            else
            {
                Debug.LogWarning("���� �Խ� ����");
            }
        });
    }

    // �������� UI ǥ��
    public void ShowLeaderboard()
    {

        Debug.Log("????????????");
        Social.ShowLeaderboardUI();
    }

    // Ư�� �������� UI ǥ��
    public void ShowSpecificLeaderboard()
    {
        Debug.Log("????????????");
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_ranking);
    }
}
