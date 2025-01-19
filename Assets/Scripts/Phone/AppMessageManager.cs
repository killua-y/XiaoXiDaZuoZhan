using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppMessageManager : MonoBehaviour
{
    public GameObject MessageBoard;

    public TextMeshProUGUI MessageText;

    public Image AppIcon;

    private float openPosition = -10;
    private float closePosition = 80;
    public float moveTime = 0.3f;

    private Coroutine currentCoroutine;

    public List<AppBehavior> appList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GenerateNewMessage(AppMessage appMessage)
    {
        AppIcon.sprite = Resources.Load<Sprite>(appMessage.imageLocation);
        MessageText.text = appMessage.message;
        OpenMessageBoard();

        NotifyApp(appMessage);
    }

    public void NotifyApp(AppMessage appMessage)
    {
        foreach (AppBehavior app in appList)
        {
            if(app.index == appMessage.index)
            {
                app.getMessage();
            }
        }
    }


    public void OpenMessageBoard()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothMove(closePosition, openPosition, moveTime));
    }

    public void CloseMessageBoard()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SmoothMove(openPosition, closePosition, moveTime));
    }

    private IEnumerator SmoothMove(float startPos, float endPos, float duration)
    {
        RectTransform rectTransform = MessageBoard.GetComponent<RectTransform>();
        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 endPosition = new Vector3(startPosition.x, endPos, startPosition.z);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // Ensure final position is accurate
        rectTransform.anchoredPosition = endPosition;
    }
}


[System.Serializable]
public class AppMessage
{
    public int index;

    public string imageLocation;

    public string message;

    public AppMessage(int _index, string _imageLocation, string _message)
    {
        index = _index;
        this.imageLocation = _imageLocation;
        this.message = _message;
    }
}