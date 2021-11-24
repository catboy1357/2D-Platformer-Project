using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public Collider2D target;
    public Transform destination;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == target)
        col.transform.position = destination.position;
    }
}
