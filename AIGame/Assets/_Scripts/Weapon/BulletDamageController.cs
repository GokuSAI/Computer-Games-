using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageController : MonoBehaviour {

    public int damage;

    // Check what bullet hit with tags
	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().GotShot(damage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.tag == "Level")
        {
            Destroy(gameObject);
        }
    }
}
