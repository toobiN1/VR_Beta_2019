using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dieses Skript ist für das Instanziieren der Blöcke zuständig.
public class BlockSpawnController : MonoBehaviour
{
    // Alle Prefabs der Blöcke werden eingeladen
    public GameObject block1, block2, block3, block4, block5, block6, block7, block8;
    public Transform spawnLocation;
    Transform blockTransform1, blockTransform2, blockTransform3, blockTransform4, blockTransform5, blockTransform6, blockTransform7, blockTransform8;

    // SpawnZeit wird in Unity bestimmt.
    public float spawnTimerMin;
    public float spawnTimerMax;

    private bool spawnReady;
    public bool blockFalling = false;
    bool block1Falling, block2Falling, block3Falling, block4Falling, block5Falling, block6Falling, block7Falling, block8Falling = false;
    float currentXPos;
    bool onlyOnce = true;
    public bool phaseTwo;


    public HexagonRenderScript hexRenderScript;




    void Start()
    {
        // Bei Start() wird die Coroutine zum Instanziieren eines zufälliges Blocks zu zufälliger Zeit gestartet.
        phaseTwo = false;
        spawnReady = false;
        StartCoroutine(SpawnRate());
        hexRenderScript = GetComponent<HexagonRenderScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (spawnReady && !phaseTwo)
        {
            spawnReady = false;
            SpawnBlock(Random.Range(0, 8));
        }

        // If-Abfrage, die fragt ob ein Block fällt, welcher der Blöcke fällt und ob dieser vom Spieler aufgehoben wurde durch Abfragen einer Änderung in der X-Position.
        // Die Zwiespältigkeit dieser If-Abfrage sorgt dafür, dass viele Abfragen nicht doppelt gemacht werden.
        if (blockFalling)
        {
            if (block1Falling)
            {
                if (currentXPos != blockTransform1.transform.position.x)
                {
                    OnBlockPickedUp();
                    block1Falling = false;
                }
            }
            if (block2Falling)
            {
                if (currentXPos != blockTransform2.transform.position.x)
                {
                    OnBlockPickedUp();
                    block2Falling = false;
                }
            }
            if (block3Falling)
            {
                if (currentXPos != blockTransform3.transform.position.x)
                {
                    OnBlockPickedUp();
                    block3Falling = false;
                }
            }
            if (block4Falling)
            {
                if (currentXPos != blockTransform4.transform.position.x)
                {
                    OnBlockPickedUp();
                    block4Falling = false;
                }
            }
            if (block5Falling)
            {
                if (currentXPos != blockTransform5.transform.position.x)
                {
                    OnBlockPickedUp();
                    block5Falling = false;
                }
            }
            if (block6Falling)
            {
                if (currentXPos != blockTransform6.transform.position.x)
                {
                    OnBlockPickedUp();
                    block6Falling = false;
                }
            }
            if (block7Falling)
            {
                if (currentXPos != blockTransform7.transform.position.x)
                {
                    OnBlockPickedUp();
                    block7Falling = false;
                }
            }
            if (block8Falling)
            {
                if (currentXPos != blockTransform8.transform.position.x)
                {
                    OnBlockPickedUp();
                    block8Falling = false;
                }
            }
        }


    }

    public void StartPhaseTwo()
    {
        phaseTwo = true;
    }

    // Methode zum Instanziieren der Blöcke: Durch Zufall wird einer der Blöcke ausgewählt. Dessen x.position wird gespeichert, um später zu bestimmen, ob der Spieler
    // diesen aufgehoben hat.
    void SpawnBlock(int x)
    {

        switch (x)
        {
            case 0:
                blockTransform1 = Instantiate(block1.transform, spawnLocation.position, Quaternion.identity);
                block1Falling = true;
                currentXPos = blockTransform1.transform.position.x;
                break;
            case 1:
                blockTransform2 = Instantiate(block2.transform, spawnLocation.position, Quaternion.identity);
                block2Falling = true;
                currentXPos = blockTransform2.transform.position.x;
                break;
            case 2:
                blockTransform3 = Instantiate(block3.transform, spawnLocation.position, Quaternion.identity);
                block3Falling = true;
                currentXPos = blockTransform3.transform.position.x;
                break;
            case 3:
                blockTransform4 = Instantiate(block4.transform, spawnLocation.position, Quaternion.identity);
                block4Falling = true;
                currentXPos = blockTransform4.transform.position.x;
                break;
            case 4:
                blockTransform5 = Instantiate(block5.transform, spawnLocation.position, Quaternion.identity);
                block5Falling = true;
                currentXPos = blockTransform5.transform.position.x;
                break;
            case 5:
                blockTransform6 = Instantiate(block6.transform, spawnLocation.position, Quaternion.identity);
                block6Falling = true;
                currentXPos = blockTransform6.transform.position.x;
                break;
            case 6:
                blockTransform7 = Instantiate(block7.transform, spawnLocation.position, Quaternion.identity);
                block7Falling = true;
                currentXPos = blockTransform7.transform.position.x;
                break;
            case 7:
                blockTransform8 = Instantiate(block8.transform, spawnLocation.position, Quaternion.identity);
                block8Falling = true;
                currentXPos = blockTransform8.transform.position.x;
                break;
        }
        blockFalling = true;
        //Hexagon auf Material für fallenden Block wechseln
        hexRenderScript.SetActiveHexagonMat();

    }

    // Nach zufälliger Zeit wird das Block Instanziieren eingeleitet.
    IEnumerator SpawnRate()
    {
        yield return new WaitForSeconds(Random.Range(spawnTimerMin, spawnTimerMax));
        spawnReady = true;
    }



    // Zerstörung des Hexagons, wenn es auf einen Block trifft.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Block")
        {
            // onlyOnce muss sein, weil sonst mehrere Collisions in einem Frame mehrere Lives heruntersetzen!!!
            if (onlyOnce)
            {
                onlyOnce = false;
                Destroy(this.gameObject);
                GameManager.lives--;
            }

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            // onlyOnce muss sein, weil sonst mehrere Collisions in einem Frame mehrere Lives heruntersetzen!!!
            if (onlyOnce)
            {
                onlyOnce = false;
                Destroy(this.gameObject);
                GameManager.lives--;
            }
        }
    }

    // Wenn der Spieler einen Block nun aufgehoben hat, soll das Standardmaterial wieder genommen werden.
    void OnBlockPickedUp()
    {
        blockFalling = false;
        hexRenderScript.isActiveHexagonMat = false;
        hexRenderScript.SetStandardMat();
        //Coroutine wird erneut eingeleitet
        StartCoroutine(SpawnRate());
    }

}
