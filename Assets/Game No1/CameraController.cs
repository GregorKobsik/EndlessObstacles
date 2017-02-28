using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
	
	// Update is called once per frame
    /// <summary>
    /// this function transforms the camera just behind the player object.
    /// additionaly the rotation of the camera is applied based on the x position of the player object.
    /// </summary>
	void Update () {
        transform.position = player.GetComponent<Transform>().position + offset;
        transform.rotation = Quaternion.Euler(0, -player.GetComponent<Transform>().position.x * 5, 0);
    }
}
