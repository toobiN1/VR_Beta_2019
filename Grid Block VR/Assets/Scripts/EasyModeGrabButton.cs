using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Der Spieler soll das Schriftelement greifen und zum Button mit dem vorgegebenen Tag führen, damit die Szene geladen wird.
public class EasyModeGrabButton : MonoBehaviour
{

    Hand hand;
    // Die zu ladende Szene wird in Unity festgelegt.
    public string level;
    public GameObject exitText;
    public GameObject hardModeText;
    // Start is called before the first frame update
    void Start()
    {
        hand = FindObjectOfType<Valve.VR.InteractionSystem.Hand>();
    }

    // Beim Loslassen soll das Objekt wieder auf seine Standardposition!
    public void OnDetached()
    {
        transform.position = new Vector3(-0.3f, 1.414f, 1.074f);
        transform.rotation = Quaternion.Euler(-180, 0, 180);
    }
    // Beim Treffen auf dem Button mit dem vorgegeben Tag, wird die Hand detached vom Objekt und das Objekt zerstört, damit das Objekt nicht in die nächste Szene gelangt!
    // Objekt muss detached von der Hand sein bevor es zerstört wird, sonst jammert SteamVR in der Console herum!
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EasyModeButton")
        {
            
            hand.DetachObject(gameObject);
            FindObjectOfType<GameManager>().LoadLevel(level);
            Destroy(gameObject);
            Destroy(exitText);
            Destroy(hardModeText);
        }
    }

}
