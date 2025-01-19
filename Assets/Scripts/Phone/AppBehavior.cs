using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppBehavior : MonoBehaviour
{
    public int index;
    public BubbleBehavior bubble;

    public bool isWechat;

    public void getMessage()
    {
        if (isWechat)
        {
            bubble.gameObject.SetActive(true);
        }
        else
        {
            bubble.apear();
        }
    }

    
}
