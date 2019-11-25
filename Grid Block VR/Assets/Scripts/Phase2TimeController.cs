using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dieses Skript regelt den Timer der zweiten Phase
public class Phase2TimeController : MonoBehaviour
{


    float currentTime = 0f;
    // Die startTime wird in Unity festgelegt!
    public float startTime;
    public TextMesh countDown;
    GameManager gameManager;
    HeightDisplayScript heightDisplayScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Der Phase 2 Countdowntext soll anfangs nicht zu sehen sein, also bleibt er leer
        countDown.text = "";
        currentTime = startTime;
        heightDisplayScript = FindObjectOfType<HeightDisplayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Wenn Phase 2 beginnt, läuft die Zeit ab und wird im TextMesh von countDown angezeigt. 
        // Wenn die Zeit 0 erreicht, ist das Spiel zu Ende und durch Änderung vom bool running wird bei der Anzeige der Höhe des Turms der Endwert weiterhin angezeigt.
        if (gameManager.phaseTwo)
        {
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                heightDisplayScript.running = false;
                countDown.text = "Game has ended!";
            } else{
                countDown.text = currentTime.ToString("0");
            }








        }


    }
}
