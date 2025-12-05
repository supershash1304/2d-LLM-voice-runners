using UnityEngine;
using EndlessRunner.Player;

public class RewardTrigger : MonoBehaviour
{
    // SUCCESS trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        var agent = other.GetComponent<VoiceRLAgent>();
        if (agent != null)
        {
            Debug.Log("SUCCESS");
            agent.AddReward(+1f);
            agent.EndEpisode();
        }
    }

    // FAIL collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        var agent = other.collider.GetComponent<VoiceRLAgent>();
        if (agent != null)
        {
            Debug.Log("FAIL");
            agent.AddReward(-1f);
            agent.EndEpisode();
        }
    }
}
