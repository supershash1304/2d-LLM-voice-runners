using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

namespace EndlessRunner.Player
{
    public class VoiceRLAgent : Agent
    {
        public PlayerControllerVoiceMB voiceWrapper;

        private float distToObstacle;
        private float playerHeight;
        private bool grounded;

        public void SetEnvironment(float dist, float height, bool g)
        {
            distToObstacle = dist;
            playerHeight = height;
            grounded = g;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(distToObstacle);
            sensor.AddObservation(playerHeight);
            sensor.AddObservation(grounded ? 1f : 0f);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            if (voiceWrapper == null)
                return;

            float jump = Mathf.Clamp01(actions.ContinuousActions[0]);
            voiceWrapper.TriggerJump(jump);
        }

        public override void OnEpisodeBegin()
        {
            voiceWrapper?.ResetForRLTraining();
        }

        // 🚫 NO SPACE INPUT ALLOWED
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            // Leave empty! RL decides everything.
        }
    }
}
