using UnityEngine;
using OpenAI;
using System.Threading.Tasks;

public class WhisperController : MonoBehaviour
{
    public static WhisperController Instance;

    private OpenAIApi api;

    private void Awake()
    {
        Instance = this;
        api = new OpenAIApi();
    }

    public async Task<string> TranscribeAsync(string filePath)
    {
        Debug.Log("[WHISPER] Sending file: " + filePath);

        var request = new CreateAudioTranscriptionsRequest
        {
            File = filePath,
            Model = "whisper-1",
            ResponseFormat = "json",
            Prompt =
                "The user will only say: 'jump', 'high jump', 'medium jump', 'short jump'. " +
                "Transcribe strictly to one of these. If uncertain return 'jump'."
        };

        try
        {
            var response = await api.CreateAudioTranscription(request);
            return response.Text;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("[WHISPER ERROR] " + ex.Message);
            return null;
        }
    }
}
