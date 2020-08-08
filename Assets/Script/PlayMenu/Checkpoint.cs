using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int whichCheckpoint;

    private void RemoveCheckpoint()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && whichCheckpoint == 1)
        {
            Debug.Log("Destroy 1");
            CheckpointGenerator.instance.UpdateCheckpoint(1);
            ScoreController.instance.AddScore(1);
            RemoveCheckpoint();
        }
        else if (collision.gameObject.layer == 12 && whichCheckpoint == 2)
        {
            Debug.Log("Destroy 2");
            CheckpointGenerator.instance.UpdateCheckpoint(2);
            ScoreController.instance.AddScore(2);
            RemoveCheckpoint();
        }
    }
}
