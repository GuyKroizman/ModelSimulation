using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour {

    public int maxPacketInBatch;

    public GameObject batch;
    public GameObject[] batchDestinations;

    private int currentDestinationIndex;
    private int packetCounter = -1;
    private GameObject currentBatch;

    private Kooloobamba kooloobamba;

    class Mover
    {
        private float SPEED = 2;

        GameObject thingToMove;
        Vector3 destination;

        public Mover(GameObject movee, Vector3 dest)
        {
            thingToMove = movee;
            destination = dest;
        }
        internal void Update()
        {
            float step = SPEED * Time.deltaTime;
            thingToMove.transform.position = Vector3.MoveTowards(thingToMove.transform.position, destination, step);
        }

        internal bool IsReachedDestination()
        {
            return Vector3.Distance(thingToMove.transform.position, destination) < 0.1;
        }
    }

    class Kooloobamba
    {
        private List<Mover> movers = new List<Mover>();

        public void Update()
        {
            var toRemove = new List<Mover>();

            foreach (var mover in movers)
            {
                
                if(mover.IsReachedDestination())
                {
                    toRemove.Add(mover);
                    continue;
                }

                mover.Update();
            }

            foreach(var mover in toRemove)
            {
                movers.Remove(mover);
            }
        }

        internal void Add(Mover t)
        {
            movers.Add(t);
        }
    }



    GameObject GetNextDestination()
    {
        currentDestinationIndex++;
        if (currentDestinationIndex >= batchDestinations.Length)
        {
            currentDestinationIndex = 0;
        }
        return batchDestinations[currentDestinationIndex];
    }

    private void Start()
    {
        kooloobamba = new Kooloobamba();
        currentBatch = CreateBatch();

        var t = new Mover(currentBatch, GetFillPosition());
        kooloobamba.Add(t); 

        currentDestinationIndex = 0;
        
    }

    // world space position where the batch should stand to be filled with packets
    private Vector3 GetFillPosition()
    {
        return new Vector3(2f, -3f, 4.3f);
    }

    // world space position where a new batch is loaded
    private Vector3 GetLoadPosition()
    {
        return new Vector3(6f, -13f, 0.85f);
    }

    private GameObject CreateBatch()
    {        
        var createdBatch = Instantiate(batch, GetLoadPosition(), Quaternion.identity);
        createdBatch.transform.Rotate(new Vector3(-90, 45, -90));
        return createdBatch;
    }

    void Update () {

        kooloobamba.Update();

		if(packetCounter >= maxPacketInBatch)
        {
            packetCounter = 0;

            var t = new Mover(currentBatch, GetNextDestination().transform.position);
            kooloobamba.Add(t);

            LoadNewBatch();

        }

    }

    private void LoadNewBatch()
    {
        currentBatch = CreateBatch();

        var t = new Mover(currentBatch, GetFillPosition());
        kooloobamba.Add(t);
    }

    private void OnTriggerEnter(Collider other)
    {
        packetCounter++;
    }
}
