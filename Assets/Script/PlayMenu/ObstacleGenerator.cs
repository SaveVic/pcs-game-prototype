using System.Collections.Generic;
using UnityEngine;

class SelectSubSet
{
    List<int> indeks;

    public SelectSubSet(int total)
    {
        indeks = new List<int>();
        for (int i = 0; i < total; i++)
        {
            indeks.Add(i);
        }
    }

    public int[] CreateSubSet(int k)
    {
        List<int> t = new List<int>(indeks);
        int[] temp = new int[k];
        int x;
        for (int i = 0; i < k; i++)
        {
            x = Random.Range(0, t.Count - i);
            temp[i] = indeks[x];
            indeks.RemoveAt(x);
        }
        indeks = t;
        return temp;
    }
}

class ObstacleObjectData
{
    public enum ObjectType
    {
        Block, Spike, Evil
    }

    public Vector3 position;
    public ObjectType type;

    public ObstacleObjectData(Vector3 position, ObjectType type)
    {
        this.position = position;
        this.type = type;
    }
}

class ObstacleLine
{
    float linePosition;
    public ObstacleObjectData[] obstacle;

    public ObstacleLine(float linePosition, int count)
    {
        this.linePosition = linePosition;
        obstacle = new ObstacleObjectData[count];
        SelectSubSet set = new SelectSubSet(8);
        int[] obsX = set.CreateSubSet(count);
        int evilPos = Random.Range(0, (2 * count) / 5);
        int chance = Random.Range(1, 101);
        for (int i = 0; i < (2 * count) / 5; i++)
        {
            ObstacleObjectData.ObjectType t = (i == evilPos && chance <= 40) ? ObstacleObjectData.ObjectType.Evil : ObstacleObjectData.ObjectType.Spike;
            obstacle[i] = new ObstacleObjectData(new Vector3(obsX[i] - 3.5f, linePosition, 0), t);
        }
        for (int i = (2 * count) / 5; i < count; i++)
        {
            obstacle[i] = new ObstacleObjectData(new Vector3(obsX[i] - 3.5f, linePosition, 0), ObstacleObjectData.ObjectType.Block);
        }
    }

    public ObstacleLine(float linePosition, int count, ObstacleObjectData.ObjectType type)
    {
        this.linePosition = linePosition;
        obstacle = new ObstacleObjectData[count];
        SelectSubSet set = new SelectSubSet(8);
        int[] obsX = set.CreateSubSet(count);
        for (int i = 0; i < count; i++)
        {
            obstacle[i] = new ObstacleObjectData(new Vector3(obsX[i] - 3.5f, linePosition, 0), type);
        }
    }
}

public class ObstacleGenerator : MonoBehaviour
{
    public Camera cam2;
    public ScriptableData data;
    public GameObject blockObject, spikeObject, evilObject;
    public static float viewObstacleAbove, viewObstacleBelow;

    float supremum;
    int supObstacle, infObstacle;
    List<ObstacleLine> info;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        viewObstacleAbove = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane)).y + 4f;
        viewObstacleBelow = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y - 4f;
    }

    // Use this for initialization
    void Start()
    {
        supObstacle = 0;
        infObstacle = -2;
        supremum = 0f;
        info = new List<ObstacleLine>();
        info.Add(new ObstacleLine(-2, 8, ObstacleObjectData.ObjectType.Block));
        Generate(0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateView();
        if (cam.transform.position.y + 8 >= supremum || cam2.transform.position.y + 8 >= supremum)
        {
            supremum += 4f;
            info.Add(new ObstacleLine(supremum, 5));
        }

        if (viewObstacleAbove > supObstacle + 4f)
        {
            supObstacle += 4;
            if (supObstacle >= 0)
            {
                Generate(supObstacle / 4);
            }
        }
        else if (viewObstacleAbove < supObstacle)
        {
            supObstacle -= 4;
        }

        if (viewObstacleBelow < infObstacle - 4f && infObstacle >= 8)
        {
            infObstacle -= 4;
            Generate(infObstacle / 4);
        }
        else if (viewObstacleBelow < -2f && infObstacle == 4)
        {
            infObstacle = -2;
            Generate(0);
        }
        else if (viewObstacleBelow > infObstacle && infObstacle >= 4)
        {
            infObstacle += 4;
        }
        else if(viewObstacleBelow > infObstacle && infObstacle == -2)
        {
            infObstacle = 4;
        }
    }

    void UpdateView()
    {
        if (data.whichMode == 1)
        {
            viewObstacleAbove = Mathf.Max(cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane)).y + 4f, cam2.ViewportToWorldPoint(new Vector3(1, 1, cam2.nearClipPlane)).y + 4f);
            viewObstacleBelow = Mathf.Min(cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y - 4f, cam2.ViewportToWorldPoint(new Vector3(0, 0, cam2.nearClipPlane)).y - 4f);
        }
        else
        {
            viewObstacleAbove = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane)).y + 4f;
            viewObstacleBelow = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y - 4f;
        }
        
    }

    void Generate(int pos)
    {
        GameObject obj;
        for (int i = 0; i < info[pos].obstacle.Length; i++)
        {
            switch (info[pos].obstacle[i].type)
            {
                case ObstacleObjectData.ObjectType.Spike:
                    obj = spikeObject;
                    break;
                case ObstacleObjectData.ObjectType.Evil:
                    obj = evilObject;
                    break;
                default:
                    obj = blockObject;
                    break;
            }
            Instantiate(obj, info[pos].obstacle[i].position, Quaternion.identity);
        }
    }
}