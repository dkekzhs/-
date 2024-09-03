using System.Collections.Generic;
using UnityEngine;

public class MessageData : MonoBehaviour
{
    public enum MessageType
    {
        ADD_FAIL_SCORE,
        GET_FAIL_SCORE,
        ADD_SUCCESS_SCORE,
        EXIT_CONTENT,
        WELCOME,
        LOGIN_FAIL,
        LOAD_FAIL_GOOGLE_LEADERBOARD,


    }

    [System.Serializable]
    public class Message
    {
        public MessageType type;
        public string english;
        public string korean;
    }

    public List<Message> messages = new List<Message>();

    private void Awake()
    {
        // 메세지를 초기화합니다.
        messages.Add(new Message { type = MessageType.ADD_FAIL_SCORE, english = "Failed to add your score.", korean = "점수 등록을 실패했습니다." });
        messages.Add(new Message { type = MessageType.GET_FAIL_SCORE, english = "Failed to retrieve the score.", korean = "점수를 가져오지 못했습니다." });
        messages.Add(new Message { type = MessageType.ADD_SUCCESS_SCORE, english = "Score added successfully.", korean = "점수가 성공적으로 등록되었습니다." });
        messages.Add(new Message { type = MessageType.EXIT_CONTENT, english = "Exiting content.", korean = "콘텐츠를 종료합니다." });
        messages.Add(new Message { type = MessageType.WELCOME, english = "Welcome!", korean = "환영합니다!" });
        messages.Add(new Message { type = MessageType.LOGIN_FAIL, english = "Login failed.", korean = "로그인에 실패했습니다." });
        messages.Add(new Message { type = MessageType.LOAD_FAIL_GOOGLE_LEADERBOARD, english = "Failed to load Google leaderboard.", korean = "Google 리더보드를 불러오는 데 실패했습니다." });
    }

    public string GetMessage(MessageType type, string language)
    {
        foreach (var message in messages)
        {
            if (message.type == type)
            {
                return language == "korean" ? message.korean : message.english;
            }
        }
        return "메세지를 찾을 수 없습니다.";
    }
}
