using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public Transform log;

    private void Update()
    {
        Vector3 pos = new Vector3(log.position.x, log.position.y, log.position.z + Time.deltaTime * 5);
        log.transform.position = pos;
    }
}
