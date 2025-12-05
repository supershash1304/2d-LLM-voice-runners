using UnityEngine;
using EndlessRunner.Player;
using EndlessRunner.Common;

public class WhisperVoiceCommands : MonoBehaviour
{
    public WhisperController whisper;
    public MicrophoneCapture mic;

    public float interval = 1.0f;

    private float timer;
    private PlayerManager playerManager;
    private VoiceRLAgent rlAgent;

    private void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();

        if (playerManager == null)
            Debug.LogWarning("[VOICE] No PlayerManager found in scene");
    }

    private void Update()
    {
        if (playerManager == null)
            return;

        if (playerManager.CurrentGameState != GameState.IN_GAME)
            return;

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
            rlAgent = FindAnyObjectByType<VoiceRLAgent>();

        if (rlAgent == null)
        {
            Debug.LogWarning("[VOICE] No VoiceRLAgent found");
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

        // Any phrase containing "jump" triggers one RL decision
        if (result.Contains("jump"))
        {
            Debug.Log("[VOICE] Triggering RL decision...");
            rlAgent.RequestDecision();
        }
    }
}
