using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeBehaviour : MonoBehaviour {
    public float fuseTime;
    public GameObject explosionEffect;
    public float radiusEffect = 1.5f;
    public float stunTime = 1f;

    private Vector3 explosionPosition;
    private List<GameObject> playerList = new List<GameObject>();
    private List<float> distanceExplosion = new List<float>();

    // Use this for initialization
    void Start () {
        Invoke("Explode", fuseTime);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Explode", 0);
    }

    void Explode ()
    {
        if (this != null)
        {
            GameObject explode = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);

            Destroy(this.GetComponent<SpriteRenderer>());
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(explode, 0.5f);
            Destroy(this.gameObject, stunTime + 1f);

            explosionPosition = this.transform.position;

            playerList.Add(GameObject.FindGameObjectWithTag("player1"));
            playerList.Add(GameObject.FindGameObjectWithTag("player2"));
            //playerList.Add(GameObject.FindGameObjectWithTag("player3"));
            //playerList.Add(GameObject.FindGameObjectWithTag("player4"));
            for (int i = 0; i < playerList.Count; i++)
            {
                distanceExplosion.Add(Vector3.Distance(playerList[i].transform.position, explosionPosition));

                if (distanceExplosion[i] < radiusEffect)
                {
                    StartCoroutine(stunPlayer(stunTime, playerList[i]));
                }
            }
        }
    }

    IEnumerator stunPlayer(float waitTime, GameObject player)
    {
        // This function temprarily freezes movement
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(waitTime);

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // FreezeRotaton is the natural state of the rigidbody

    }
}
