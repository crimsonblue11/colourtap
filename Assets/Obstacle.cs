using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float rotation_speed = 0.2f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotation_speed));
    }
}
