using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")     //check if it is the frog GameObject  ==> other.transform.parent is the Player GameObject
        {
            other.transform.parent.parent = this.transform;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Player enter trigger.");
    //        other.transform.parent.parent = this.transform;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent.parent = null;
        }
    }
}
