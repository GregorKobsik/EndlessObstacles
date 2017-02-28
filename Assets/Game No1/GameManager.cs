using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject obstacleCreator;

    public float pickupBonus = 100f;
    public float speedBonus = 10f;

    private float score = 0f;
    private int level = 0;

	// Use this for initialization
	void Start () {
        //instantiate the obstacleCreate from a prefab
        obstacleCreator = Instantiate(obstacleCreator);
	}
	
	// Update is called once per frame
	void Update () {
        //add points to score if the player has almost maximum speed
        if (player.GetComponent<Rigidbody>().velocity.magnitude >= player.GetComponent<PlayerController>().maxSpeed * 0.95)
        {
            score += speedBonus * Time.deltaTime;
        }

        //check if the player is still on the track otherwise call gameOver
        if (player.GetComponent<Transform>().position.y < -10)
        {
            gameOver();
        }
    }

    //Collision with PickUp's
    /// <summary>
    /// this function is mainly used to detect the collision of the GameManager with the player.
    /// the goal of the player object is to reach the finish line, which is represented by the GameManager.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            resetPlayerPos(other.GetComponent<Transform>());
            level++;
            obstacleCreator.GetComponent<ObstacleCreator>().resetLevel(level);

            
        }

    }

    /// <summary>
    /// this function resets the position of the given GameObject to the default player position.
    /// </summary>
    /// <param name="player"></param>
    private void resetPlayerPos(Transform player, bool resetAllStats = false)
    {
        player.position = new Vector3(player.position.x, player.position.y, -499f);

        if (resetAllStats)
        {
            player.position = new Vector3(0, 0.5f, -499f);
            player.rotation = new Quaternion(0, 0, 0, 1);
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
        }

    } 

    /// <summary>
    /// This function handles the pickups in the level. After a collision with a pickable object,
    /// the player object calls this function with the collided object as parameter.
    /// The GameManager should handle then the object and create proper behavior.
    /// </summary>
    /// <param name="pickable"></param>
    public void gotPickable(GameObject pickable)
    {
        score += pickupBonus;
    }

    /// <summary>
    /// this function is called, when the player object falls off the playground or somehow
    /// is destroyed by an obstacle or ends the game by other means.
    /// </summary>
    void gameOver()
    {
        score = 0;
        level = 0;

        //TODO: change this function later to call a menu or highscore view.
        obstacleCreator.GetComponent<ObstacleCreator>().resetLevel(level);
        resetPlayerPos(player.GetComponent<Transform>(), true);
    }

    /// <summary>
    /// this function creates a simple gui to show the score of the game
    /// </summary>
    private void OnGUI()
    {
        int boxWidth = 80;
        //top left corner
        Rect topLeft = new Rect(new Vector2(0, 0), new Vector2(boxWidth, 40));
        GUI.Box(topLeft, "Score");
        topLeft.position = new Vector2(5, 20);
        GUI.Label(topLeft, "" + Mathf.Round(score));

        //top right corner
        Rect topRight = new Rect(new Vector2(Screen.width - boxWidth, 0), new Vector2(boxWidth, 40));
        GUI.Box(topRight, "Level");
        topRight.position = new Vector2(Screen.width - boxWidth/2 - 5, 20);
        GUI.Label(topRight, "" + level);
    }
}
