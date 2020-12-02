using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (PlayerController.isDead) return;
        this.transform.position += PlayerController.player.transform.forward * -0.1f;

        if (PlayerController.currentPlatform == null) return;
        if (PlayerController.currentPlatform.tag == "StairsUp")
            this.transform.Translate(0, -0.06f, 0);
        if(PlayerController.currentPlatform.tag == "StairsDown")
            this.transform.Translate(0, 0.06f, 0);
    
    }
}
