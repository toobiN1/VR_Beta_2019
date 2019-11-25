using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dieses Skript betrifft die extra Collider an den Blocks.
// (Dies muss sein, da Mesh Collider als Rigidbody nicht erlaubt sind!)
public class SideBlockScript : MonoBehaviour
{
    
    public GameObject mainBlock;
    Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Die anhängenden Collider sollen wissen, wenn der Ursprungsblock aufgehoben ist, damit auch er nicht mehr is Trigger ist und nun kollidiert.
        BlockController blockController = mainBlock.GetComponent<BlockController>();

        if (blockController.pickedUp == true)
        {
            m_Collider.isTrigger = false;
            transform.tag = "Untagged";
        }else{
            transform.tag = "Block";
        }
    }
    // Der ganze Block soll zerstört werden, wenn dieser Collider trifft.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hexagon")
        {
            Destroy(mainBlock);
        }
        // Tag "Body" betrifft den Kopf Collider.
        if (col.gameObject.tag == "Body")
        {
            Physics.IgnoreCollision(col.collider, m_Collider);
        }
        if (col.gameObject.tag == "Floor")
        {
            Destroy(mainBlock);
        }
    }
    // Der ganze Block soll zerstört werden, wenn dieser Collider trifft.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hexagon"))
        {
            Destroy(mainBlock);
        }
    }


}
