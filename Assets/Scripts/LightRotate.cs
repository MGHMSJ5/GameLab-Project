using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotate : MonoBehaviour

{

    public Transform target;
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

}
