using UnityEngine;
using System.Collections;
using EndlessRunner.Player;
using EndlessRunner.Common;

public class WhisperVoiceCommands : MonoBehaviour
{
    public WhisperController whisper;
    public MicrophoneCapture mic;

    private PlayerManager manager;
    private PlayerControllerVoiceMB voiceMB;

    private float timer;
    public float interval = 1.2f;

    private IEnumerator Start()
    {
        manager = FindAnyObjectByType<PlayerManager>();

        while (manager == null || manager.VoicePlayerMB == null)
            yield return null;

        voiceMB = manager.VoicePlayerMB;

        Debug.Log("[VOICE] Voice Player connected!");
    }

    private void Update()
    {
        if (manager == null)
            return;

        if (manager.CurrentGameState != GameState.IN_GAME)
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
        if (mic == null) return;

        var clip = mic.GetLastSecondClip();
        if (clip == null) return;

        string path = mic.SaveTempWav(clip);

        string result = await whisper.TranscribeAsync(path);
        if (string.IsNullOrEmpty(result))
        {
            Debug.LogWarning("[VOICE] Whisper empty");
            return;
        }

        result = result.ToLower();

        if (result.Contains("short jump"))
            voiceMB.TriggerJump(0.6f);
        else if (result.Contains("medium jump"))
            voiceMB.TriggerJump(1.0f);
        else if (result.Contains("high jump"))
            voiceMB.TriggerJump(1.4f);
        else if (result.Contains("jump"))
            voiceMB.TriggerJump(1.0f);
    }
}
