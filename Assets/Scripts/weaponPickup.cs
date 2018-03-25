using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour {
    public GameObject newWeapon;

    private GameObject player;
    private Vector2 weaponPos;
    private Quaternion weaponRot;
    private GameObject[] currentWeaponList;
    private Transform currentWeapon;
    private GameObject weaponTag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        currentWeaponList = GameObject.FindGameObjectsWithTag("current weapon");
        Transform[] currentWeaponTransforms = new Transform[currentWeaponList.Length];
        for (int i = 0; i < currentWeaponList.Length; i++)
        {
            currentWeaponTransforms[i] = currentWeaponList[i].transform;
        }

        currentWeapon = GetClosestEnemy(currentWeaponTransforms);
        weaponPos = currentWeapon.position;
        weaponRot = currentWeapon.rotation;

        if (trigger.gameObject.tag == "player1" ||
            trigger.gameObject.tag == "player2" ||
            trigger.gameObject.tag == "player3" ||
            trigger.gameObject.tag == "player4")
        {
            player = GameObject.FindGameObjectWithTag(trigger.gameObject.tag);
            Destroy(currentWeapon.gameObject);
            GameObject Weapon = (GameObject)Instantiate(newWeapon, weaponPos, weaponRot);
            Weapon.transform.parent = player.transform;

            weaponTag = GameObject.FindGameObjectWithTag("player weapon");
            weaponTag.gameObject.tag = trigger.gameObject.tag + " weapon";
            Weapon.GetComponent<PistolShoot>().weaponController = weaponTag.gameObject.tag;
            Weapon.GetComponent<PistolShoot>().playerController = trigger.gameObject.tag;
            Weapon.GetComponent<PistolShoot>().fire1Controller = "Fire1_" + trigger.gameObject.tag;

        }
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


}
