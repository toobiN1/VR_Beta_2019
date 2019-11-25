using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class BlockController : MonoBehaviour
{
    GameManager gameManager;
    Collider m_Collider;
    private Rigidbody rb;
    Throwable throwable;
    // Der Block bewegt sich mit einer Geschwindigkeit(speed)
    float speed = -0.1f;
    public bool pickedUp = false;
    bool pickedUpOnce = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        throwable = GetComponent<Throwable>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Wenn ein Block zu tief fällt, soll er zerstört werden
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        // Blöcke bewegen sich nach unten über Zeit nach Höhe von speed (was im Minusbereich sein soll).
        // Nachdem der Block zum ersten mal genommen wurde, soll er sich nicht mehr bewegen.
        // Die Geschwindigkeit steigt linear über Zeit durch den Timer im gameManager.
        if (pickedUpOnce == false)
        {
            transform.position += transform.up * speed * Time.deltaTime * gameManager.currentTime;

        }
    }


    // Block wird bei Collision und bei Trigger zerstört, damit der Block auch nachdem der Spieler ihn in der Hand hatte, der Block am Boden zerstört wird.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hexagon")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
        // Tag "Body" betrifft den Kopf Collider
        if (col.gameObject.tag == "Body")
        {
            Physics.IgnoreCollision(col.collider, m_Collider);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hexagon"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }

    // Wenn der Spieler mit der Hand mit dem Block interagiert, wird der Block durch Deaktivieren von isTrigger kollidierbar und kann auf den Tisch gelegt werden.
    // Davor kann der Spieler mit der Hand durch die Blöcke durchgehen, damit er sie nicht beim Nehmen aus Versehen aus der Bahn haut!
    public void OnPickedUp()
    {
        pickedUp = true;
        pickedUpOnce = true;
        m_Collider.isTrigger = false;
        rb.useGravity = true;
        // Beim Aufheben wird der tag geändert, damit er nicht zerstört werden kann, wenn er in Hand ist. Sonst meckert SteamVR herum.
        transform.tag = "Untagged";
    }
    public void OnDetached()
    {
        // Beim Loslassen wird der tag geändert, damit der Block wieder zerstört werden kann.
        transform.tag = "Block";
        pickedUp = false;
    }

}
