using UnityEngine;
using System.Collections.Generic;

public static class ObjectPoolScript
{
    const int defaultPoolSize = 3;

    class Pool
    {
        Stack<GameObject> inactive;
        private GameObject prefab;

        public Pool(GameObject prefab, int initialQty)
        {
            this.prefab = prefab;

            inactive = new Stack<GameObject>(initialQty);
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject currentObject;
            if (inactive.Count == 0)
            {
                currentObject = Object.Instantiate(prefab, position, rotation);

                currentObject.AddComponent<PoolMember>().pool = this;
            }
            else
            {
                currentObject = inactive.Pop();

                if (currentObject == null)
                {
                    return Spawn(position, rotation);
                }
            }

            currentObject.transform.position = position;
            currentObject.transform.rotation = rotation;
            currentObject.SetActive(true);

            return currentObject;
        }

        public void Despawn(GameObject currentObject)
        {
            currentObject.SetActive(false);

            inactive.Push(currentObject);
        }

    }

    class PoolMember : MonoBehaviour
    {
        public Pool pool;
    }

    static Dictionary<GameObject, Pool> pools;

    static void Init(GameObject prefab = null, int poolSize = defaultPoolSize)
    {
        if (pools == null)
        {
            pools = new Dictionary<GameObject, Pool>();
        }
        if (prefab != null && pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new Pool(prefab, poolSize);
        }
    }

    static public void Preload(GameObject prefab, int poolSize = 1)
    {
        Init(prefab, poolSize);

        GameObject[] objectArray = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            objectArray[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < poolSize; i++)
        {
            Despawn(objectArray[i]);
        }
    }

    static public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Init(prefab);

        return pools[prefab].Spawn(position, rotation);
    }

    static public void Despawn(GameObject currentObject)
    {
        if(currentObject != null)
        {
            PoolMember poolMember = currentObject.GetComponent<PoolMember>();
            if (poolMember == null)
            {
                Object.Destroy(currentObject);
            }
            else
            {
                poolMember.pool.Despawn(currentObject);
            }
        }
    }
}