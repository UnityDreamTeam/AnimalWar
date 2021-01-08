using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;
    [Tooltip("Wether to adjust speed of the player according to the current tile it walks on")]
    [SerializeField] bool useWeightAsSpeed = false;
    [Tooltip("True to use dijkstra, false to use BFS")]
    [SerializeField] bool useDijkstra = false;
    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    bool animated;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.
    IPathFinder<Vector3Int> pathFinder; //This property uses to choose path finding algorithm at runtime

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;

    //The usage of onValidate function is very effective instead of keep polling in while true loop
    private void OnValidate()
    {
        if (useDijkstra)
        {
            pathFinder = new Dijkstra<Vector3Int>();
        }
/*        else
        {
            pathFinder = new BFS<Vector3Int>();
        }*/
    }
    protected virtual void Start() {
        animated = true;
        //pathFinder = new BFS<Vector3Int>();//Initialize to BFS algorithm
        pathFinder = new Dijkstra<Vector3Int>();

        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
        
    }

    IEnumerator MoveTowardsTheTarget() {
        for (;;) {
            int currentWeight = 1; //Default weight
            if (useWeightAsSpeed)
            {
                //Convert transform.position to Vector3Int
                Vector3Int vec = new Vector3Int((int)Math.Floor(transform.position.x), 
                    (int)Math.Floor(transform.position.y), (int)Math.Floor(transform.position.z));
                TileBase playerTile = tilemap.GetTile(vec);//Get player current

                currentWeight = allowedTiles.GetWeight(playerTile);
            }

            //timeBetweenSteps = 1 / (speed / currentWeight);

            timeBetweenSteps = 1f;
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
            {
                if (animated)
                {
                    gameObject.GetComponentInChildren<Animator>().SetBool("Run", true);
                    animated = false;
                }
                MakeOneStepTowardsTheTarget();
            }
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;

        //A beautiful abstraction :)
        List<Vector3Int> shortestPath = pathFinder.GetPath(tilemapGraph, startNode, endNode, maxIterations);

        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count >= 2) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        } else {
            atTarget = true;
            gameObject.GetComponentInChildren<Animator>().SetBool("Run", false);
            animated = true;
        }
    }
}
