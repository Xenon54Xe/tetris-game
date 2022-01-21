using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject objectToCircle;
    public Vector3 circleRayon;
    public float speed = 2f;
    private bool doThing;

    public Vector3 observationPos;

    private void Start()
    {
        doThing = true;
    }

    private void Update()
    {
        if(GameManager.instance.GameState == GameStates.Waiting || GameManager.instance.GameState == GameStates.Controls || GameManager.instance.GameState == GameStates.ChangeTouche || GameManager.instance.GameState == GameStates.MainOptions || GameManager.instance.GameState == GameStates.Music || GameManager.instance.GameState == GameStates.Difficulty || GameManager.instance.GameState == GameStates.Score)
        {
            if (doThing)
            {
                doThing = false;
                transform.position = objectToCircle.transform.position + circleRayon;
            }
            MakeCircles(speed);
        }
        else
        {
            doThing = true;
        }/*
        if(GameManager.instance.GameState == GameStates.Options)
        {
            
        }*/
    }

    void MakeCircles(float speed)
    {
        transform.LookAt(objectToCircle.transform.position);
        transform.position = transform.position + transform.TransformDirection(new Vector3(0.05f, 0, 0) * speed);
    }

    public void GoToObservationSpot()
    {
        transform.position = observationPos;
        transform.LookAt(objectToCircle.transform.position);
        transform.Rotate(new Vector3(0, 0, 0));
    }
}
