using UnityEngine;
using System.IO;

public class MicrophoneCapture : MonoBehaviour
{
    public int sampleRate = 44100;

    private AudioClip micClip;
    private string device = null;

    void Start()
    {
        StartContinuousMic();
    }

    public void StartContinuousMic()
    {
        if (Microphone.IsRecording(device))
            return;

        Debug.Log("[MIC] Starting microphone...");
        micClip = Microphone.Start(device, true, 5, sampleRate);
    }

    public AudioClip GetLastSecondClip()
    {
        if (micClip == null)
            return null;

        int micPosition = Microphone.GetPosition(device);
        if (micPosition <= 0)
            return null;

        int samplesToRead = sampleRate * 1;
        float[] allSamples = new float[micClip.samples];
        micClip.GetData(allSamples, 0);

        float[] segment = new float[samplesToRead];
        int startPos = Mathf.Clamp(micPosition - samplesToRead, 0, allSamples.Length - 1);

        for (int i = 0; i < samplesToRead && startPos + i < allSamples.Length; i++)
            segment[i] = allSamples[startPos + i];

        float avg = 0f;
        for (int i = 0; i < segment.Length; i++)
            avg += Mathf.Abs(segment[i]);
        avg /= segment.Length;

        Debug.Log("[MIC] Volume=" + avg);

        AudioClip newClip = AudioClip.Create("micSegment", samplesToRead, 1, sampleRate, false);
        newClip.SetData(segment, 0);

        return newClip;
    }

    public string SaveTempWav(AudioClip clip)
    {
        if (clip == null)
            return null;

        string path = Path.Combine(Application.persistentDataPath, "temp_voice.wav");
        SaveWav(path, clip);
        return path;
    }

    void SaveWav(string path, AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        using (var file = new FileStream(path, FileMode.Create))
        using (var writer = new BinaryWriter(file))
        {
            int sampleCount = samples.Length;
            int byteCount = sampleCount * 2;
            int headerSize = 44;

            writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
            writer.Write(byteCount + headerSize - 8);
            writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));
            writer.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
            writer.Write(16);
            writer.Write((short)1);
            writer.Write((short)1);
            writer.Write(sampleRate);
            writer.Write(sampleRate * 2);
            writer.Write((short)2);
            writer.Write((short)16);
            writer.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            writer.Write(byteCount);

            foreach (float s in samples)
            {
                short val = (short)Mathf.Clamp(s * short.MaxValue, short.MinValue, short.MaxValue);
                writer.Write(val);
            }
        }
    }
}
