using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public int RotOffset = 90;
    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //player pos from mouse pos
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; //find angle in degrees 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + RotOffset);
    }
}
