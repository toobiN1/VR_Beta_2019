using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonRenderScript : MonoBehaviour
{
    // Dieses Script ist verantwortlich für das Wechseln der Materialien eines Hexagons!
    public Material activeHexagonMat;
    public Material selfDestructMat;
    public Material standardMat;
    new Renderer renderer; 
    public bool isSelfDestructMaterial;
    public bool isActiveHexagonMat;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        isActiveHexagonMat = false;
        isSelfDestructMaterial = false;
    }
    // Das Selbstzerstörungsmaterial hat die höchste Priorität und überschreibt IMMER die anderen Materialen!
    public void SetSelfDestructMat(){
        isSelfDestructMaterial = true;
        isActiveHexagonMat = false;
        renderer.material = selfDestructMat;
    }
    // Das Material für die Hexagone, wo ein Block herunterfällt, hat die zweithöchste Priorität und überschreibt NUR das Standardmaterial!
    public void SetActiveHexagonMat(){
        if(!isSelfDestructMaterial){
            isActiveHexagonMat = true;
            isSelfDestructMaterial = false;
            renderer.material = activeHexagonMat;
        }
    }
    // Das Standardmaterial wird von den anderen Scripts aufgerufen, wenn auf dem Hexagon gerade kein Block fällt 
    // und nicht eine Selbstzerstörung bevorsteht. Deswegen müssen BEIDE auf false gesetzt sein, damit das erst passiert.
        public void SetStandardMat(){
        if(!isSelfDestructMaterial && !isActiveHexagonMat){
            renderer.material = standardMat;
        }
    }

}
