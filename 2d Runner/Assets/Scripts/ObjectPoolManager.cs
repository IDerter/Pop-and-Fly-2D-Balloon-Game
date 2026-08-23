using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    
    // Словарь, где ключ — имя префаба, а значение — очередь свободных объектов
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        string key = prefab.name;

        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
        }

        GameObject objToSpawn = null;

        // Если в пуле есть свободные объекты, берем первый
        if (poolDictionary[key].Count > 0)
        {
            objToSpawn = poolDictionary[key].Dequeue();
            Debug.Log($"<color=green>[POOL]</color> Достали из запаса: {key}. Осталось свободных: {poolDictionary[key].Count}");
        }
        else // Если свободных нет, создаем новый
        {
            objToSpawn = Instantiate(prefab, parent);
            objToSpawn.name = prefab.name; 
            Debug.Log($"<color=red>[POOL]</color> В запасе нет, создали НОВЫЙ: {key}");
        }

        // Настраиваем позицию
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        // Включаем сам объект
        objToSpawn.SetActive(true);

        // КРИТИЧЕСКИ ВАЖНО ДЛЯ СОСТАВНЫХ ПРЕФАБОВ:
        // Включаем обратно всех детей внутри Variant1, Variant2 и т.д., 
        // так как они могли быть выключены по отдельности при сборе
        foreach (Transform child in objToSpawn.transform)
        {
            child.gameObject.SetActive(true);
        }

        return objToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        // Очищаем имя от (Clone), чтобы ключи пула всегда совпадали с именами префабов
        string cleanName = obj.name.Replace("(Clone)", "").Trim();
        obj.name = cleanName;

        obj.SetActive(false);
        
        if (!poolDictionary.ContainsKey(cleanName))
        {
            poolDictionary[cleanName] = new Queue<GameObject>();
        }
        
        poolDictionary[cleanName].Enqueue(obj);
    }
}