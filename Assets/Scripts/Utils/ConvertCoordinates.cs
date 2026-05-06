using UnityEngine;

public static class CoordinatesUtil
{
    //Converts coordinates to a float, so that coordinates can be ordered by y first and then x
    public static float convert(Vector3 position)
    {
        //TODO Maybe use integers with Vector3Int?
        //If map is especially big we can make the multiplier bigger
        return position.x + position.y * 300;
    }
}
