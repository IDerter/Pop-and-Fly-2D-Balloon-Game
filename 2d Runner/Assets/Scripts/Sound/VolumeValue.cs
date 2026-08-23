using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace AmNuamRunner
{
    public class VolumeValue : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private AudioMixerGroup _SFXAudioMixer;
        [SerializeField] private AudioMixerGroup _musicAudioMixer;

        [SerializeField] private MoveSlider _slidersSettings;

        [SerializeField] private Slider _silderSoundsVolume;
        [SerializeField] private Slider _silderMusicVolume;

        private float minVolumeValue = -80f;
        private float maxVolumeValue = 0f;

        private void OnEnable()
        {
            YG2.onGetSDKData += LoadVolumeData;
        }

        private void OnDisable()
        {
            YG2.onGetSDKData -= LoadVolumeData;
        }

        private void Start()
        {
            if (YG2.isSDKEnabled)
            {
                LoadVolumeData();
            }
        }

        private void LoadVolumeData()
        {
            if (YG2.saves == null) return;

            // ������ ��������� ���� � ����. �������� �� �������, ���� ���� �������!
            _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, 0, YG2.saves.musicVolume));
            _audioMixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-40, 0, YG2.saves.soundsVolume));

            if (YG2.saves.isSFXEnabled)
            {
                _audioMixerGroup.audioMixer.SetFloat("SFXVolume", maxVolumeValue);
            }
            else
            {
                _audioMixerGroup.audioMixer.SetFloat("SFXVolume", minVolumeValue);
            }
        }

        public void GameStop()
        {
            Time.timeScale = 0;
            Sound.Click.Play();
        }

        public void GameStart()
        {
            Time.timeScale = 1;
            Sound.Click.Play();
        }

        // ���� ����� ���������� ����� ������� ��� �������� ���� (��� �� ���������)
        public void SetSliderValue()
        {
            if (_silderSoundsVolume == null || _silderMusicVolume == null || _slidersSettings == null)
                return;

            // 1. ������������� ��������� UI (����� �������� �������� ���������� ������ �� ������)
            Canvas.ForceUpdateCanvases();

            // 2. ������������� �������� ��� �������� OnValueChanged (����� �� ������� ������������)
            _silderSoundsVolume.SetValueWithoutNotify(YG2.saves.soundsVolume);
            if (_slidersSettings.SliderFillVolume != null)
                _slidersSettings.SliderFillVolume.fillAmount = _silderSoundsVolume.value;

            _silderMusicVolume.SetValueWithoutNotify(YG2.saves.musicVolume);
            if (_slidersSettings.SliderFillMusic != null)
                _slidersSettings.SliderFillMusic.fillAmount = _silderMusicVolume.value;
        }

        public void ToogleSFX(bool enabled)
        {
            if (enabled)
            {
                _audioMixerGroup.audioMixer.SetFloat("SFXVolume", maxVolumeValue);
            }
            else
            {
                _audioMixerGroup.audioMixer.SetFloat("SFXVolume", minVolumeValue);
            }

            YG2.saves.isSFXEnabled = enabled;
            YG2.SaveProgress();
        }

        public void SliderMusic(float volume)
        {
            _musicAudioMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, 0, volume));
            YG2.saves.musicVolume = volume;
        }

        public void ChangeVolume(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-40, 0, volume));
            YG2.saves.soundsVolume = volume;
        }

        public void SaveSettingsToCloud()
        {
            YG2.SaveProgress();
        }
    }
}