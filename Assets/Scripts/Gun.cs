using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float fireRate = 0f;
    public float dmg = 10f;
    public LayerMask ToHit;

    public GameObject BulletTrailPrefab;


    private float TimeToFire = 0f;
    public Transform firePoint;

    
  
    // Update is called once per frame
    void Update()
    {
        //Shoot();
        if(fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    
    }

    void Shoot()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 FirePointPos = new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        RaycastHit2D Hit = Physics2D.Raycast(firePoint.transform.position, mousePos - FirePointPos, 100, ToHit);

        Effect();

        Debug.DrawLine(FirePointPos, (mousePos - FirePointPos) * 100, Color.cyan);
        if(Hit.collider != null)
        {
            Debug.DrawLine(FirePointPos, Hit.point, Color.red);
        }
    }

    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

}
