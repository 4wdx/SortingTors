using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "new AudioSettings", menuName = "Configs/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
        [field: SerializeField] public AudioMixerGroup MusicGroup { get; private set; }
        [field: SerializeField] public AudioMixerGroup SfxGroup { get; private set; }
    }
}