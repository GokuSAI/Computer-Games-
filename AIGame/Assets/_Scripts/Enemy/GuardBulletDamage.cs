using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBulletDamage : MonoBehaviour {

    public int damage;

    // Check what bullet hit with tags
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().GotShot(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Level")
        {
            Destroy(gameObject);
        }
    }
}
