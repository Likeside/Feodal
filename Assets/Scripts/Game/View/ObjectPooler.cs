using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class ObjectPooler {

        Queue<GameObject> _objs;
        GameObject _prefab;
        int _curAmount;

        public ObjectPooler(GameObject prefab, int amount) {
            _prefab = prefab;
            _curAmount = amount;
            _objs = new Queue<GameObject>();
            SpawnObjs();
        }
        public void DisableAll() {
            foreach (var obj in _objs) {
                obj.SetActive(false);
            }
        }
        public GameObject SpawnFromPool() {
            if (_objs.Count == 0) {
                SpawnObjs();
            }
            var obj = _objs.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        public T SpawnFromPoolComp<T>() where T : Component {
           if (_objs.Count == 0) {
               SpawnObjs();
           }
           var obj = _objs.Dequeue();
           obj.SetActive(true);
           return obj.GetComponent<T>();
       }
        public void ReturnToPool(GameObject obj) {
            _objs.Enqueue(obj);
            obj.SetActive(false);
        }
        
        void SpawnObjs() {
            for (int i = 0; i < _curAmount; i++) {
                var obj = GameObject.Instantiate(_prefab);
                _objs.Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
}