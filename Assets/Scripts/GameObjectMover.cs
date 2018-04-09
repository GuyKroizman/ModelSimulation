using UnityEngine;
using System.Collections;

public class GameObjectMover
{
    private float SPEED = 2;

    private GameObject thingToMove;
    private Vector3 destination;

    public GameObjectMover(GameObject movee, Vector3 dest)
    {
        thingToMove = movee;
        destination = dest;
    }
    public void Update()
    {
        float step = SPEED * Time.deltaTime;
        thingToMove.transform.position = Vector3.MoveTowards(thingToMove.transform.position, destination, step);
    }

    public bool IsReachedDestination()
    {
        return Vector3.Distance(thingToMove.transform.position, destination) < 0.1;
    }
}