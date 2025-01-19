using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBehavior : Singleton<PhoneBehavior>
{
    public GameObject WechatContactList;
    public GameObject WechatBackground;
    public GameObject PhoneBackground;

    public GameObject OpenButton;
    public GameObject CloseButton;

    public int BubbleNumber;


    public Vector2 closedPosition = new Vector2(-1100, -700); // Initial position
    public Vector2 openPosition = Vector2.zero; // Target position
    public float closedScale = 0.3f; // Initial scale
    public float openScale = 1f; // Target scale
    public float duration = 1f; // Duration of the animation in seconds

    private RectTransform rectTransform; // RectTransform of the GameObject

    // 玩家信息
    public static string playerProfilePhoto = "123.jpg";

    void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("This script requires a RectTransform component!");
            return;
        }

        // Set the initial position and scale to "closed" state
        //rectTransform.anchoredPosition = closedPosition;
        //rectTransform.localScale = Vector3.one * closedScale;
    }

    public void OpenThePhone()
    {
        // Start the coroutine to move and scale to the open state
        StartCoroutine(MoveAndScale(openPosition, openScale));
        CloseButton.SetActive(true);
        OpenButton.SetActive(false);

        GoToPhoneBackground();
    }

    public void CloseThePhone()
    {
        // Start the coroutine to move and scale to the closed state
        StartCoroutine(MoveAndScale(closedPosition, closedScale));
        CloseButton.SetActive(false);
        OpenButton.SetActive(true);
    }

    public void GoToPhoneBackground()
    {
        WechatContactList.SetActive(false);
        WechatBackground.SetActive(false);
        PhoneBackground.SetActive(true);
    }

    public void OpenWechat()
    {
        WechatContactList.SetActive(true);
        WechatBackground.SetActive(false);
        PhoneBackground.SetActive(false);
    }

    public void OpenChatBackground()
    {
        WechatContactList.SetActive(false);
        WechatBackground.SetActive(true);
        PhoneBackground.SetActive(false);
    }

    public void AddBubble()
    {

    }

    public void DecreaseBubble()
    {

    }

    private IEnumerator MoveAndScale(Vector2 targetPosition, float targetScale)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float startScale = rectTransform.localScale.x;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate progress (0 to 1)
            float progress = elapsedTime / duration;

            // Interpolate position and scale
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, progress);
            float scale = Mathf.Lerp(startScale, targetScale, progress);
            rectTransform.localScale = Vector3.one * scale;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final values are set
        rectTransform.anchoredPosition = targetPosition;
        rectTransform.localScale = Vector3.one * targetScale;
    }
}
