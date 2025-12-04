using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager Instance;   // other

    [Header("Weather Objects")]
    public GameObject rainGenerator;
    public GameObject windGenerator;
    public GameObject sunlightManager;

    [Header("Bee")]
    public BeeScript bee;

    [Header("Timing")]
    public float normalDuration = 10f;
    public float weatherMinDuration = 5f;
    public float weatherMaxDuration = 8f;

    [Header("Game Speed")]
    public float worldSpeedMultiplier = 1f;   // 1 = normal, >1 = faster, <1 = slower

    float timer = 0f;
    float currentDuration;
    bool weatherActive = false;
    GameObject currentWeather;
    int lastChoice = -1;
    // Start is called before the first frame update
   void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentDuration = normalDuration;
        DisableAllWeather();
        ResetBee();
        worldSpeedMultiplier = 1f;   // start normal
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentDuration)
        {
            timer = 0f;

            if (!weatherActive)
                ActivateRandomWeather();
            else
                StopWeather();
        }
    }
void ActivateRandomWeather()
    {
        weatherActive = true;

        int choice;
        do
        {
            choice = Random.Range(0, 3); // 0 = sun, 1 = rain, 2 = wind
        } while (choice == lastChoice);
        lastChoice = choice;

        if (choice == 0) currentWeather = sunlightManager;
        if (choice == 1) currentWeather = rainGenerator;
        if (choice == 2) currentWeather = windGenerator;

        if (bee != null)
        {
            ResetBee(); // back to base first

            if (choice == 0)          // sun fast mode
            {
                bee.speedMultiplier = 1f;   // keep control normal
                worldSpeedMultiplier = 1.3f; // everything in game faster
            }
            else if (choice == 1)     // rain slower
            {
                bee.speedMultiplier = 0.2f;
                worldSpeedMultiplier = 0.7f;
            }
            else if (choice == 2)     // wind push back
            {
                bee.speedMultiplier = 1f;
                bee.windForce = -2f;
                worldSpeedMultiplier = 0.7f; // keep overall speed normal
            }
        }

        if (currentWeather != null)
            currentWeather.SetActive(true);

        currentDuration = Random.Range(weatherMinDuration, weatherMaxDuration);
    }

    void StopWeather()
    {
        weatherActive = false;
        DisableAllWeather();
        ResetBee();
        worldSpeedMultiplier = 1f;    // back to normal 
        currentDuration = normalDuration;
    }

    void DisableAllWeather()
    {
        if (rainGenerator != null) rainGenerator.SetActive(false);
        if (windGenerator != null) windGenerator.SetActive(false);
        if (sunlightManager != null) sunlightManager.SetActive(false);
    }

    void ResetBee()
    {
        if (bee == null) return;

        bee.speedMultiplier = 1f;
        bee.windForce = 0f;
    }
}
