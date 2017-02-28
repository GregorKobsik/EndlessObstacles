using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{


    // Update is called once per frame
    /// <summary>
    /// this function causes the object to rotate around its own y-axis.
    /// </summary>
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    /// <summary>
    /// this function is called, when the player object collides with the pickable object.
    /// on a pickup the renderer of the object is deactivatet and the particle system is started.
    /// after 1 second the object is destroyed and removed from the szene.
    /// </summary>
    /// <param name="speed"></param>
    public void pickUp(float speed)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().startSpeed = speed * 2f;
        gameObject.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 1f);
    }

}
