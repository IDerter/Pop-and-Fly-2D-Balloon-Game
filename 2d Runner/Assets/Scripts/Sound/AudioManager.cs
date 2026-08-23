using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

 public class AudioManager : SingletonBase<AudioManager>
    {
        [Header("Settings")]
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioMixerGroup _sfxGroup;

        [Header("Resources")]
        [SerializeField] private SoundScriptable _soundData;

        // ��� ��� SFX
        private List<AudioSource> _sfxPool = new List<AudioSource>();
        private AudioSource _musicSource;

        protected override void Awake()
        {
            base.Awake();

            // ������� ��������� �������� ��� ������� ������
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.outputAudioMixerGroup = _musicGroup;
            _musicSource.loop = true;
        }

		private void Start()
		{
            Sound.BGM.Play();
		}

		public void PlayMusic(Sound sound)
        {
            var data = _soundData[sound];
            if (data.Clip == null || _musicSource.clip == data.Clip) return;

            _musicSource.clip = data.Clip;

            _musicSource.volume = data.Volume;

            _musicSource.Play();
        }

        public void PlaySFX(Sound sound)
        {
            var data = _soundData[sound]; // �������� ��������� SoundData
            if (data.Clip == null) return;

            AudioSource source = GetFreeSFXSource();
            source.clip = data.Clip;

            // ��������� ��������� �� ����������
            source.volume = data.Volume;

            source.pitch = Random.Range(0.9f, 1.1f);
            source.Play();
        }

        private AudioSource GetFreeSFXSource()
        {
            // ���� ��������� �������� � ����
            foreach (var s in _sfxPool)
            {
                if (!s.isPlaying) return s;
            }

            // ���� �� �����, ������� �����
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = _sfxGroup;
            _sfxPool.Add(newSource);
            return newSource;
        }
    }