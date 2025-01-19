using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StressManager : Singleton<StressManager>
{
    public TextMeshProUGUI stressNumberText;
    public GameObject stressMask;
    private float maxPosition = 5.7f;
    private float minPosition = 1.8f;

    public int currentStressNumber;

    public void GameStart()
    {
        currentStressNumber = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateStress()
    {
        // Clamp the stress number to ensure it's between 0 and 100
        currentStressNumber = Mathf.Clamp(currentStressNumber, 0, 100);

        // Update the stress number text
        stressNumberText.text = "压力值: " + currentStressNumber + "%";

        // Calculate the new position of the stress mask based on the stress number
        float normalizedStress = currentStressNumber / 100f; // Normalize stress to a range of 0-1
        float newYPosition = Mathf.Lerp(minPosition, maxPosition, normalizedStress);

        // Update the position of the stress mask
        Vector3 currentPosition = stressMask.transform.localPosition;
        currentPosition.y = newYPosition;
        stressMask.transform.localPosition = currentPosition;
    }

    public void IncreaseStress(int amount)
    {
        currentStressNumber += amount;
        if (currentStressNumber > 100)
        {
            GameManager.Instance.GameOver();
        }
        UpdateStress();
    }


    public void DecreaseStress(int amount)
    {
        currentStressNumber -= amount;
        if (currentStressNumber < 0)
        {
            currentStressNumber = 0;
        }

        UpdateStress();
    }
}
