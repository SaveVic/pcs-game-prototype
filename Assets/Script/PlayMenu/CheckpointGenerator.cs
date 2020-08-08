using System.Collections.Generic;
using UnityEngine;

public class CheckpointGenerator : MonoBehaviour
{
    Vector3 checkpoint1, checkpoint2;
    List<Vector3> listPosition;
    int index1, index2;
    Vector3 offset1, offset2;

    public static CheckpointGenerator instance;
    public GameObject checkpointObject1, checkpointObject2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        offset1 = new Vector3(.25f, 0, 0);
        offset2 = new Vector3(-.25f, 0, 0);
        listPosition = new List<Vector3>();
        int temp = Random.Range(0, 16);
        Vector3 checkpoint = new Vector3(0.5f * temp - 3.75f, 2, 0);
        listPosition.Add(checkpoint);
        index1 = index2 = 0;
        if(GameControl.instance.data.whichMode == 1)
        {
            Instantiate(checkpointObject2, listPosition[index2] + offset2, Quaternion.identity);
            ArrowController.instance.SetTarget(2, listPosition[index2] + offset2);
        }
        Instantiate(checkpointObject1, listPosition[index1] + offset1, Quaternion.identity);
        ArrowController.instance.SetTarget(1, listPosition[index1] + offset1);
    }

    public void UpdateCheckpoint(int which)
    {
        switch (which)
        {
            case 1:
                if (index1 == listPosition.Count - 1)
                {
                    int temp = Random.Range(0, 16);
                    listPosition.Add(new Vector3(0.5f * temp - 3.75f, listPosition[index1].y + 4, 0));
                }
                index1++;
                Instantiate(checkpointObject1, listPosition[index1] + offset1, Quaternion.identity);
                ArrowController.instance.SetTarget(1, listPosition[index1] + offset1);
                break;
            case 2:
                if (index2 == listPosition.Count - 1)
                {
                    int temp = Random.Range(0, 16);
                    listPosition.Add(new Vector3(0.5f * temp - 3.75f, listPosition[index2].y + 4, 0));
                }
                index2++;
                Instantiate(checkpointObject2, listPosition[index2] + offset2, Quaternion.identity);
                ArrowController.instance.SetTarget(2, listPosition[index2] + offset2);
                break;
            default:
                break;
        }
    }

    //public bool IsOutsideView()
    //{
    //    if (checkpoint.y > ObstacleGenerator.viewObstacleAbove
    //        || checkpoint.y < ObstacleGenerator.viewObstacleBelow) return true;
    //    else return false;
    //}
}
