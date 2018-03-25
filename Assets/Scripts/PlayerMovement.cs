using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public int health = 3;
    public float maxSpeed;
    public float moveSpeed;
    public string horizontalMoveController = "Horizontal_Move_P1";
    public string verticalMoveController = "Vertical_Move_P1";
    public string horizontalLookController = "Horizontal_Look_P1";
    public string verticalLookController = "Vertical_Look_P1";

    private Rigidbody2D rb;
    private Vector2 input;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        input = new Vector2(Input.GetAxis(horizontalMoveController), Input.GetAxis(verticalMoveController));
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(input * moveSpeed);
        }
        Vector2 lookDirection = new Vector2(Input.GetAxis(horizontalLookController), +Input.GetAxis(verticalLookController));
        if (lookDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
