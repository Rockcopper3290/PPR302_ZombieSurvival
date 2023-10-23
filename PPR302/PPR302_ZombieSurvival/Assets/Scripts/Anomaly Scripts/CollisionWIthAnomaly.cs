using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWIthAnomaly : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Burner Anomaly")
        {
            //play a poof partical effect at point of impact
            Destroy(this.gameObject);
        }
    }
}
