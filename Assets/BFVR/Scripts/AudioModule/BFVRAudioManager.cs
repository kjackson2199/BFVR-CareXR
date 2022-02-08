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

        const float levelMin = 0.0001f;
        const float levelMax = 1.0f;

        const float levelDbMin = -80;
        const float levelDbMax = 20;

        [SerializeField] AudioMixer mainMix;

        [Space]
        [SerializeField] AudioSource sfx;
        [SerializeField] string sfxParameterName;
        [Range(0.0001f, 1)] public float defaultSfxLevel = .8f;

        [Space]
        [SerializeField] AudioSource music;
        [SerializeField] string musicParameterName;
        [SerializeField] bool playMusicBehindVideo = true;
        [SerializeField] [Range(.0001f, .5f)] float musicLevelBehindVideo = .25f;
        [Range(0.0001f, 1)] public float defaultMusicLevel = .8f;

        [Space]
        [SerializeField] AudioSource video;
        [SerializeField] string videoParameterName;
        [Range(0.0001f, 1)] public float defaultVideoLevel = .8f;

        [Space]
        [SerializeField] AudioSource voice;
        [SerializeField] string voiceOverVolume;
        [Range(0.0001f, 1)] public float defaultVoiceLevel = .8f;

        [Space]
        [SerializeField] List<AudioClip> musicTrackList;

        int musicTrackIndex = -1;
        bool musicSmoothTransitionActive;

        Coroutine musicSmoothTranstionCoroutine;

        private void Awake()
        {
            if (_instance != null && _instance != this)
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

        #region Levels

        /// <summary>
        /// Sets sound fx audio level on audio mixer.
        /// </summary>
        /// <param name="sfxLvl"></param>
        public void SetSFXLevel(float sfxLvl)
        {
            sfxLvl = Mathf.Clamp(sfxLvl, levelMin, levelMax);
            mainMix.SetFloat(sfxParameterName, Mathf.Log10(sfxLvl) * 20);
        }

        /// <summary>
        /// Gets sound fx audio level from audio mixer
        /// </summary>
        /// <returns>Returns 0.0001 - 1</returns>
        public float GetSFXLevel()
        {
            float value;
            mainMix.GetFloat(sfxParameterName, out value);
            return BFVRMathExtentions.Remap(value, levelDbMin, levelDbMax, levelMin, levelMax);
        }

        public void SetSFXLevelToDefault()
        {
            SetSFXLevel(defaultSfxLevel);
        }

        /// <summary>
        /// Sets music audio level on audio mixer.
        /// </summary>
        /// <param name="musicLvl"> Target Music Level (0.001 - 1) </param>
        /// <param name="smooth"> Use smooth transition. Defaults to true. </param>
        /// <param name="time"> Transtion timing. Defaults to 2. </param>
        public void SetMusicLevel(float musicLvl, bool smooth = true, float time = 2)
        {
            musicLvl = Mathf.Clamp(musicLvl, levelMin, levelMax);
            if(smooth && !musicSmoothTransitionActive)
            {
                musicSmoothTranstionCoroutine = StartCoroutine(SetMusicLevelSmooth(musicLvl, time));
            }
            else if(smooth && musicSmoothTransitionActive)
            {
                StopCoroutine(musicSmoothTranstionCoroutine);
                musicSmoothTranstionCoroutine = StartCoroutine(SetMusicLevelSmooth(musicLvl, time));
            }
            else
            {
                mainMix.SetFloat(musicParameterName, Mathf.Log10(musicLvl) * 20);
            }
        }

        public void SetMusicLevelToDefault()
        {
            SetMusicLevel(defaultMusicLevel);
        }

        IEnumerator SetMusicLevelSmooth(float musicLvl, float time)
        {
            musicSmoothTransitionActive = true;

            float timeElapsed = 0;
            float currentMusicLvl = GetMusicLevel();
            float targetMusicLvl = Mathf.Log10(musicLvl) * 20;

            while (timeElapsed < time)
            {
                mainMix.SetFloat(musicParameterName, Mathf.Lerp(currentMusicLvl, targetMusicLvl, timeElapsed / time));
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            musicSmoothTransitionActive = false;
        }

        /// <summary>
        /// Gets music audio level from audio mixer
        /// </summary>
        /// <returns>Returns 0.0001 - 1</returns>
        public float GetMusicLevel()
        {
            float value;
            mainMix.GetFloat(musicParameterName, out value);

            return value;
        }

        /// <summary>
        /// Sets video audio level on audio mixer.
        /// </summary>
        /// <param name="videoLvl"></param>
        public void SetVideoLevel(float videoLvl)
        {
            videoLvl = Mathf.Clamp(videoLvl, levelMin, levelMax);
            mainMix.SetFloat(videoParameterName, Mathf.Log10(videoLvl) * 20);
        }

        /// <summary>
        /// Gets video audio level from audio mixer
        /// </summary>
        /// <returns>Returns 0.0001 - 1</returns>
        public float GetVideoLevel()
        {
            float value;
            mainMix.GetFloat(videoParameterName, out value);

            return BFVRMathExtentions.Remap(value, levelDbMin, levelDbMax, levelMin, levelMax);
        }

        public void SetVideoLevelToDefault()
        {
            SetVideoLevel(defaultVideoLevel);
        }

        /// <summary>
        /// Sets voice audio level on audio mixer.
        /// </summary>
        /// <param name="voiceLvl"></param>
        public void SetVoiceLevel(float voiceLvl)
        {
            voiceLvl = Mathf.Clamp(voiceLvl, levelMin, levelMax);
            mainMix.SetFloat(voiceOverVolume, Mathf.Log10(voiceLvl) * 20);
        }

        /// <summary>
        /// Gets voice audio level from audio mixer
        /// </summary>
        /// <returns>Returns 0.0001 - 1</returns>
        public float GetVoiceLevel()
        {
            float value;
            mainMix.GetFloat(voiceOverVolume, out value);

            return BFVRMathExtentions.Remap(value, levelDbMin, levelDbMax, levelMin, levelMax);
        }

        public void SetVoiceLevelToDefault()
        {
            SetVoiceLevel(defaultVoiceLevel);
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
