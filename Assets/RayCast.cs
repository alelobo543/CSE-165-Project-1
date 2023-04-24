using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    float startime = 0;
    float maxtime = 1.3f;
    Collider collide = null;
    GameObject held;
    float distance;
    bool item;
    public LineRenderer line;

    LineRenderer actual;
    void Start()
    {
        item = false;
        actual = Instantiate(line);
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        actual.SetPositions(positions);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (item)
        {

            held.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * distance;
            Debug.Log(held.name);
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {

                item = false;
                held = null;
                

            }
        }
        else
        {
            actual.startColor = Color.red;
            actual.endColor = Color.red;
            actual.enabled = true;
            actual.SetPosition(0, transform.position);
            actual.SetPosition(1, transform.TransformDirection(Vector3.forward) * 500);
            int layerMask = ~0;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                
                actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
                if (hit.collider.gameObject.layer == 3)
                {

                    actual.startColor = Color.cyan;
                    actual.endColor = Color.cyan;

                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.S))
                    {
                        
                        held = hit.collider.gameObject;
                       
                        held.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        distance = (hit.distance*1.5f);
                        item = true;
                        actual.enabled = false;

                    }

                }

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                
                startime = 0;
                collide = null;

            }
        }
    }
}

