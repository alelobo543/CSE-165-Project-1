using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject chair;
    public GameObject desk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3  spawnLocation = transform.TransformDirection(Vector3.forward);
        spawnLocation.y = 0.5f;
        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space))
        {

            Instantiate(chair, transform.position + spawnLocation , chair.transform.rotation);
        }
        if (OVRInput.Get(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.A))
        {

            Instantiate(desk, transform.position + spawnLocation, desk.transform.rotation);
        }

    }
}
