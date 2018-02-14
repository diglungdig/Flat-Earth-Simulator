using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Pooling using Dictionary and Queue
/// </summary>
public class ObjectPooler : MonoBehaviour {

#region Singleton
    //Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
#endregion

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    // Use this for initialization
    void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject newOne = Instantiate(pool.prefab);
                newOne.SetActive(false);

                queue.Enqueue(newOne);
            }

            poolDictionary.Add(pool.name, queue);

        }
	}


    /// <summary>
    /// Return a gameobject from the pool[name]
    /// </summary>
    /// <param name="name"></param>
    /// <param name="Pos"></param>
    /// <param name="rot"></param>
    /// <returns></returns>
    public GameObject SpawnPoolingObject(string name, Vector3 Pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(name))
        {
            Debug.Log("This key "+ name + " doesnt exist");
            return null;
        }

        GameObject newOne = poolDictionary[name].Dequeue();

        newOne.GetComponent<Rigidbody>().velocity = Vector3.zero;

        newOne.SetActive(true);
        newOne.transform.position = Pos;
        newOne.transform.rotation = rot;

        poolDictionary[name].Enqueue(newOne);

        return newOne;
    }
}
