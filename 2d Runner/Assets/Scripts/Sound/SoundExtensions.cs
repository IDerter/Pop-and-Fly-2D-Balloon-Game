using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundExtensions
{
    public static void Play(this Sound sound)
    {
        // ���� ��� ������� ������ (BGM), ��������� ����� ����������� �����
        if (sound == Sound.BGM)
            AudioManager.Instance.PlayMusic(sound);
        else
            AudioManager.Instance.PlaySFX(sound);
    }
}