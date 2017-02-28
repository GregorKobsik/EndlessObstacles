using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour {

    public Transform pickUp;
    public Transform cubeObstacle;
    public Transform rampObstacle;

	// Use this for initialization
	void Start () {
        //create level 0 level at the beginning of the game, when the object is initialized
        createLevel(0);
	}

    /// <summary>
    /// this function creates the level, when it is called.
    /// it creates obstacles, pickables and other elements of a certain level.
    /// the level can be choosen with the levelNo parameter.
    /// </summary>
    /// <param name="levelNo"></param>
    public void createLevel(int levelNo)
    {

        if (levelNo < 1)
        {
            createBorder();
        }

        //create random obstacles every few steps
        for (int i = -470; i < 480; i++)
        {
            if (Random.Range(0, 5) == 0)
            {
                //obstacle
                createObstacle(i, levelNo);
                //pickable
                createPickable(new Vector3(Random.Range(-4, 5), 0.5f, i + 5));
                i += 10;
            }
        }
    }

    /// <summary>
    /// this function resets the game level, by destroying all elements in the current level
    /// and later creating a completly new level given by the levelNo parameter.
    /// </summary>
    /// <param name="levelNo"></param>
    public void resetLevel(int levelNo)
    {
        //remove all elements from map
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //create a new level
        createLevel(levelNo);
    }
	
    /// <summary>
    /// this function is called to determine what kind of obstacle should be created at
    /// a certain position. the position of the obstacle along the track is choosen with the pos argument.
    /// </summary>
    /// <param name="pos"></param>
    void createObstacle(int pos, int modifier)
    {
        modifier = Random.Range(0, modifier) % 4;
        switch (modifier) {
            case 0:
                creatBlockObstacle(Random.Range(1, 3), pos);
                break;
            case 1:
                creatBlockObstacle(Random.Range(1, 4), pos);
                break;
            case 2:
                creatBlockObstacle(Random.Range(1, 5), pos);
                break;
            case 3:
                createRampObstacle(Random.Range(1, 5), pos);
                break;
            case 4:
                creatBlockObstacle(Random.Range(1, 5), pos);
                break;
            default:
                creatBlockObstacle(Random.Range(1, 4), pos);
                break;
        }
        
    }

    /// <summary>
    /// this function creates a simple block obstacle, which consists of 1 to 3 cubes which are placed in
    /// a straigt line orthogonal to the plane direction. the number of objects can be choosen with the
    /// number parameter, the position along the track, in the z direction with the pos argument.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="pos"></param>
	void creatBlockObstacle(int number, int pos, bool isDeadly = false)
    {
        switch(number)
        {
            case 1:
                createBox(new Vector3(Random.Range(-4,4), 0.5f, pos));
                break;
            case 2:
                createBox(new Vector3(-3 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(3 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 3:
                createBox(new Vector3(0 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-3 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(3 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 4:
                createBox(new Vector3(-4 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-1 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(1 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(4 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 5:
                createBox(new Vector3(-4 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-2 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(0 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(2 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(4 + Random.Range(-1, 1), 0.5f, pos));
                break;
            default:
                Debug.Log("Error: unknow number in createBlockObstacle");
                break;
        }
    }

    /// <summary>
    /// this function creates a simple block obstacle consisting of a uniform cube, 
    /// with a Rigidbody. The cube has no speed and no rotation applied at the beginning.
    /// the position argument specifies the position of the cube.
    /// </summary>
    /// <param name="position"></param>
    void createBox(Vector3 position, bool isDeadly = false)
    {
        Transform cube = Instantiate(cubeObstacle,transform);
        cube.position = position;
    }

    /// <summary>
    /// this function creates a simple pickable Object, that is specified by the position argument.
    /// </summary>
    /// <param name="position"></param>
    void createPickable(Vector3 position)
    {
        Transform trans = Instantiate(pickUp, transform);
        trans.position = position;
    }

    /// <summary>
    /// this function creates a border on both sides of the level.
    /// this is a help in the first levels to preven the player object from falling of the ground
    /// and help new players to with lower difficulty.
    /// </summary>
    void createBorder()
    {
        GameObject cube;

        //creates the left border
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = transform;
        cube.transform.position = new Vector3(-5,0,0);
        cube.transform.localScale = new Vector3(0.5f, 1f, 1000);

        //creates the right border
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = transform;
        cube.transform.position = new Vector3(5, 0, 0);
        cube.transform.localScale = new Vector3(0.5f, 1f, 1000);
    }

    /// <summary>
    /// this function creates a simple ramp on the given position.
    /// </summary>
    /// <param name="position"></param>
    void createRamp(Vector3 position)
    {
        Transform ramp = Instantiate(rampObstacle, transform);
        ramp.position = position;
    }

    void createRampObstacle(int number, int pos, bool isDeadly = false)
    {
        switch (number)
        {
            case 1:
                createBox(new Vector3(Random.Range(-4, 4), 0.5f, pos));
                break;
            case 2:
                createBox(new Vector3(-3 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(3 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 3:
                createBox(new Vector3(0 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-3 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(3 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 4:
                createBox(new Vector3(-4 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-1 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(1 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(4 + Random.Range(-1, 1), 0.5f, pos));
                break;
            case 5:
                createBox(new Vector3(-4 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(-2 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(0 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(2 + Random.Range(-1, 1), 0.5f, pos));
                createBox(new Vector3(4 + Random.Range(-1, 1), 0.5f, pos));
                break;
            default:
                Debug.Log("Error: unknow number in createBlockObstacle");
                break;
        }
        createRamp(new Vector3(0, 0.5f, pos + 1));
    }

}
