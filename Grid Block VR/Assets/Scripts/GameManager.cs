using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Der GameManager regelt, ob easyMode oder hardMode ist und das Ladem der Szenen.
// Er hilft auch bei anderen Dingen: dem Messen der Höhe des Turms, der Einleitung der zweiten Phase, dem Behalten der Leben und des Scores.
public class GameManager : MonoBehaviour
{
    public bool phaseTwo = false;

    //Lives ist die Anzahl der Hexagone!
    public static int lives = 5;
    // Der GameManager hat ein eigenen Timer, der vom BlockController für das Steigen der Geschwindigkeit der Blöcke genutzt wird!
    public float currentTime;
    public BlockSpawnController bsController;
    public TimeController timeController;
    public int score;  //ist die höchste gemessene Höhe im jetzigen Frame
    public int gameMode; // 1 = easy 2 = hard
    bool onlyOnce = true;
    BlockSpawnController [] hexagons;
    
    // Alle Messlatten im Spiel müssen geladen werden.
    public GameObject heightMeasure1, heightMeasure2, heightMeasure3, heightMeasure4, heightMeasure5, heightMeasure6, heightMeasure7, heightMeasure8, heightMeasure9, heightMeasure10
    , heightMeasure11, heightMeasure12;
    HeightMeasureScript heightMeasureScript1, heightMeasureScript2, heightMeasureScript3, heightMeasureScript4, heightMeasureScript5, heightMeasureScript6, heightMeasureScript7,
    heightMeasureScript8, heightMeasureScript9, heightMeasureScript10, heightMeasureScript11, heightMeasureScript12;

    // Start is called before the first frame update
    void Start()
    {
        // Time ist bei 1 damit sie am Anfang die Blöcke nicht verlangsamt
        currentTime = 1;
        timeController = FindObjectOfType<TimeController>();
        //heightMeasureScript1 = heightMeasure1.GetComponent<HeightMeasureScript>();
        //heightMeasureScript2 = heightMeasure2.GetComponent<HeightMeasureScript>();
        //heightMeasureScript3 = heightMeasure3.GetComponent<HeightMeasureScript>();
        //heightMeasureScript4 = heightMeasure4.GetComponent<HeightMeasureScript>();
        //heightMeasureScript5 = heightMeasure5.GetComponent<HeightMeasureScript>();
        //heightMeasureScript6 = heightMeasure6.GetComponent<HeightMeasureScript>();
        //heightMeasureScript7 = heightMeasure7.GetComponent<HeightMeasureScript>();
        //heightMeasureScript8 = heightMeasure8.GetComponent<HeightMeasureScript>();
        //heightMeasureScript9 = heightMeasure9.GetComponent<HeightMeasureScript>();
        //heightMeasureScript10 = heightMeasure10.GetComponent<HeightMeasureScript>();
        //heightMeasureScript11 = heightMeasure11.GetComponent<HeightMeasureScript>();
        //heightMeasureScript12 = heightMeasure12.GetComponent<HeightMeasureScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Time steigt damit auch die Geschwindigkeit der Blöcke im BlockController steigt. Damit das nicht zu schnell ist, wird es durch eine Zahl geteilt
        currentTime += Time.deltaTime/16;
        // Wenn alle Hexagone zerstört sind, soll die zweite Phase sofort beginnen. Dies soll natürlich nur einmal ausgeführt werden.
        if (onlyOnce)
        {
            if (lives <= 0)
            {
                timeController.PhaseTwo();
                onlyOnce = false;
            }
        }

        // Locksystem zum Fesstellen der Höhe des Turms: Die zweite Messlatte sendet seine height nur an den GameManager, wenn sein open-Attribut true ist.
        // Dafür muss aber einerseits zuerst die erste und dann zweite Messlatte berührt werden. Diese Logik geht mit den folgenden Messlatten so weiter.
        // Dadurch öffnen sich die Messlatten nacheinander beim Bauen des Turms und der Spieler kann nicht schummeln durch Hochwerfen der Blöcke, 
        // wo dann nämlich die Messlatten die Blöcke nicht messen, weil deren open-Attribut false bleibt!
        //if (heightMeasureScript1.touchingBlock == true)
        //{
        //    heightMeasureScript1.open = true;
        //    if (heightMeasureScript2.touchingBlock == true )
        //    {
        //        heightMeasureScript2.open = true;
        //        if (heightMeasureScript3.touchingBlock == true)
        //        {
        //            heightMeasureScript3.open = true;
        //            if (heightMeasureScript4.touchingBlock == true)
        //            {
        //                heightMeasureScript4.open = true;
        //                if (heightMeasureScript5.touchingBlock == true)
        //                {
        //                    heightMeasureScript5.open = true;
        //                    if (heightMeasureScript6.touchingBlock == true)
        //                    {
        //                        heightMeasureScript6.open = true;
        //                        if (heightMeasureScript7.touchingBlock == true)
        //                        {
        //                            heightMeasureScript7.open = true;
        //                            if (heightMeasureScript8.touchingBlock == true)
        //                            {
        //                                heightMeasureScript8.open = true;
        //                                if (heightMeasureScript9.touchingBlock == true)
        //                                {
        //                                    heightMeasureScript9.open = true;
        //                                    if (heightMeasureScript10.touchingBlock == true)
        //                                    {
        //                                        heightMeasureScript10.open = true;
        //                                        if (heightMeasureScript11.touchingBlock == true)
        //                                        {
        //                                            heightMeasureScript11.open = true;
        //                                            if (heightMeasureScript12.touchingBlock == true)
        //                                            {
        //                                                heightMeasureScript12.open = true;
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }
    // lädt die Szene mit dem übergenen Namen
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartPhaseTwo()
    {
        bsController.phaseTwo = true;
        bsController.StartPhaseTwo();
        phaseTwo = true;
    }
    // Auswerten der Messlatten: Die Messlatte mit der höchsten height wird score übergeben.
    public void saveMeasurement(int height)
    {
        if (height > score)
        {
            score = height;
        }

    }



}
