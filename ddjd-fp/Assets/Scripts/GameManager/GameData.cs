using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Data")]
public class GameData: ScriptableObject {
    public class Data: ScriptableObject {
        private static Data _instance;

        public int currentHealth;
        public int maxHealth;
        
        public int numCrystals;
        
        public static Data GetInstance() {
            if (!_instance) {
                _instance = FindObjectOfType<Data>();
            }

            if (!_instance)  {
                _instance = CreateInstance<Data>();

                // Initialize Values
                _instance.currentHealth = 0;
                _instance.maxHealth = 0;

                _instance.numCrystals = 0;
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

    public int NumCrystals { get { return Data.GetInstance().numCrystals; } set { Data.GetInstance().numCrystals = value; } }
    #endregion
}