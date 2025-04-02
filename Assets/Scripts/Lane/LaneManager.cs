using UnityEngine;

public class LaneManager
{
    Lane[] lanes;

    public Lane this[int index] => lanes[index];

    public LaneManager(int laneCount)
    {
        lanes = new Lane[laneCount];
        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i] = new Lane(i);
        }
    }
}
