using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCreator : MonoBehaviour {

    public GameObject packet;

    private float timer;
    private float tubeSize;

	void Start () {
        tubeSize = 1f;
	}
	
	
	void Update () {
        timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            timer = Random.Range(0.01f, 0.2f);

            float xAdd = Random.Range(-1*tubeSize, tubeSize);
            float yAdd = Random.Range(-1*tubeSize, tubeSize);
            var position = new Vector3(transform.position.x + xAdd, transform.position.y + yAdd, transform.position.z);
            var p = Instantiate(packet, position, Quaternion.identity);

            float scaleFactor = 0;
            if (Random.Range(0f, 1f) > 0.8)
            {
                scaleFactor = Random.Range(0f, 0.8f);
            }
            
            p.transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);

        }
	}
}
