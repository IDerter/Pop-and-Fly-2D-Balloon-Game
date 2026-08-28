using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager :  SingletonBase<ObjectPoolManager>
{
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        string key = prefab.name;

        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
        }

        GameObject objToSpawn = null;

        // 1. ЗАЩИТА: Перебираем очередь, пока не найдем "живой" объект
        while (poolDictionary[key].Count > 0)
        {
            objToSpawn = poolDictionary[key].Dequeue();
            
            // Если объект не равен null (не был уничтожен Unity при рестарте), выходим из цикла
            if (objToSpawn != null)
            {
                break;
            }
        }

        // 2. Создаем новый, если живых в запасе не осталось
        if (objToSpawn == null)
        {
            objToSpawn = Instantiate(prefab, parent);
            objToSpawn.name = prefab.name; 
            Debug.Log($"<color=red>[POOL]</color> В запасе нет (или удалены), создали НОВЫЙ: {key}");
        }
        else
        {
            Debug.Log($"<color=green>[POOL]</color> Достали из запаса: {key}. Осталось свободных: {poolDictionary[key].Count}");
        }

        // Настраиваем позицию
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);

        // Включаем обратно всех детей внутри Variant
        foreach (Transform child in objToSpawn.transform)
        {
            child.gameObject.SetActive(true);
        }

        return objToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        string cleanName = obj.name.Replace("(Clone)", "").Trim();
        obj.name = cleanName;
        
        if (!poolDictionary.ContainsKey(cleanName))
        {
            poolDictionary[cleanName] = new Queue<GameObject>();
        }
        
        // --- ЗАЩИТА ОТ ДВОЙНОГО ВОЗВРАТА ---
        // Если этот объект уже числится в очереди, просто прерываем выполнение
        if (poolDictionary[cleanName].Contains(obj))
        {
            return; 
        }

        obj.SetActive(false);
        poolDictionary[cleanName].Enqueue(obj);
    }
}