using UnityEngine;
using EndlessRunner.Player;
using EndlessRunner.Common;

public class WhisperVoiceCommands : MonoBehaviour
{
    public WhisperController whisper;
    public MicrophoneCapture mic;

    public float interval = 0.3f;   

    private float timer;
    private float lastJumpTime = 0f;

    private PlayerManager playerManager;
    private VoiceRLAgent rlAgent;

    private const float silenceThreshold = 0.02f;

    private void OnEnable()
    {
        playerManager = null;
        rlAgent = null;
    }

    private void Update()
    {
        if (playerManager == null)
            playerManager = FindAnyObjectByType<PlayerManager>();

        if (playerManager == null)
            return;

        if (playerManager.CurrentGameState != GameState.IN_GAME)
            return;

        if (rlAgent == null)
            rlAgent = FindAnyObjectByType<VoiceRLAgent>();

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            ProcessVoice();
        }
    }

    private async void ProcessVoice()
    {
        if (mic == null || whisper == null)
            return;

        if (rlAgent == null)
        {
            Debug.LogWarning("[VOICE] RL Agent not found — waiting for spawn");
            return;
        }

        var clip = mic.GetLastSecondClip();
        if (clip == null)
            return;

        string path = mic.SaveTempWav(clip);
        string result = await whisper.TranscribeAsync(path);

        if (string.IsNullOrEmpty(result))
            return;

        result = result.ToLower();
        Debug.Log("[VOICE] Heard: " + result);

        if (result.Contains("jump"))
        {
            Debug.Log("[VOICE] Triggering RL decision...");
            rlAgent.RequestDecision();
        }
    }
}
