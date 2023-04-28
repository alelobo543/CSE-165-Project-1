using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour
{
    public RayCast ray;
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) 
    {
        if(col.gameObject.layer == 3)
        {
            Debug.Log("HELLO");
            GameObject temp = col.gameObject;
            while (temp.name != "chair_1(Clone)" && temp.name != "desk_1(Clone)" && temp.name != "tv_1(Clone)" && temp.name != "whiteboard_1(Clone)" && temp.name != "cabinet_1(Clone)" && temp.name != "locker_1(Clone)")
            {
                temp = temp.transform.parent.gameObject;
                Debug.Log(temp.name);
            }
            transform.parent.GetComponent<RayCast>().SelectorCollision(this, temp);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
