using UnityEngine;

namespace Vampire.Gimmick
{
    public class TargetInfo : MonoBehaviour
    {
        [SerializeField] string _comment;

        /// <value>表示するコメント</value>
        public string Comment
        {
            get { return _comment; }
        }
    }
}
