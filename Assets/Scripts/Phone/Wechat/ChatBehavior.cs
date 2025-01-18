using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehavior : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public Image chatPhoto;

    public void SetupText(string imageLcation,string textString)
    {
        chatText.text = textString;
        chatPhoto.sprite = Resources.Load<Sprite>(imageLcation);
    }
}
