using System;
using UnityEngine;

namespace Vampire.Select
{
    [Serializable]
    public class ExplanationData 
    {
        [SerializeField, Multiline(3)] string _text;
        [SerializeField] Sprite _image;

        /// <value>説明文</value>
        public string Text
        {
            get { return _text; }
        }

        /// <value>説明画像</value>
        public Sprite Image
        {
            get { return _image; }
        }
    }
}
