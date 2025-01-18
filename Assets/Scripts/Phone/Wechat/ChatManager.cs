using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI contactPersonNameText;

    public GameObject chatObject;
    public GameObject ownChatObject;
    public Transform chatParent;

    public GameObject replyPanel;

    public void GenerateChat(List<Chat> ChatMessageList, ContactPersonBehavior contactPersonBehavior, Person person)
    {
        foreach (var Chat in ChatMessageList)
        {
            GameObject newChat = null;
            if (Chat.isMyOwnChat)
            {
                newChat = Instantiate(ownChatObject, chatParent);
            }
            else
            {
                newChat = Instantiate(chatObject, chatParent);
            }
            if (newChat == null)
            {
                Debug.Log("error, chat is null");
            }
            ChatBehavior newChatBehavior = newChat.GetComponent<ChatBehavior>();

            if (newChatBehavior == null)
            {
                Debug.Log("error, chat behavior is null");
            }

            newChatBehavior.SetupText(Chat.imageLocation, Chat.chatMessage);
        }

        contactPersonNameText.text = person.name;

        GenerateReplyPanel(person);

    }

    private void GenerateReplyPanel(Person person)
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
