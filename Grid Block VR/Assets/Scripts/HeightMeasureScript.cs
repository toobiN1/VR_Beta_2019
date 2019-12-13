using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMeasureScript : MonoBehaviour
{
    // Der Wert von height wird in Unity für die Messlatten individuell festgelegt.
    public int height;
  
    GameManager gameManager;
    BoundsDebug bounds;

    // open wird vom GameManager genutzt, um zu bestimmen, ob eine Messlatte seine height schicken darf. (siehe GameManager.cs)
    public bool open = false;

    // Start is called before the first frame update
    void Start()
    {
       
        gameManager = FindObjectOfType<GameManager>();
        bounds = GetComponent<BoundsDebug>();
    }

    //// Bei Berührung mit den Blocks sendet die Messlatte seine Höhe an den GameMangager unter der Bedingung, dass sein Attribut open true ist.
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Block"))
    //    {
    //        touchingBlock = true;
    //        if (open)
    //        {
    //            gameManager.saveMeasurement(height);
    //        }
    //    }

    //}
    //// Beim Verlassen schließen sich die Schlösser, sodass deren Höhe nicht gemessen wird.
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Block"))
    //    {
    //        touchingBlock = false;
    //        open = false;
    //    }
    //}

    private void Update()
    {
        //Debug.Log(bounds.GetMaxBounds(gameObject).size.y);

        height = (int)((bounds.GetMaxBounds(gameObject).size.y - 1)* 10);
        Debug.Log(height);
        gameManager.saveMeasurement(height);

    }

    //heightToPoints(float height)
    //{
    //    return points
    //}
}
