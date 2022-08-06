﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
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

        public UnityEvent UpGradeEvent = new();


        public int UpgradePoints = 0;

        public int Speed = 0;
        public int Health = 0;
        public int GunRpm = 0;
        public int Damage = 0;
        public int BulletCount = 0;
        public int Piercing = 0;
        public int Explosion = 0;

        public TextMeshProUGUI SpeedVisual;
        public TextMeshProUGUI HealthVisual;
        public TextMeshProUGUI GunVisual;
        public TextMeshProUGUI DamageVisual;
        public TextMeshProUGUI BulletCountVisual;
        public TextMeshProUGUI PiercingVisual;
        public TextMeshProUGUI ExplosionVisual;
        public TextMeshProUGUI UpgradesVisual;

        private void Awake()
        {
            _instance = this;
        }

        public void Upgrade(Upgrades upgrade)
        {
            if (UpgradePoints < 1) return;
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
                case Upgrades.DAMAGE:
                    Damage++;
                    break;
            }
            UpgradePoints--;
            UpGradeEvent.Invoke();
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
        public int GetHealth()
        {
            return 100 + Health * 10;
        }

        public void UpgradeSpeed() 
        {
            Upgrade(Upgrades.SPEED);
        }

        public void UpgradeHealth()
        {
            Upgrade(Upgrades.HEALTH);
        }

        public void UpgradeGunRpm()
        {
            Upgrade(Upgrades.GUNRPM);
        }
        public void UpgradeDamage()
        {
            Upgrade(Upgrades.DAMAGE);
        }
        public void UpgradeBulletCount()
        {
            Upgrade(Upgrades.BULLETCOUNT);
        }
        public void UpgradePiercing()
        {
            Upgrade(Upgrades.PIERCING);
        }
        public void UpgradeExplosion()
        {
            Upgrade(Upgrades.EXPLOSION);
        }

        public void GiveUpgradePoint()
        {
            UpgradePoints++;
        }

        private void Update()
        {
            BulletCountVisual.text = "5" + BulletCount;
            DamageVisual.text = "5" + Damage;
            ExplosionVisual.text = "5" + Explosion;
            GunVisual.text = "5" + GunRpm;
            HealthVisual.text = "5" + Health;
            PiercingVisual.text = "5" + Piercing;
            SpeedVisual.text = "5" + Speed;
            UpgradesVisual.text = "5" + UpgradePoints;
        }
    }
}