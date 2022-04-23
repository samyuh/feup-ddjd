using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Data")]
public class GameData: ScriptableObject {
    public class Data: ScriptableObject {
        private static Data _instance;

        public int currentHealth;
        public int maxHealth;
        public int healthCrystal;
        public int manaCrystal;
        
        public static Data GetInstance() {
            if (!_instance) {
                _instance = FindObjectOfType<Data>();
            }

            if (!_instance)  {
                _instance = CreateInstance<Data>();

                // Initialize Values
                _instance.currentHealth = 0;
                _instance.maxHealth = 0;
                _instance.healthCrystal = 0;
                _instance.manaCrystal = 0;
            }

            return _instance;
        }
    }

    public Data GetInstance() {
        return Data.GetInstance();
    }

    #region Global Attributes
    public int CurrentHealth { get { return Data.GetInstance().currentHealth; } set { Data.GetInstance().currentHealth = value; } }
    public int MaxHealth { get { return Data.GetInstance().maxHealth; } set { Data.GetInstance().maxHealth = value; } }
    public int HealthCrystal { get { return Data.GetInstance().healthCrystal; } set { Data.GetInstance().healthCrystal = value; } }
    public int ManaCrystal { get { return Data.GetInstance().manaCrystal; } set { Data.GetInstance().manaCrystal = value; } }
    #endregion
}