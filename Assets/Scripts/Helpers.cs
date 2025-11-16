using UnityEngine;

public static class Helpers
{
    public static float GetPercentage(float percentage, float ofThis)
    {
        float percent = (percentage / ofThis) * 100;

        return percent;
    }

    public static int GetPercentage(int percentage, int ofThis)
    {
        int percent = (percentage / ofThis) * 100;

        return percent;
    }

    public static double GetPercentage(double percentage, double ofThis)
    {
        double percent = (percentage / ofThis) * 100;

        return percent;
    }

    public static float ClampValue(float value)
    {
        return value / 100;
    }

    public static float ClampValue(int value)
    {
        return Mathf.Clamp01(value / 100f);
    }
}
