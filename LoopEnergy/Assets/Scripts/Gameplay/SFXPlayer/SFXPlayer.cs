using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.SFX
{
    /// <summary>
    /// Manages the playback of sound effects.
    /// </summary>
    public class SFXPlayer : MonoBehaviour
    {
        // The audio source component used to play sound effects
        [SerializeField] AudioSource audioSource;

        // Array of audio records containing sound effects
        [SerializeField] AudioRecord[] audiosRecord;

        /// <summary>
        /// Plays an audio clip with the specified name.
        /// </summary>
        public void PlayAudioClip(string audioName)
        {
            if (FindAudioClip(audioName, out AudioClip audioClip))
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        /// <summary>
        /// Finds an audio clip by name.
        /// </summary>
        bool FindAudioClip(string particleName, out AudioClip audioClip)
        {
            AudioClip[] audioClips = audiosRecord.First(p => p.Name.Contains(particleName)).AudioClips;
            audioClip = audioClips[Random.Range(0, audioClips.Length)];
            return audioClips.Length > 0;
        }
    }


    /// <summary>
    /// Represents a record of audio clips with a specific name.
    /// </summary>
    [System.Serializable]
    public class AudioRecord
    {
        public AudioClip[] AudioClips;
        public string Name;
    }
}