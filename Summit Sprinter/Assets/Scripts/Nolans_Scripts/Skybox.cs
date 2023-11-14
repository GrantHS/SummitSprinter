using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{

    public float Speed = 0.005f;
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, Speed);
    }
}
