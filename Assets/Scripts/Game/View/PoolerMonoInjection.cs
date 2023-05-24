using GameScripts;
using UnityEngine;
using Zenject;

namespace Game.View {
    public class PoolerMonoInjection: MonoBehaviour {

        protected ObjectPooler _pooler;


        [Inject]
        public void SetPooler(ObjectPooler pooler) {
            _pooler = pooler;
        }
    }
}