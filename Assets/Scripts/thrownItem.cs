using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrownItem : MonoBehaviour {
    public float throwCooldown = 1f;
    public GameObject grenadePrefab;
    public float grenadeSpeed;
    public float minTorque;
    public float maxTorque;
    public string fire2Controller = "Fire2_player1";
    public string playerController = "player1";
    public string throwController = "throw source player1";

    private GameObject throwSource;
    private float nextFire = 0.0f;

    // Use this for initialization
    void Start () {
        throwSource = GameObject.FindGameObjectWithTag(throwController);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButton(fire2Controller) && Time.time > nextFire)
        {
            nextFire = Time.time + throwCooldown;

            GameObject player = GameObject.FindGameObjectWithTag(playerController);
            Vector2 trajectory = player.transform.right;

            GameObject grenade = (GameObject)Instantiate(grenadePrefab, throwSource.transform.position, transform.rotation);

            grenade.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(minTorque, maxTorque), ForceMode2D.Force);
            grenade.GetComponent<Rigidbody2D>().AddForce(trajectory * grenadeSpeed);
            
        }
	}

}
