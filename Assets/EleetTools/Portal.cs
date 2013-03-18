using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PortalTypes
{
    ToLevel,
    ToObject
}

public class Portal : MonoBehaviour 
{
    public PortalTypes type = PortalTypes.ToObject;

    [HideInInspector]
    public List<string> portalTags = new List<string>();

    // To Object Properties
    [HideInInspector]
    public Transform destinationObject;

    [HideInInspector]
    public bool isOneShot = false;

    // To Level Properties
    [HideInInspector]
    public string destinationSceneName = string.Empty;

    void OnTriggerEnter(Collider other)
    {
        bool willPort = false;
        foreach (string tag in portalTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                willPort = true;
                break;
            }
        }

        if (willPort)
        {
            switch(type)
            {
                case PortalTypes.ToLevel:
                    if (destinationSceneName != string.Empty)
                        Application.LoadLevel(destinationSceneName);
                    break;
                case PortalTypes.ToObject:
                    if (destinationObject != null)
                    {
                        Vector3 newLocation = destinationObject.position;
                        Rigidbody rbody = other.gameObject.GetComponent<Rigidbody>();
                        if (rbody != null)
                            rbody.velocity = Vector3.zero;
                        other.transform.position = newLocation;
                        if (isOneShot)
                            Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
