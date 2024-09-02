using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioEffectManager : MonoBehaviour
{
    public GameObject[] backgroundAudioEffects;
    public GameObject[] soundEffects;
    public AudioSource[] sounds;
    public Button muteButton; // 음소거 버튼
    public Sprite soundOnSprite; // 소리가 켜져 있을 때의 버튼 이미지
    public Sprite soundOffSprite; // 소리가 꺼져 있을 때의 버튼 이미지

    #region Singleton
    public static AudioEffectManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        // 씬이 로드될 때마다 오디오 소스 배열을 초기화
        RefreshAudioSources();

        // 저장된 음소거 상태를 확인하고 반영
        soundCheck();

        // 씬이 로드될 때 호출될 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        // 씬 전환이 완료되면 오디오 소스를 갱신
        RefreshAudioSources();
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 오디오 소스 갱신
        RefreshAudioSources();
    }



    public void AllStopSound()
    {
        int soundBoolean = PlayerPrefs.GetInt("sound", 0);

        if (soundBoolean != 0) 
        {
            // 모든 오디오 소스를 음소거
            foreach (AudioSource audio in sounds)
            {
                audio.mute = true; // 음소거 활성화
            }

            // 음소거 버튼의 이미지를 "소리 꺼짐" 이미지로 변경
            muteButton.image.sprite = soundOffSprite;

            // 음소거 상태를 저장
            PlayerPrefs.SetInt("sound", 0);
        }
        else // soundBoolean이 0이면 소리 꺼짐 상태
        {
            // 모든 오디오 소스의 음소거 해제
            foreach (AudioSource audio in sounds)
            {
                audio.mute = false; // 음소거 해제
            }

            // 음소거 버튼의 이미지를 "소리 켜짐" 이미지로 변경
            muteButton.image.sprite = soundOnSprite;

            // 음소거 해제 상태를 저장
            PlayerPrefs.SetInt("sound", 1);
        }

        // PlayerPrefs를 즉시 저장
        PlayerPrefs.Save();
    }
    private void RefreshAudioSources()
    {
        // 현재 씬에서 모든 AudioSource 컴포넌트를 가져와 sounds 배열을 갱신
        sounds = FindObjectsOfType<AudioSource>();
        soundCheck(); // 현재 상태에 따라 음소거 여부를 설정
    }
    public void soundCheck()
    {
        int sound = PlayerPrefs.GetInt("sound",0);
        if (sound == 1)
        {
            muteButton.image.sprite = soundOnSprite;
            // 모든 오디오 소스의 음소거 해제
            foreach (AudioSource audio in sounds)
            {
                audio.mute = false;
            }
        }
        else
        {
            muteButton.image.sprite = soundOffSprite;
            // 모든 오디오 소스의 음소거 해제
            foreach (AudioSource audio in sounds)
            {
                audio.mute = true;
            }
        }
    }



    //효과음 오디오 재생
    public void AudioClipPlay(int audioIndex)
    {
        soundEffects[audioIndex].GetComponent<AudioSource>().Play();
    }

    //효과음 오디오 재생 중지
    public void AudioClipStop(int audioIndex)
    {
        soundEffects[audioIndex].GetComponent<AudioSource>().Stop();
    }

    //배경음 오디오 재생
    public void BackgroundAudioClipStart(int audioIndex)
    {
        backgroundAudioEffects[audioIndex].GetComponent<AudioSource>().Play();
    }


    //배경음 오디오 재생 중지
    public void BackgroundAudioClipStop(int audioIndex)
    {
        backgroundAudioEffects[audioIndex].GetComponent<AudioSource>().Stop();
    }
}
