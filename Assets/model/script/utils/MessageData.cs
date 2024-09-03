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
        // �޼����� �ʱ�ȭ�մϴ�.
        messages.Add(new Message { type = MessageType.ADD_FAIL_SCORE, english = "Failed to add your score.", korean = "���� ����� �����߽��ϴ�." });
        messages.Add(new Message { type = MessageType.GET_FAIL_SCORE, english = "Failed to retrieve the score.", korean = "������ �������� ���߽��ϴ�." });
        messages.Add(new Message { type = MessageType.ADD_SUCCESS_SCORE, english = "Score added successfully.", korean = "������ ���������� ��ϵǾ����ϴ�." });
        messages.Add(new Message { type = MessageType.EXIT_CONTENT, english = "Exiting content.", korean = "�������� �����մϴ�." });
        messages.Add(new Message { type = MessageType.WELCOME, english = "Welcome!", korean = "ȯ���մϴ�!" });
        messages.Add(new Message { type = MessageType.LOGIN_FAIL, english = "Login failed.", korean = "�α��ο� �����߽��ϴ�." });
        messages.Add(new Message { type = MessageType.LOAD_FAIL_GOOGLE_LEADERBOARD, english = "Failed to load Google leaderboard.", korean = "Google �������带 �ҷ����� �� �����߽��ϴ�." });
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
        return "�޼����� ã�� �� �����ϴ�.";
    }
}
