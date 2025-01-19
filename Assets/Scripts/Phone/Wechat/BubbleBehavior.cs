using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBehavior : MonoBehaviour
{
    private int clickCount = 0; // Tracks the number of clicks
    private Vector3 originalScale; // Stores the original scale of the UI Image
    public float shakeDuration = 0.2f; // Duration of the shake effect
    public float shakeScaleFactor = 1.2f; // Factor by which the scale increases during the shake
    public float hoverScaleFactor = 1.1f; // Factor by which the scale increases on hover

    public TextMeshProUGUI numberText;
    private int bubbleNumber;

    public bool isSmall;

    void Start()
    {
        // Save the original scale of the image
        originalScale = transform.localScale;

        // Ensure the object has an Image component
        if (GetComponent<Image>() == null)
        {
            Debug.LogError("This script should be attached to a UI element with an Image component.");
        }
    }

    public void OnImageClick()
    {
        // Increment the click count
        clickCount++;

        // Start the shake effect
        StartCoroutine(ShakeEffect());

        // Check if the click count reaches 3
        if (clickCount >= 3)
        {
            Disapear();
        }
    }

    public void apear()
    {
        // Make the UI Image disappear
        gameObject.SetActive(true);
        clickCount = 0;
        bubbleNumber += 1;
        numberText.text = bubbleNumber + "";
        if (bubbleNumber >= 2)
        {
            numberText.gameObject.SetActive(true);
        }
        else
        {
            numberText.gameObject.SetActive(false);
        }    
    }

    public void Disapear()
    {
        Disapear(true);
    }

    public void Disapear(bool effect)
    {
        // Make the UI Image disappear
        if (this.gameObject.activeSelf)
        {
            clickCount = 0;
            bubbleNumber -= 1;
            numberText.text = bubbleNumber + "";
            if (bubbleNumber == 0)
            {
                // 泡泡破裂
                gameObject.SetActive(false);
                if (effect)
                {
                    if (isSmall)
                    {
                        EffectManager.Instance.PlayEffect("SmallBubblePop", this.transform.position);
                    }
                    else
                    {
                        EffectManager.Instance.PlayEffect("BubblePop", this.transform.position);
                    }
                }
                // 减少压力
                StressManager.Instance.DecreaseStress(5);
            }
            else
            {
                if (effect)
                {
                    if (isSmall)
                    {
                        EffectManager.Instance.PlayEffect("SmallBubblePop", this.transform.position);
                    }
                    else
                    {
                        EffectManager.Instance.PlayEffect("BubblePop", this.transform.position);
                    }
                }
                // 减少压力
                StressManager.Instance.DecreaseStress(5);
                if (bubbleNumber <= 1)
                {
                    numberText.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator ShakeEffect()
    {
        // Increase the size
        transform.localScale = originalScale * shakeScaleFactor;

        // Wait for half the shake duration
        yield return new WaitForSeconds(shakeDuration / 2);

        // Return to the original size
        transform.localScale = originalScale;

        // Wait for the remaining duration
        yield return new WaitForSeconds(shakeDuration / 2);
    }

    public void OnMouseEnter()
    {
        // Increase the size slightly on hover
        transform.localScale = originalScale * hoverScaleFactor;
    }

    public void OnMouseExit()
    {
        // Return to the original size when the mouse leaves
        transform.localScale = originalScale;
    }
}
