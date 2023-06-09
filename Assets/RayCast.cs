using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    float startime = 0;
    float maxtime = 1.3f;
    Collider collide = null;
    bool selectorMode = true;
    public GameObject held;
    float distance;
    bool item;
    bool rotate;
    bool scale;
    Vector3 starthand;
    public LineRenderer line;
    public GameObject panel;
    public Image img;
    public Button btn;
    public GameObject chair;
    public GameObject tv;
    public GameObject desk;
    public GameObject whiteboard;
    public GameObject cabinet;
    public GameObject locker;
    public GameObject selector;
    GameObject spehere_selector;
    float startDist;
    Vector3 startScale;
    public void Select()
    {
        distance = 1;
        held.GetComponent<Rigidbody>().isKinematic = true;
        Light[] children = held.GetComponentsInChildren<Light>();
        foreach (Light l in children)
        {
            l.enabled = true;
        }
        item = true;
        actual.enabled = false;
    }
    LineRenderer actual;
    void Start()
    {
        item = false;
        rotate = false;
        scale = false;
        actual = Instantiate(line);
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        actual.SetPositions(positions);
        img = panel.GetComponent<Image>();
    }
    public void SelectorCollision(onCollision script, GameObject col)
    {
        held = col;
        Select();
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.I))
        {
            
            if (GameObject.Find("Spawn Menu")!=null)
            {
                GameObject.Find("Spawn Menu").SetActive(false);
            }
            else
            {
                Debug.Log(GameObject.Find("Spawn Menu").active);
                GameObject.Find("Spawn Menu").SetActive(true);
            }

        }

        if (item)
        {
            if (!rotate&&!scale)
            {
                held.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * distance;
            }
            Debug.Log(held.name);
            if (scale)
            {
                float dist = (transform.position - GameObject.Find("RightHandAnchor").transform.position).magnitude;
                if (dist == 0)
                {
                    dist = 0.0001f;
                }
                float ratio = dist / startDist;

                if (ratio < 0.2f)
                {
                    ratio = 0.25f;
                }
                if (ratio > 4f)
                {
                    ratio = 4;
                }
                held.transform.localScale = startScale * (dist / startDist);
                if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.L))
                {
                    scale = false;
                }
            }
            else if (rotate)
            {

                Vector3 dist = (transform.position - starthand);
                Debug.Log(dist);
                if (Vector3.Dot(held.transform.up, Vector3.up) >= 0)
                {
                    held.transform.Rotate(held.transform.up*3f, -Vector3.Dot(dist, transform.right)*3f, Space.World);
                }
                else
                {
                    held.transform.Rotate(held.transform.up*3f, Vector3.Dot(dist, transform.right)*3f, Space.World);
                }
                held.transform.Rotate(transform.right*3f, Vector3.Dot(dist, transform.up) * 3f, Space.World);
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.L))
                {
                    rotate = false;
                }
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.S))
            {
                Light[] children = held.GetComponentsInChildren<Light>();
                foreach (Light l in children)
                {
                    l.enabled = false;
                }
                rotate = false;
                item = false;
                scale = false;
                held.GetComponent<Rigidbody>().isKinematic = false;
                held = null;


            }
            else if (!scale&&!rotate&&(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.M)))
            {

                rotate = true;
                starthand = transform.position;



            }
            else if (!scale && !rotate && (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.P)))
            {
                scale = true;
                startDist = (transform.position - GameObject.Find("RightHandAnchor").transform.position).magnitude;
                if (startDist == 0)
                {
                    startDist = 0.0001f;
                }
                startScale = held.transform.localScale;
            }
        }
        else
        {
            rotate = false;
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

                //for menu
                if(hit.collider.gameObject.layer == 5)
                {
                    Vector3 spawnLocation = transform.TransformDirection(Vector3.forward);
                    spawnLocation.y = 0.5f;
                    btn = hit.collider.gameObject.GetComponent<Button>();
                    if(btn != null)
                    {
                        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.S))
                        {
                            if(btn.name == "chair_btn")
                            {
                                held = Instantiate(chair, transform.position + spawnLocation, chair.transform.rotation);
                                Select();
                            }
                            else if(btn.name == "tv_btn")
                            {
                                held = Instantiate(tv, transform.position + spawnLocation, tv.transform.rotation);
                                Select();
                            }
                            else if (btn.name == "desk_btn")
                            {
                                held = Instantiate(desk, transform.position + spawnLocation, desk.transform.rotation);
                                Select();
                            }
                            else if (btn.name == "cabinet_btn")
                            {
                                held = Instantiate(cabinet, transform.position + spawnLocation, cabinet.transform.rotation);
                                Select();
                            }
                            else if (btn.name == "locker_btn")
                            {
                                held = Instantiate(locker, transform.position + spawnLocation, locker.transform.rotation);
                                Select();
                            }
                            else if (btn.name == "whiteboard_btn")
                            {
                                held = Instantiate(whiteboard, transform.position + spawnLocation, whiteboard.transform.rotation);
                                Select();
                            }
                        }
                    }
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                actual.SetPosition(1, transform.position + (transform.TransformDirection(Vector3.forward) * hit.distance));
                if (hit.collider.gameObject.layer == 3)
                {

                    actual.startColor = Color.cyan;
                    actual.endColor = Color.cyan;
                    GameObject temp = hit.collider.gameObject;
                    Debug.Log("HERES THE NAME" + temp.name);
                    while (temp.name != "chair_1(Clone)" && temp.name != "desk_1(Clone)" && temp.name != "tv_1(Clone)" && temp.name != "whiteboard_1(Clone)" && temp.name != "cabinet_1(Clone)" && temp.name != "locker_1(Clone)")
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
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.S))
                    {

                        if (held != null)
                        {
                            held.GetComponent<Rigidbody>().isKinematic = true;
                            
                            Light[] children = held.GetComponentsInChildren<Light>();
                            foreach(Light l in children)
                            {
                                l.enabled = true;
                            }
                            //held.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);

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
            if (item == false && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("HELLO");
                spehere_selector = Instantiate(selector, transform.position, selector.transform.rotation);
               // spehere_selector.transform.parent = gameObject.transform;
                spehere_selector.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
            }
    }
}


