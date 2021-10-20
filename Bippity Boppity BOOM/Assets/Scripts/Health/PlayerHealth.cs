using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class PlayerHealth : MonoBehaviour
    {
        #region Health Variables

        public HealthBar healthBar; // Health bar UI.

        public int maxHealth = 480; // Maximum health - actual value set inside the inspector.
        public int currentHealth; // Current health. Used in this class to determine how much damage you've taken and keep the bar's UI accurate.
        private int DamageOverTime = 1; // Bleeding out mechanic.

        #endregion

        void Start() // Start is called before the first frame update
        {
            currentHealth = maxHealth; // Starts the game and has the player at maximum health.
            healthBar.SetMaxHealth(maxHealth); // Starts the game with a full health bar.
            StartCoroutine(BleedOut()); // A coroutine that will make the player take 1 damage every second.
            WrongBox();
        }

    public void WrongBox()
    {
        currentHealth -= 150; // When the player opens the wrong box, take off a chunk of their blood.
    }

    IEnumerator BleedOut()
        {
            float lastDecrementTime = 0f;
            float delayBetweenDecrements = 1f; // Keeps a 1-second gap between 'bleeds'.

            while (currentHealth > 0) // While the player's health is above 0...
            {
                if (Time.time - lastDecrementTime > delayBetweenDecrements) // And it's been a second since their last 'bleed'...
                {
                    currentHealth -= 1; // Reduces the player's health by 1.
                    healthBar.SetHealth(currentHealth); // Keeps the health bar's filling accurate
                    lastDecrementTime += delayBetweenDecrements; // Keeps the time between each 'bleed' accurate.
                }
                yield return null;
            }
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage; // Reduces health number by the number of damage dealt.      
            healthBar.SetHealth(currentHealth);
        }
    }