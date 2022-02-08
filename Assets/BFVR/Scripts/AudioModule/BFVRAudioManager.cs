using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BFVR.AudioModule
{
    /// <summary>
    /// Standard BFVR Audio Manager. Controls primary audio sources and primary audio mixer. (Does not destroy on load)
    /// </summary>
    public class BFVRAudioManager : MonoBehaviour
    {
        private static BFVRAudioManager _instance;
        public static BFVRAudioManager Instance { get { return _instance; } }

        [SerializeField] AudioMixer mainMix;

        [Space]
        [SerializeField] AudioSource sfx;
        [SerializeField] string sfxParameterName;

        [Space]
        [SerializeField] AudioSource music;
        [SerializeField] string musicParameterName;

        [Space]
        [SerializeField] AudioSource video;
        [SerializeField] string videoParameterName;

        [Space]
        [SerializeField] AudioSource voice;
        [SerializeField] string voiceOverVolume;

        [Space]
        [SerializeField] List<AudioClip> musicTrackList;

        int musicTrackIndex = -1;

        private void Awake()
        {
            if (!_instance && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            
        }

        #region Levels

        /// <summary>
        /// Sets sound fx audio level on audio mixer.
        /// </summary>
        /// <param name="sfxLvl"></param>
        void SetSFXLevel(float sfxLvl)
        {
            mainMix.SetFloat(sfxParameterName, sfxLvl);
        }

        /// <summary>
        /// Sets music audio level on audio mixer.
        /// </summary>
        /// <param name="musicLvl"></param>
        void SetMusicLevel(float musicLvl)
        {
            mainMix.SetFloat(musicParameterName, musicLvl);
        }

        /// <summary>
        /// Sets video audio level on audio mixer.
        /// </summary>
        /// <param name="videoLvl"></param>
        void SetVideoLevel(float videoLvl)
        {
            mainMix.SetFloat(videoParameterName, videoLvl);
        }

        /// <summary>
        /// Sets voice audio level on audio mixer.
        /// </summary>
        /// <param name="voiceLvl"></param>
        void SetVoiceLevel(float voiceLvl)
        {
            mainMix.SetFloat(voiceOverVolume, voiceLvl);
        }
        #endregion

        public void PlaySFX(AudioClip clip)
        {
            if (!sfx) return;
            if(sfx.isPlaying)
            {
                sfx.Stop();
            }

            sfx.clip = clip;
            sfx.Play();
        }

        public void StopSFX()
        {
            if (!sfx) return;
            sfx.Stop();
        }

        /// <summary>
        /// Mutes sound fx audio source.
        /// </summary>
        /// <returns> Returns true if muted. </returns>
        public bool ToggleMuteSFX()
        {
            if(!sfx)
            {
                return false;
            }

            if (sfx.mute) sfx.mute = false;
            else sfx.mute = true;

            return sfx.mute;
        }

        public void PlayVoiceClip(AudioClip clip)
        {
            if (!voice) return;
            if (voice.mute) return;

            if (voice.isPlaying)
            {
                voice.Stop();
            }

            voice.clip = clip;
            voice.Play();
        }

        public void StopVoiceClip()
        {
            if (!voice) return;
            voice.Stop();
        }

        /// <summary>
        /// Mutes/un-mutes voice audio source.
        /// </summary>
        /// <returns> Returns true if muted. </returns>
        public bool ToggleMuteVoice()
        {
            if (!voice)
            {
                return false;
            }

            if (voice.mute) voice.mute = false;
            else voice.mute = true;

            return voice.mute;
        }

        public void PlayMusic()
        {
            if (!music) return;
            if (music.mute) return;

            if (music.clip && musicTrackIndex > -1) music.Play();
            else
            {
                musicTrackIndex = 0;
                music.clip = musicTrackList[musicTrackIndex];
                music.Play();
            }
        }

        public void StopMusic()
        {
            if (!music) return;
            music.Stop();
        }

        public int GetMusicTrackIndex()
        {
            return musicTrackIndex;
        }

        /// <summary>
        /// Returns music track List<> count.
        /// </summary>
        public int GetMusicTrackCount()
        {
            return musicTrackList.Count;
        }

        public void PlayNextTrack()
        {
            if (!music) return;

            musicTrackIndex++;
            if(musicTrackIndex >= musicTrackList.Count)
            {
                musicTrackIndex = 0;
            }

            music.clip = musicTrackList[musicTrackIndex];
            PlayMusic();
        }

        public void PlayPreviousTrack()
        {
            if (!music) return;

            musicTrackIndex--;
            if(musicTrackIndex < 0)
            {
                musicTrackIndex = musicTrackList.Count - 1;
            }

            music.clip = musicTrackList[musicTrackIndex];
            PlayMusic();
        }

        /// <summary>
        /// Mutes/un-mutes music audio source.
        /// </summary>
        /// <returns> Returns true if muted. </returns>
        public bool ToggleMuteMusic()
        {
            if (!music)
            {
                return false;
            }
            if (music.mute) music.mute = false;
            else music.mute = true;

            return music.mute;
        }
    }
}
