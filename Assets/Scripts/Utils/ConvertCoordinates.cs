using UnityEngine;

public static class CoordinatesUtil
{
    public static float convert(Vector3 position)
    {
        return position.x + position.y * 100;
    }
}
