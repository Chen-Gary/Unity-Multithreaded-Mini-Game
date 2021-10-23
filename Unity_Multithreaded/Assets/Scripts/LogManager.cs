/*
 * This class is explicit multi-threaded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
using System.Threading;


public class LogManager : MonoBehaviour
{
    private float MAX_POSZ = 26.73004f;
    private float MIN_POSZ = -26.76996f;

    public static LogManager _instance; // make LogManager a singleton

    public Slider speedControlSlider;   // set in inspector
    public Transform[] logs;            // set in inspector

    int logCount;

    private Vector3[] logsPos;          // need muxtex
    private Mutex[] logsPos_mutex;

    private const float speedBase = 0.1f;
    private float speed = speedBase;       // need muxtex
    private float speedScale;
    private Mutex speed_mutex = new Mutex();


    private bool isGameEnd = false;        // either win or lose ==> end, need muxtex
    private Mutex isGameEnd_mutex = new Mutex();

    private Thread[] logThreads;


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
        float speed_local;

        // speed up
        if (speedScale > 0)
        {
            speed_local = speedBase * (speedScale + 1);
        }
        // speed down
        else if (speedScale < 0)
        {
            speed_local = speedBase / (-speedScale + 1);
        }
        // unchanged
        else    //speedScale == 0
        {
            speed_local = speedBase;
        }


        // update global speed
        speed_mutex.WaitOne();
        speed = speed_local;
        speed_mutex.ReleaseMutex();
    }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()    // main function
    {
        // init logsPos and logsPos_mutex
        logCount = logs.Length;

        // randomize logs position
        for (int i = 0; i < logCount; i++)
        {
            logs[i].position = new Vector3(logs[i].position.x, logs[i].position.y, Random.Range(MIN_POSZ, MAX_POSZ));
        }

        logsPos = new Vector3[logCount];
        logsPos_mutex = new Mutex[logCount];

        for (int i = 0; i < logCount; i++)
        {
            logsPos[i] = logs[i].position;

            logsPos_mutex[i] = new Mutex();
        }


        // create threads for each log to control the movement
        // 1. init and start
        logThreads = new Thread[logCount];
        for (int i = 0; i < logCount; i++)
        {
            logThreads[i] = new Thread(new ThreadStart(logPosUpdate));

            logThreads[i].Name = i.ToString();        //thread id as string to Thread.Name

            logThreads[i].Start();
        }

        // 2. join (we cannot join in Start(), otherwise the game will not actually start!)
        // Join all side threads in GameStatusManager.ManuallyQuitGame()
        //JoinAllSideThreads();

    }

    private void logPosUpdate()
    {
        int logIndex = int.Parse(Thread.CurrentThread.Name);

        while (!GetIsGameEnd())
        {
            logsPos_mutex[logIndex].WaitOne();

            if (logIndex % 2 == 0) 
                logsPos[logIndex] += new Vector3(0, 0, speed);
            else 
                logsPos[logIndex] -= new Vector3(0, 0, speed);

            // reset pos if needed
            if (logsPos[logIndex].z > MAX_POSZ)
                logsPos[logIndex].z = MIN_POSZ + 0.01f;
            else if (logsPos[logIndex].z < MIN_POSZ)
                logsPos[logIndex].z = MAX_POSZ - 0.01f;

            logsPos_mutex[logIndex].ReleaseMutex();

            Thread.Sleep(10);
        }

    }


    public void JoinAllSideThreads()
    {
        for (int i = 0; i < logCount; i++)
        {
            logThreads[i].Join();
        }

        Debug.Log("Join all done");
    }


    private void Update()
    {
        // since we cannot update transform.position in side thread,
        // we update their position in Update()

        // go through all logs
        for (int i = 0; i < logCount; i++)
        {
            logsPos_mutex[i].WaitOne();
            logs[i].position = logsPos[i];
            logsPos_mutex[i].ReleaseMutex();
        }
    }
}
