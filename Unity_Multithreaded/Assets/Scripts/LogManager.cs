/*
 * This class is explicit multi-threaded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class LogManager : MonoBehaviour
{
    public Transform[] logs;

    private Vector3[] posSet;

    private static bool isGameEnd = false;    //win or lose, need muxtex

    public bool GetIsGameEnd()
    {
        return isGameEnd;
    }

    public void SetIsGameEndToTure()
    {
        isGameEnd = true;
    }

    private void Start()
    {
        //pos = log.position;

        //Thread threadInstance = new Thread(new ThreadStart(threadPrint));

        //threadInstance.Start();
    }

    private void threadPrint()
    {
        //int counter = 0;

        //while(counter < 10000000)
        //{
        //    counter++;
        //    pos = pos + new Vector3(0, 0, 0.5f);

        //    Thread.Sleep(100);
        //}

        //Debug.Log("print in thread");
    }

    private void Update()
    {
        
    }
}
