using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class CompassNeedle : MonoBehaviour
    {
        [SerializeField] float compassJitterMax = 4f;
        [SerializeField] float compassJitterSpeed = 12f;
        
        float activeJitter;
        Vector3 velocity = Vector3.zero;
        Quaternion targetRotation;
        Quaternion primaryRotation;

        void Start()
        {
            WeatherManager.OnWeatherUpdated += UpdateCompass;
        }

        void UpdateCompass(WeatherDay current, List<WeatherDay> forecast)
        {
            primaryRotation = Quaternion.Euler(0, 0, -current.WindDirection);
            targetRotation = primaryRotation;
        
            //Higher wind speeds create more jitter in the compass movement
            activeJitter = current.WindSpeed / 15f * compassJitterMax;
        }

        private void Update()
        {
            JitterCompass();
            MoveCompassNeedle();
        }

        void JitterCompass()
        {
            //This sets a new rotation somewhat close to the primary direction
            if (transform.rotation.eulerAngles != targetRotation.eulerAngles) return;
            
            float jitter = Random.Range(-activeJitter, activeJitter);
            targetRotation = Quaternion.Euler(0, 0, primaryRotation.eulerAngles.z + jitter);
        }
    
        void MoveCompassNeedle()
        {
            transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(
                transform.rotation.eulerAngles, 
                targetRotation.eulerAngles, 
                ref velocity, 
                compassJitterSpeed * Time.deltaTime));
        }
    }
}