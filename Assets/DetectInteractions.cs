using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteractions : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            collision.gameObject.GetComponent<CratePhysics>().DestroyCrate();
        Debug.Log("Coliziune cu o cutie");
        }
    }
}
