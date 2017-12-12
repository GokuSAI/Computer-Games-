using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // play some pickup sound here
           Inventory pInv = col.GetComponent<Inventory>();
           pInv.pistolAmmo += 3;
           Destroy(gameObject);
        }
    }
}
