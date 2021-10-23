/*
 * This class is explicit multi-threaded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class LogManager : MonoBehaviour
{
    public Slider speedControlSlider;   // set in inspector

    //public Transform[] logs;

    //private Vector3[] posSet;

    private float speed = 1;            // need muxtex
    private float speedScale;
    private Mutex speed_mutex = new Mutex();


    private bool isGameEnd = false;        // either win or lose ==> end, need muxtex
    private Mutex isGameEnd_mutex = new Mutex();


    #region isGameEnd
    public bool GetIsGameEnd()
    {
        isGameEnd_mutex.WaitOne();
        bool isGameEnd_local = isGameEnd;
        isGameEnd_mutex.ReleaseMutex();

        return isGameEnd_local;
    }

    public void SetIsGameEndToTure()
    {
        isGameEnd_mutex.WaitOne();
        isGameEnd = true;
        isGameEnd_mutex.ReleaseMutex();
    }
    #endregion


    #region SpeedControl
    public void SliderUpdateSpeed()
    {
        speedScale = speedControlSlider.value;

        speed_mutex.WaitOne();
        speed *= speedScale;
        speed_mutex.ReleaseMutex();
    }
    #endregion


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
    }

    private void Update()
    {
        
    }
}
