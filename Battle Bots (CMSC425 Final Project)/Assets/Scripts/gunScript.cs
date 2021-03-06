﻿
using UnityEngine;

public class gunScript : MonoBehaviour {

	public int damage;
	public float range = 100f;
	public float fireRate = 1f;


	/* public GameObject laser;
	public Transform laserSpawn; */
	public Camera fpsCam;

	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

    void Start ()
    {
        damage = PlayerPrefs.GetInt("PlayerDamage");
    }

    // Update is called once per frame
    void Update () {

		if (Input.GetButton ("Fire1") && Time.time >=nextTimeToFire) {

			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot ();
		}
	}

	void Shoot(){

		Fire ();

		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {


			if (hit.transform.tag == "Enemy") {
                Target target = hit.transform.GetComponent<Target>();
                target.TakeDamage (damage);
			}

			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * 30);
			}

			GameObject impact = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impact, .1f);
		}

	}

	void Fire(){
        
        /*
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;

		Destroy(bullet, 2.0f);
        */


	}
}
