using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class StatManager : MonoBehaviour
    {
        private static StatManager _instance;
        public static StatManager Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("Gamemanager is null");
                return _instance;
            }
        }

        public int Speed = 0;
        public int Health = 0;
        public int GunRpm = 0;
        public int Damage = 0;
        public int BulletCount = 0;
        public int Piercing = 0;
        public int Explosion = 0;

        private void Awake()
        {
            _instance = this;
        }

        public void Upgrade(Upgrades upgrade)
        {
            switch (upgrade)
            {
                case Upgrades.SPEED:
                    Speed++;
                    break;
                case Upgrades.HEALTH:
                    Health++;
                    break;
                case Upgrades.GUNRPM:
                    GunRpm++;
                    break;
                case Upgrades.BULLETCOUNT:
                    BulletCount++;
                    break;
                case Upgrades.PIERCING:
                    Piercing++;
                    break;
                case Upgrades.EXPLOSION:
                    Explosion++;
                    break;
            }
        }
        
        public enum Upgrades
        {
            SPEED,
            HEALTH,
            GUNRPM,
            DAMAGE,
            BULLETCOUNT,
            PIERCING,
            EXPLOSION
        }

    }
}