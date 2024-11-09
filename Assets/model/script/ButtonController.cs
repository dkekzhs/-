using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject exitPannel;
    private Boolean exit_flag = false;
    public TextMeshProUGUI title;
    //public static ButtonController instance;
    public Button rankingButton;



    void Start()
    {
        rankingButton.onClick.AddListener(LeaderBoardManager.instance.ShowSpecificLeaderboard);
        exitPannel.SetActive(false);
        title.text = "�����Ͻðڽ��ϱ�?";
    }
    public void GameExit()
    {
        exitPannel.SetActive(true);
    }

    public void GameExitSumbit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                    Application.Quit();
        #endif
    }

    public void GameExitCnacel()
    {
        exitPannel.SetActive(false);
    }

}
