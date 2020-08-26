using UnityEngine;
using System.Collections;

//простой мов компонент, годится если дороги имеют угловые изгибы, в случае дуговых, лушче использовать A*Pathfinding
public class EnemyMove
{

    [HideInInspector]
    GameObject[] waypoints;
    GameObject enemy;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    
    public EnemyMove(GameObject[] waypoints, GameObject enemy)
    {     
        lastWaypointSwitchTime = Time.time;
        this.waypoints = waypoints;
        this.enemy = enemy;
        this.lastWaypointSwitchTime = Time.time;
    }
    

    public void Move()
    {

        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, endPosition, speed * Time.deltaTime);

        if (enemy.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
            }

        }
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            enemy.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
