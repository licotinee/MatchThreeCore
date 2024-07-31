using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ObjectPooling : Singleton<ObjectPooling >  
    {

        Dictionary<GameObject, List<GameObject>> _pools = new Dictionary<GameObject, List<GameObject>>();

        public GameObject getObject(GameObject g)
        {
            List<GameObject> pool = new List<GameObject>();
            if (_pools.ContainsKey(g))
                pool = _pools[g];
            else
                _pools.Add(g, pool);

            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                    return obj;
            }

            GameObject obj2 = Instantiate(g);
            pool.Add(obj2);
            obj2.SetActive(false);

            _pools[g] = pool;
            return obj2;
        }


        public T getObjectType<T>(T component) where T: MonoBehaviour
        {
            List<GameObject> pool = new List<GameObject>();
            if (_pools.ContainsKey(component.gameObject))
                pool = _pools[component.gameObject];
            else
                _pools.Add(component.gameObject, pool);

            foreach (GameObject obj in pool)
            {
                if (!obj.activeSelf)
                    return obj.GetComponent<T>();
            }

            T obj2 = Instantiate(component);
            pool.Add(obj2.gameObject);
            obj2.gameObject.SetActive(false);

            _pools[component.gameObject] = pool;
            return obj2; 
        }
        
    
    }
