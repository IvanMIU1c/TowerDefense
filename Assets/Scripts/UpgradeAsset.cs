using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite sprite;

        public int[] costByLevel = { 3 };
    }
}