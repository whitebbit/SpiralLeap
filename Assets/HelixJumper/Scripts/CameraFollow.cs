using System.Collections;
using System.Collections.Generic;
using HelixJumper.Scripts;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Ball CurrentBall;

    public float _offset;

    private void Update()
    {
        if (CurrentBall.isFly)
        {
            transform.position = new Vector3(0, CurrentBall.gameObject.transform.position.y + _offset, -8f);
        }
    }
}