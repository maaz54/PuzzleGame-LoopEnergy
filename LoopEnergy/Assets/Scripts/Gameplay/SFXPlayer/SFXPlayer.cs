using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.SFX
{
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioRecord[] audiosRecord;

        public void PlayAudioClip(string audioName)
        {
            if (FindAudioClip(audioName, out AudioClip audioClip))
            {
                audioSource.PlayOneShot(audioClip);
            }
        }


        bool FindAudioClip(string particleName, out AudioClip audioClip)
        {
            AudioClip[] audioClips = audiosRecord.First(p => p.Name.Contains(particleName)).AudioClips;
            audioClip = audioClips[Random.Range(0, audioClips.Length)];
            return audioClips.Length > 0;
        }
    }

    [System.Serializable]
    public class AudioRecord
    {
        public AudioClip[] AudioClips;
        public string Name;
    }
}