using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeightDisplayScript : MonoBehaviour
{
    public TextMesh heightText;
    public bool running;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
       
        running = true;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Das Textmesh zeigt den score vom gameManager an. Wenn Phase 2 vorbei ist, soll der letzte score weiterhin gespeichert werden.
        if (gameManager.phaseTwo)
        {
            
            heightText.text = gameManager.score.ToString();
            gameManager.score = 0;
        }

    }

}
