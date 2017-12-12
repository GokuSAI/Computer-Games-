using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public int pistolAmmo;
    public Text ammoCount;

	// Use this for initialization
	void Start ()
    {
        pistolAmmo = 30;
        SetAmmoText();
    }
	
	// Update is called once per frame
	void Update ()
    {
        SetAmmoText();
	}

    // Call to update ammo count
    void SetAmmoText()
    {
        ammoCount.text = "Ammo: " + pistolAmmo.ToString();
    }

    // Decrease pistolAmmo
    public void DecreasePistolAmmo()
    {
        pistolAmmo--;
    }
}
