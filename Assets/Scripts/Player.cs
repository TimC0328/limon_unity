
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float dest;
    public bool canMove = true;
    private float speed = 2f;
    
    void Awake()
    {

    }

    void Update()
    {
        if (!canMove)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        HandleMovement();

    }

    void HandleMovement()
    {
        float step = speed * Time.deltaTime;
        if(transform.position.x != dest)
        {
            if (dest > transform.position.x)
                transform.position = new Vector3(transform.position.x + 1 * speed * step, transform.position.y, transform.position.z);
            if (dest < transform.position.x)
                transform.position = new Vector3(transform.position.x - 1 * speed * step, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Actor actor = col.gameObject.GetComponent<Actor>();
        if(actor)
            actor.HandleCollision(this);
        dest = transform.position.x;

    }

}
