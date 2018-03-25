using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    
    public List<GameObject> Bullets = new List<GameObject>();
    public GameObject muzzleFlash;
    public float projectileVelocity;
    public float fireRate = 0.1f;
    public float flightTime = 1.2f;
    public string fire1Controller;
    public string weaponController;
    public string playerController;

    private int bulletChoice;
    private float nextFire = 0.0f;
    private Rigidbody2D rb;
    private GameObject playerWeapon;
    private List<GameObject> Projectiles = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        //rb = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (Input.GetButton(fire1Controller) && Time.time > nextFire)
        {
            playerWeapon = GameObject.FindGameObjectWithTag(weaponController);
            GameObject player = GameObject.FindGameObjectWithTag(playerController);
            Vector2 trajectory = player.transform.right;
            nextFire = Time.time + fireRate;
            bulletChoice = Random.Range(0, Bullets.Count);

            GameObject Flash = (GameObject)Instantiate(muzzleFlash, transform.position, transform.rotation);
            GameObject bullet = (GameObject)Instantiate(Bullets[bulletChoice], playerWeapon.transform.position, transform.rotation);
            
            Projectiles.Add(bullet);
            bullet.GetComponent<Rigidbody2D>().velocity = player.transform.GetComponent<Rigidbody2D>().velocity / 2;

            /*
            divide by two on previous line because adding the whole velocity of the player movement produces 
            slow bullets if you are moving in the opposite direction due to player speed being so high.
            */

            bullet.GetComponent<Rigidbody2D>().AddForce(trajectory * projectileVelocity);
            
            Destroy(bullet, flightTime);
            Destroy(Flash, 2);
        }
        
	}
}
