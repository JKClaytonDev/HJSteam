using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreObj : MonoBehaviour
{
    public GameObject obj;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<gameEnemy>())
            Destroy(other.gameObject);
    }
}
