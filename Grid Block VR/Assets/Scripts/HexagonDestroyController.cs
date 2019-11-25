using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HexagonDestroyController : MonoBehaviour
{
    // Dieses Script regelt alles bezüglich des Selbstzerstörungsmechanismus der Hexagone.

    // Minimumzeit für das Einleiten der Danger-Coroutine!
    public int DTimerMin;

    // Maximumzeit für das Einleiten der Danger-Coroutine!
    public int DTimerMax;

    // Dieser boolean ist das Todesurteil für das Objekt. Hier gibt's kein zurück mehr!
    private bool destroyReady;

    // Nach danger hat hat der Spieler eine kurze Zeit zum Deaktivieren des Objekts!
    private bool danger;

    // Das Signal der Rettung für das Objekt.
    bool untouched;
    public HexagonRenderScript hexRenderScript;
    public BlockSpawnController blocksSpnCon;
    public GameManager gm;

    void Start()
    {
        destroyReady = false;
        danger = false;
        //Beim Start beginnt die DangerRate, die nach einer zufälligen Zeit 
        StartCoroutine(DangerRate());
    }

    
    void Update()
    {
        // Wenn die erste Phase läuft und der HardMode gespielt wird.
        if (!gm.phaseTwo && gm.gameMode == 2)
        {
            if (danger)
            {
                // Bei der Gefahrphase wird das Material für die Gefahr gesetzt und die Coroutine für das Zerstören beginnt.
                danger = false;
                untouched = true;
                hexRenderScript.SetSelfDestructMat();
                StartCoroutine(DestroyTimer());
            }
            // Ende Gelände. Punkt, Aus , Ende. Rien ne va Plus.
            if (destroyReady)
            {
                DestroyHex();
            }
        }
        // Bei Phase Zwei werden die Hexagone auch noch entfernt.
        if (gm.phaseTwo)
        {
            DestroyHex();
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        //hand.GetGrabStarting gibt zurück, welche der Grifftasten gedrückt wurde.
        GrabTypes startingGrabType = hand.GetGrabStarting();

        //Wird irgendeine Grifftaste gedrückt...
        if (startingGrabType != GrabTypes.None)
        {
            // Beim Deaktivieren der Selbstzerstörung soll die Farbe natürlich auch wieder auf blau gesetzt, falls ein Block fällt. Wenn nicht, dann wird er grau!
            untouched = false;
            hexRenderScript.isSelfDestructMaterial = false;
            if (blocksSpnCon.blockFalling)
            {
                hexRenderScript.SetActiveHexagonMat();
            }
            else
            {
                hexRenderScript.SetStandardMat();
            }
            DangerRate();
        }
    }

    // DangerRate leitet nach einer zufälligen Zeit die Gefahr mit dem boolean danger ein!
    IEnumerator DangerRate()
    {
        yield return new WaitForSeconds(Random.Range(DTimerMin, DTimerMax));
        danger = true;
    }

    // Wenn danger ist, wird dieser Timer eingeleitet. Nach einer kurzen wird das Todesurteil entschieden, wenn nicht vorher untouched auf true gesetzt wird!
    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5.0f);
        if (untouched)
        {
            destroyReady = true;
        }
    }

    public void DestroyHex()
    {
        GameManager.lives--;
        Destroy(this.gameObject);
    }
}
