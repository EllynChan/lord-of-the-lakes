using UnityEngine;

public class LogNormalRandom : MonoBehaviour
{
    // Function to generate a log-normal random number
    public static float LogNormal(float mean, float stdDev)
    {
        // Generate a normal distributed value (Gaussian)
        float u1 = Random.value; // Random number between 0 and 1
        float u2 = Random.value;

        // Box-Muller transform to generate standard normal distribution (mean 0, std dev 1)
        float standardNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

        // Adjust by mean and standard deviation for normal distribution
        float normalValue = mean + stdDev * standardNormal;

        // Convert to log-normal distribution
        return Mathf.Exp(normalValue);
    }
}
