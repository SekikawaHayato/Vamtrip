using System.Collections.Generic;
using UnityEngine;

namespace Vampire.AI
{
    public struct AIData
    {
        public AIState state;
        public GameObject target;
        public GameObject myself;
        public List<GameObject> allTargets;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="myself">自身のGameObject</param>
        /// <param name="targets">全ての目的地</param>
        public AIData(GameObject myself,GameObject[] targets)
        {
            this.myself = myself;
            this.state = AIState.Stay;
            this.target = null;
            this.allTargets = new List<GameObject>(targets);
        }
    }
}
