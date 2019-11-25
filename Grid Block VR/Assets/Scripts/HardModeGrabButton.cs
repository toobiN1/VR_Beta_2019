using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Der Spieler soll das Schriftelement greifen und zum Button mit dem vorgegebenen Tag führen, damit die Szene geladen wird.
public class HardModeGrabButton : MonoBehaviour
{


    Hand hand;
    public GameObject exitText;
    public GameObject easyModeText;

    // Die zu ladende Szene wird in Unity festgelegt.
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        hand = FindObjectOfType<Valve.VR.InteractionSystem.Hand>();
    }

    // Beim Loslassen soll das Objekt wieder auf seine Standardposition!
    public void OnDetached()
    {
        transform.position = new Vector3(0, 1.604f, 1.074f);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    // Beim Treffen auf dem Button mit dem vorgegeben Tag, wird die Hand detached vom Objekt und das Objekt zerstört, damit das Objekt nicht in die nächste Szene gelangt!
    // Objekt muss detached von der Hand sein bevor es zerstört wird, sonst jammert SteamVR in der Console herum!
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HardModeButton")
        {

            hand.DetachObject(gameObject);
            FindObjectOfType<GameManager>().LoadLevel(level);
            Destroy(gameObject);
            Destroy(exitText);
            Destroy(easyModeText);
        }
    }

}
