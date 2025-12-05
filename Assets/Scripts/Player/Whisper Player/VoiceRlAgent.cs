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
        private float height;
        private bool grounded;

        public void SetEnvironment(float dist, float h, bool g)
        {
            distToObstacle = dist;
            height = h;
            grounded = g;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(distToObstacle);
            sensor.AddObservation(height);
            sensor.AddObservation(grounded ? 1f : 0f);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            float act = actions.ContinuousActions[0];

            Debug.Log($"[RL] ActionReceived. act={act}, grounded={grounded}, wrapper={(voiceWrapper != null)}");

            if (voiceWrapper == null)
            {
                Debug.LogWarning("[RL] voiceWrapper is NULL — cannot jump!");
                return;
            }

            // 🔥 ALWAYS jump if grounded (voice-triggered RL only)
            if (grounded)
            {
                float jumpStrength = Mathf.Clamp01(act);
                Debug.Log("[RL] Triggering jump! strength=" + jumpStrength);
                voiceWrapper.TriggerJump(jumpStrength);
            }
        }

        public override void OnEpisodeBegin()
        {
            voiceWrapper?.ResetForRLTraining();
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var c = actionsOut.ContinuousActions;
            c[0] = 0f;
        }
    }
}
