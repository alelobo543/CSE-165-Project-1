using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelRay : MonoBehaviour
{
    // Start is called before the first frame update
    float startime = 0;
    float maxtime = 1.3f;
    Collider collide = null;
    GameObject camera;
    public LineRenderer line;
    LineRenderer actual;
    Vector3 point;
    RaycastHit currhit;
    void Start()
    {
        actual = Instantiate(line);
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        actual.SetPositions(positions);
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A) && currhit.collider.gameObject.name == "Plane")
        {
            GameObject.Find("OVRCameraRig").transform.position = currhit.point;
            GameObject.Find("OVRCameraRig").transform.Translate(new Vector3(0, 0.5f, 0), Space.World);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        actual.enabled = true;
        actual.SetPosition(0, transform.position);
        actual.SetPosition(1, transform.TransformDirection(Vector3.forward) * 500);
        int layerMask = 1;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
            currhit = hit;

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            
            startime = 0;
            collide = null;

        }
    }
}

