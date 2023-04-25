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
    private void Update()
    {
        if (item)
        {

            held.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * distance;
            Debug.Log(held.name);
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.S))
            {

                item = false;
                held.GetComponent<Rigidbody>().isKinematic = false;
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
                    GameObject temp = hit.collider.gameObject;
                    Debug.Log(temp.name);
                    while (temp.name != "chair_1(Clone)" && temp.name != "desk_1(Clone)")
                    {
                        temp = temp.transform.parent.gameObject;
                        Debug.Log(temp.name);
                    }
                    held = temp;
                    temp = null;
                    distance = (hit.distance);
                    if (distance < 1)
                    {
                        distance = 1;
                    }
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.S))
                    {

                        if (held != null)
                        {
                            held.GetComponent<Rigidbody>().isKinematic = true;

                            held.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                            item = true;
                            actual.enabled = false;
                        }

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


