using System.Collections;
using System.IO;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Networking;

namespace _scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource _as;
        private bool _choosing;

        private string[] _paths;
        private string _songPath;

        private bool _canPlay = true;

        // Set your fallback URL here
        private string _fallbackSongUrl = "https://6hundred9.github.io/songs/Original%20Tetris%20theme%20(Tetris%20Soundtrack)%20(320kbps).wav";

        [BurstCompile]
        void Start()
        {
            _as = GetComponent<AudioSource>();
            _songPath = Path.Combine(Application.dataPath, "Songs");

            if (!Directory.Exists(_songPath))
            {
                Directory.CreateDirectory(_songPath);
            }

            _paths = Directory.GetFiles(_songPath, "*.wav");

            if (_paths.Length < 1)
            {
                Debug.LogWarning("No songs found in the Songs folder. Fetching song from URL.");
                StartCoroutine(DownloadAudioFromUrl(_fallbackSongUrl));
            }
        }

        [BurstCompile]
        void Update()
        {
            if (_paths.Length < 1) return;
            if (!_as.isPlaying && !_choosing && _canPlay)
            {
                _choosing = true;
                StartCoroutine(LoadAudio(_paths[Random.Range(0, _paths.Length)]));
            }
        }

        [BurstCompile]
        public void ToggleSong()
        {
            _canPlay = !_canPlay;
            
            if (_as.isPlaying) {_as.Pause(); _as.time = 0; }
            else _as.UnPause();
        }

        [BurstCompile]
        IEnumerator LoadAudio(string filePath)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + filePath, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error loading audio: " + www.error);
                }
                else
                {
                    AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                    Debug.Log("Loaded AudioClip: " + audioClip.name);

                    _as.clip = audioClip;
                    _as.Play();
                }
            }

            _choosing = false;
        }

        [BurstCompile]
        IEnumerator DownloadAudioFromUrl(string url)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error downloading audio from URL: " + www.error);
                }
                else
                {
                    AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                    Debug.Log("Loaded AudioClip from URL: " + audioClip.name);

                    // Optionally save the audio clip to the local Songs folder
                    string localPath = Path.Combine(_songPath, "DownloadedSong.wav");
                    File.WriteAllBytes(localPath, www.downloadHandler.data);

                    _paths = Directory.GetFiles(_songPath, "*.wav");

                    // Load the audio from the saved file
                    yield return LoadAudio(localPath);
                }
            }
        }
    }
}
