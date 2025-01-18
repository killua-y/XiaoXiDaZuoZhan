using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContactPersonBehavior : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the child moves
    public float targetPosition = 0f; // Target X position in the middle
    public float disappearTarget = 1000f; // X position where the child will be destroyed

    public RectTransform childRectTransform; // RectTransform of the child UI GameObject

    public Person person;

    public List<Chat> ChatList = new List<Chat>();

    public Image profilePhoto;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messagePreviewText;

    public ChatManager chatManager;

    private void Awake()
    {
        chatManager = FindAnyObjectByType<ChatManager>();
    }

    void Start()
    {
        // Ensure the parent has at least one child
        if (transform.childCount == 0)
        {
            Debug.LogError("No child found under this GameObject.");
            return;
        }

        // Get the RectTransform of the first child
        //childRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

        // Ensure the child has a RectTransform
        if (childRectTransform == null)
        {
            Debug.LogError("The child does not have a RectTransform component.");
            return;
        }

        // Set the initial position of the child to the left of the screen, equal to its width
        childRectTransform.anchoredPosition = new Vector2(-childRectTransform.rect.width, childRectTransform.anchoredPosition.y);

        // Start moving the child to the middle
        StartCoroutine(MoveToPosition(childRectTransform, new Vector2(targetPosition, childRectTransform.anchoredPosition.y)));

    }

    public void SetupImageAndText(Person _person)
    {
        person = _person;
        Chat newChat = new Chat(person.imageLocation, person.chatMessage, false);
        ChatList.Add(newChat);

        profilePhoto.sprite = Resources.Load<Sprite>(person.imageLocation);

        nameText.text = person.name;
        messagePreviewText.text = person.chatMessage;
    }

    public void GoToChatPanel()
    {
        PhoneBehavior.Instance.OpenChatBackground();
        chatManager.GenerateChat(ChatList, this, person);
    }


    private IEnumerator MoveToPosition(RectTransform rectTransform, Vector2 destination)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, destination) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                destination,
                moveSpeed * Time.deltaTime
            );
            yield return null; // Wait until the next frame
        }

        // Ensure the position is exactly at the destination
        rectTransform.anchoredPosition = destination;
    }

    public void Disappear()
    {
        // Start the disappearing coroutine for the child
        StartCoroutine(MoveAndDestroy(childRectTransform, new Vector2(disappearTarget, childRectTransform.anchoredPosition.y)));
    }

    private IEnumerator MoveAndDestroy(RectTransform rectTransform, Vector2 destination)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, destination) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                destination,
                moveSpeed * Time.deltaTime
            );
            yield return null; // Wait until the next frame
        }

        // Ensure the position is exactly at the destination
        rectTransform.anchoredPosition = destination;

        // Destroy the child GameObject
        Destroy(rectTransform.gameObject);
    }
}
