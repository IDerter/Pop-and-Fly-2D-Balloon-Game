using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu()]
    public class SoundScriptable : ScriptableObject
    {
        // Вложенная структура для настроек конкретного звука
        [Serializable]
        public struct SoundData
        {
            public AudioClip Clip;
            [Range(0f, 1f)] public float Volume;
        }

        [SerializeField] private SoundData[] _sounds;

        // Обновленный индексатор: теперь возвращает структуру целиком
        public SoundData this[Sound s] => _sounds.Length > (int)s ? _sounds[(int)s] : new SoundData { Volume = 1f };

#if UNITY_EDITOR
        [CustomEditor(typeof(SoundScriptable))]
        public class SoundInspector : Editor
        {
            private static readonly int _soundCount = Enum.GetValues(typeof(Sound)).Length;
            private new SoundScriptable target => base.target as SoundScriptable;

            public override void OnInspectorGUI()
            {
                if (target._sounds == null || target._sounds.Length != _soundCount)
                {
                    Undo.RecordObject(target, "Resize Sound Array");
                    Array.Resize(ref target._sounds, _soundCount);
                    // Инициализируем громкость по умолчанию, если она 0
                    for (int i = 0; i < target._sounds.Length; i++)
                        if (target._sounds[i].Volume <= 0) target._sounds[i].Volume = 1f;

                    EditorUtility.SetDirty(target);
                }

                EditorGUI.BeginChangeCheck();

                for (int i = 0; i < target._sounds.Length; i++)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField($"{(Sound)i}", EditorStyles.boldLabel);

                    target._sounds[i].Clip = EditorGUILayout.ObjectField("Clip",
                        target._sounds[i].Clip, typeof(AudioClip), false) as AudioClip;

                    target._sounds[i].Volume = EditorGUILayout.Slider("Volume",
                        target._sounds[i].Volume, 0f, 1f);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space(2);
                }

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
            }
        }
#endif
    }