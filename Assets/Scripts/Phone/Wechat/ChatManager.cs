using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI contactPersonNameText;

    public GameObject chatObject;
    public GameObject ownChatObject;

    public GameObject replyPanel;

    public void GenerateChat(List<Chat> ChatMessageList, ContactPersonBehavior contactPersonBehavior, Person person)
    {

    }
}


[System.Serializable]
public class Chat
{
    public string imageLocation;

    public string chatMessage;

    public bool isMyOwnChat;

    public Chat(string _imageLocation, string _chatMessage, bool _isMyOwnChat)
    {
        this.imageLocation = _imageLocation;
        this.chatMessage = _chatMessage;
        this.isMyOwnChat = _isMyOwnChat;
    }
}
