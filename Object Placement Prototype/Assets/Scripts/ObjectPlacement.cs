using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ObjectPlacement : MonoBehaviour
{
    [Header("Object Prefabs")]
    public GameObject block;
    public GameObject currentObject;

    [Header("Raycast Settings")]
    public float distance;
    public LayerMask layerMask;

    [Header("Object Placement Details")]
    public bool currentlyPlacingObject = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetermineGroundAngle();

        
    }

    private void FixedUpdate()
    {
        if (currentlyPlacingObject)
        {

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance))
            {
                currentObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                currentObject.transform.eulerAngles = hit.normal;
            }
        }
    }

    private void DetermineGroundAngle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentlyPlacingObject)
            {
                currentlyPlacingObject = false;
                currentObject = null;
                currentObject.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                return;
            }

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Firing");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance) )
            {
                Debug.Log(hit.collider.gameObject.name);


                GameObject item = Instantiate(block, transform.position, Quaternion.identity);
                item.transform.position = hit.point;
                item.transform.eulerAngles = hit.normal;
                item.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;

                currentObject = item;
                currentlyPlacingObject = true;

                Debug.Log("Placed Object");
            }
        }
    }
}
