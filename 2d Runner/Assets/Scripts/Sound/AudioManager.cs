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

        [Header("Pool Settings")]
        [SerializeField] private int _maxSfxSources = 15;

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
            // 1. Ищем свободный источник
            foreach (var s in _sfxPool)
            {
                if (!s.isPlaying) return s;
            }

            // 2. Если свободных нет, но лимит еще НЕ исчерпан — создаем новый
            if (_sfxPool.Count < _maxSfxSources)
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.outputAudioMixerGroup = _sfxGroup;
                _sfxPool.Add(newSource);
                return newSource;
            }

            // 3. Если пул переполнен — "крадем" самый старый играющий источник.
            // Обычно самый первый элемент в списке играет дольше всех.
            AudioSource oldestSource = _sfxPool[0];
            
            // Перемещаем его в конец списка, чтобы в следующий раз перезаписать другой
            _sfxPool.RemoveAt(0);
            _sfxPool.Add(oldestSource);
            
            return oldestSource; 
        }
    }