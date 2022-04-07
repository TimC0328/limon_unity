
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public InventoryUI inventory;
    public float dest;
    private float speed = 2f;
    public float leftLimit = -100f;
    public float rightLimit = 100f;

    public enum States {DEFAULT, INVENTORY, DIALOG };
    public States state = States.DEFAULT;
    private States prevState = States.DEFAULT;
    
    void Awake()
    {
        //inventory = Inventory.Instance();
    }

    void Update()
    {

        if(Input.GetButtonDown("Inventory"))
        {
            if (state != States.INVENTORY)
            {
                prevState = state;
                state = States.INVENTORY;
            }
            else
                state = prevState;

            inventory.ToggleInventory();
        }

        if (state != States.DEFAULT)
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
            if (dest > transform.position.x && transform.position.x <= rightLimit)
                transform.position = new Vector3(transform.position.x + 1 * speed * step, transform.position.y, transform.position.z);
            if (dest < transform.position.x && transform.position.x >= leftLimit)
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

    public void SetState(int input)
    {
        prevState = state;
        state = (States)input;
    }

}
