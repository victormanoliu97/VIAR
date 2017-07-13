using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePhysics : MonoBehaviour {

    public float countdown = 5.0f;
    public void DestroyCrate()
    {
        GetComponent<Collider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
        for (int i = 0; i < 6; ++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Collider>().enabled = true;
            child.AddComponent<Rigidbody>();
        }
        Destroy(gameObject, countdown);
    }
}
