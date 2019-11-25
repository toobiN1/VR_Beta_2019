using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Dieses Skript regelt den Timer der ersten Phase
public class TimeController : MonoBehaviour
{
    float currentTime = 0f;
    // Die startTime wird in Unity festgelegt!
    public float startTime;

    public TextMesh countDown;

    bool timeRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        // Wenn die zweite Phase beginnt, läuft die Zeit nicht mehr ab und es wird ein Text angezeigt.
        if (timeRunning == true)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        if (currentTime <= 0 || !timeRunning)
        {
            PhaseTwo();
        }
        else
        {   
            // Zeit wird angezeigt in TextMesh von countDown
            countDown.text = currentTime.ToString("0");
        }

    }

    public void PhaseTwo()
    {
        timeRunning = false;
        countDown.text = "PhaseTwo started!";
        FindObjectOfType<GameManager>().StartPhaseTwo();
    }



}
