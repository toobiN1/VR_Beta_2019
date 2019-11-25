using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Der Spieler soll das Schriftelement greifen und zum Button mit dem vorgegebenen Tag führen, damit das Spiel beendet wird.
public class ExitGrabButton : MonoBehaviour
{
    Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = FindObjectOfType<Valve.VR.InteractionSystem.Hand>();
    }

    // Beim Loslassen soll das Objekt wieder auf seine Standardposition!
    public void OnDetached()
    {
        transform.position = new Vector3(0.3f, 1.414f, 1.074f);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    // Beim Treffen auf dem Button mit dem vorgegeben Tag, wird das Spiel beendet! 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ExitButton")
        {
            Application.Quit();
        }
    }
}
