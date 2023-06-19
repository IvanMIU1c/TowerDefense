using UnityEngine;
using SpaceShooter;


namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject

    {
        public int goldCost = 15;
        public Sprite towerGui;
        public Sprite sprite;
        public Tower towerPrefab;
        public TurretProperties turretProperties;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;
        public bool IsAvailable()
        {
            return !requiredUpgrade || requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        }

        public TowerAsset[] m_upgradesTo;
    }
}