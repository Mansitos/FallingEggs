using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementUtilities
{
    public static Vector3 generateRandom2DPositionInRange(float xMin, float xMax, float yMin, float yMax, float z = 0)
    {
        Vector3 randomPoint = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), z);
        return randomPoint;
    }

    public static Trajectory generate2DTrajectoryInRange(Vector2 xLims, Vector2 yLims)
    {
        Vector3 startPoint = generateRandom2DPositionInRange(xLims.x, xLims.x, yLims.x, yLims.y);
        Vector3 endPoint   = generateRandom2DPositionInRange(xLims.y, xLims.y, yLims.x, yLims.y);
        Trajectory trajectory = new Trajectory(startPoint, endPoint);
        return trajectory;
    }
}
