using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float sidewaysForce = 50f;
    public float jumpForce = 1f;
    public float speed = 15f;
    public float maxSpeed = 15f;

    public GameObject gameManager;

    private bool isOnGorund = true;

    //Collision with PickUp's
    /// <summary>
    /// This function handles the collision of the player object with objects that have a trigger,
    /// mainly pickable objects. The player object calls then the pickUp method of the pickable object and
    /// then passes the object further to the GameManager to handle the behaivior of the element.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            other.GetComponent<Rotator>().pickUp(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
            gameManager.GetComponent<GameManager>().gotPickable(other.GetComponent<GameObject>());
        }
    }

    /// <summary>
    /// this function is used for movement of the player. Each frame the player object can receive forces
    /// from the input. This function reads the input from the human player and translates it to
    /// forces which are added to the player object.
    /// </summary>
    void FixedUpdate () {

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            rb.AddForce(sidewaysForce * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
        else
            rb.velocity.Set(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), 0, rb.velocity.z);


        if (Input.GetButtonDown("Jump") )
        {
            if (Physics.Raycast(transform.position,Vector3.down,0.6f))
            {
                rb.AddForce(0, jumpForce, 0);
                rb.AddTorque(jumpForce / 10, 0, 0);
            }
            
        }

        if (rb.velocity.z < maxSpeed)
            rb.AddForce(0, 0, speed * Time.deltaTime);
        else
            rb.velocity.Set(rb.velocity.x, 0, maxSpeed);

    }


}
