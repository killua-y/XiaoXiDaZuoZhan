using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI contactPersonNameText;

    public GameObject chatObject;
    public GameObject ownChatObject;
    public Transform chatParent;

    public GameObject replyPanel;
    public TextMeshProUGUI replyMessageText1;
    public TextMeshProUGUI replyMessageText2;
    public Button replyButton1;
    public Button replyButton2;

    private float height = 650;
    private float yposition = - 8.16f;

    private int newheight = 540;
    private int newyposition = 54;

    public RectTransform chatPanelRectTransform;

    private ContactPersonBehavior currentContactPersonBehavior;

    private void Start()
    {
        UpdateChatPanelRectTransform(true);
    }

    public void GenerateChat(List<Chat> ChatMessageList, ContactPersonBehavior contactPersonBehavior, Person person)
    {
        // 先清理消息
        foreach (Transform child in chatParent)
        {
            Destroy(child.gameObject);
        }

        currentContactPersonBehavior = contactPersonBehavior;
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

        if (!contactPersonBehavior.hasReplayed)
        {
            GenerateReplyPanel(person);
        }
        else
        {
            CloseReplyPanel();
        }
    }

    private void GenerateReplyPanel(Person person)
    {
        UpdateChatPanelRectTransform(false);
        replyPanel.SetActive(true);
        replyMessageText1.text = person.replyMessage1;
        replyMessageText2.text = person.replyMessage2;

        // Clear existing listeners
        // 给按钮assign function
        replyButton1.onClick.RemoveAllListeners();
        Chat newReplayChat1 = new Chat(PhoneBehavior.playerProfilePhoto, person.replyMessage1, true);
        replyButton1.onClick.AddListener(() => sendText(newReplayChat1));
        replyButton1.onClick.AddListener(() => CloseReplyPanel());

        Chat newReplayChat2 = new Chat(PhoneBehavior.playerProfilePhoto, person.replyMessage2, true);
        replyButton2.onClick.RemoveAllListeners();
        replyButton2.onClick.AddListener(() => sendText(newReplayChat2));
        replyButton2.onClick.AddListener(() => CloseReplyPanel());
    }

    private void CloseReplyPanel()
    {
        replyPanel.SetActive(false);
        UpdateChatPanelRectTransform(true);
    }

    public void sendText(Chat chat)
    {
        // 添加新的聊天框
        GameObject newChatObject = null;
        if (chat.isMyOwnChat)
        {
            newChatObject = Instantiate(ownChatObject, chatParent);
            // 如果是玩家发送的消息需要添加到联系人聊天记录中
            currentContactPersonBehavior.ChatList.Add(chat);
            // 并提供对应回复
            currentContactPersonBehavior.SendFurtherReplayMessage();
        }
        else
        {
            newChatObject = Instantiate(chatObject, chatParent);
        }
        if (newChatObject == null)
        {
            Debug.Log("error, chat is null");
        }
        ChatBehavior newChatBehavior = newChatObject.GetComponent<ChatBehavior>();

        if (newChatBehavior == null)
        {
            Debug.Log("error, chat behavior is null");
        }
        newChatBehavior.SetupText(chat.imageLocation, chat.chatMessage);
    }

    // Function to update the RectTransform
    private void UpdateRectTransform(float targetHeight, float targetYPosition)
    {
        if (chatPanelRectTransform == null)
        {
            Debug.LogError("Target RectTransform is not assigned.");
            return;
        }

        // Update the size (height) and position (Y)
        Vector2 sizeDelta = chatPanelRectTransform.sizeDelta;
        sizeDelta.y = targetHeight; // Change the height
        chatPanelRectTransform.sizeDelta = sizeDelta;

        Vector3 position = chatPanelRectTransform.localPosition;
        position.y = targetYPosition; // Change the Y position
        chatPanelRectTransform.localPosition = position;
    }

    private void UpdateChatPanelRectTransform(bool expend)
    {
        if (expend)
        {
            UpdateRectTransform(height, yposition);
        }
        else
        {
            UpdateRectTransform(newheight, newyposition);
        }
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
