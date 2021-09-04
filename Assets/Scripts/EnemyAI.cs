using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.AI;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour
{

    private GameObject target;
    public float UpdateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //path
    public Path path;

    //ai speed
    public float speed = 150f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool PathIsEnded = false;

    public float nextWaypointDistance = 3f;

    private int CurrentWaypoint = 0;

    private void Start()
    {
        target = FindClosestEnemy();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            return;
        }
        seeker.StartPath(transform.position,target.transform.position, OnPathComplete);
        
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //search cow
            FindClosestEnemy();
        }
        seeker.StartPath(transform.position, target.transform.position, OnPathComplete);

        yield return new WaitForSeconds(1f / UpdateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            CurrentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            //search cow
            return;
        }

        if (path == null)
        {
            return;
        }
        if (CurrentWaypoint >= path.vectorPath.Count)
        {
            if (PathIsEnded)
            {
                return;
            }
            PathIsEnded = true;
            return;
        }
        PathIsEnded = false;

        //Next Waypoint
        Vector3 dir = (path.vectorPath[CurrentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //move
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[CurrentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            CurrentWaypoint++;
            return;
        }
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Vaca");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
