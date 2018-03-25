using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectBehaviour : MonoBehaviour {

    public int maxBounce;
    public float minSpeed;
    public GameObject bounceAnimation;
    public GameObject bloodHit;
    public GameObject bloodDeath;

    private Rigidbody2D rb;
    private GameObject bullet;
    private Vector2 initialVelocity;
    private Vector2 reflectedDir;
    private int bounceCount = 0;
    private int healthCheck;

    // Use this for initialization
    void Start()
    {
        bullet = this.gameObject;
        rb = bullet.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        initialVelocity = rb.velocity;
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (bounceCount > maxBounce)
        {
            Destroy(bullet);
        }
        if (rb.velocity.magnitude < minSpeed)
            Destroy(bullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "wall" || collision.collider.gameObject.tag == "shield")
        {
            reflectedDir = Vector2.Reflect(initialVelocity, collision.contacts[0].normal);

            rb.velocity = reflectedDir;
            

            bounceCount++;

            GameObject newParticle = (GameObject)Instantiate(bounceAnimation, transform.position, transform.rotation);
            Destroy(newParticle, 0.1f);

        }
        if (collision.gameObject.tag == "player hitbox")
        {
            Debug.Log("hit player hitbox");
            Destroy(bullet.GetComponent<CapsuleCollider2D>()); // This line is to prevent multiple hits from one bullet

            GameObject HitParticle = (GameObject)Instantiate(bloodHit, transform.position, transform.rotation);
            Destroy(HitParticle, 0.1f);

            collision.transform.parent.gameObject.GetComponent<PlayerMovement>().health -= 1;
            healthCheck = collision.transform.parent.gameObject.GetComponent<PlayerMovement>().health;
            if (healthCheck <= 0)
            {
                Destroy(collision.transform.parent.gameObject);
                GameObject deathParticle = (GameObject)Instantiate(bloodDeath, transform.position, transform.rotation);
                Destroy(deathParticle, 0.1f);

            }

            Destroy(bullet);
        }
    }
}
