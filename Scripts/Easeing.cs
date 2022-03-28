using UnityEngine;

public class Easing
{
    public static float QuadIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return To * t * t + Zero;
    }

    public static float QuadOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return -To * t * (t - 2) + Zero;
    }

    public static float QuadInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime / 2;
        if (t < 1) return To / 2 * t * t + Zero;

        t = t - 1;
        return -To / 2 * (t * (t - 2) - 1) + Zero;
    }

    public static float CubicIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return To * t * t * t + Zero;
    }

    public static float CubicOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t = t / TotalTime - 1;
        return To * (t * t * t + 1) + Zero;
    }

    public static float CubicInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime / 2;
        if (t < 1) return To / 2 * t * t * t + Zero;

        t = t - 2;
        return To / 2 * (t * t * t + 2) + Zero;
    }

    public static float QuartIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return To * t * t * t * t + Zero;
    }

    public static float QuartOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t = t / TotalTime - 1;
        return -To * (t * t * t * t - 1) + Zero;
    }

    public static float QuartInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime / 2;
        if (t < 1) return To / 2 * t * t * t * t + Zero;

        t = t - 2;
        return -To / 2 * (t * t * t * t - 2) + Zero;
    }

    public static float QuintIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return To * t * t * t * t * t + Zero;
    }

    public static float QuintOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t = t / TotalTime - 1;
        return To * (t * t * t * t * t + 1) + Zero;
    }

    public static float QuintInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime / 2;
        if (t < 1) return To / 2 * t * t * t * t * t + Zero;

        t = t - 2;
        return To / 2 * (t * t * t * t * t + 2) + Zero;
    }

    public static float SineIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        return -To * Mathf.Cos(t * (Mathf.PI * 90 / 180) / TotalTime) + To + Zero;
    }

    public static float SineOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        return To * Mathf.Sin(t * (Mathf.PI * 90 / 180) / TotalTime) + Zero;
    }

    public static float SineInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        return -To / 2 * (Mathf.Cos(t * Mathf.PI / TotalTime) - 1) + Zero;
    }

    public static float ExpIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        return t == 0.0 ? Zero : To * Mathf.Pow(2, 10 * (t / TotalTime - 1)) + Zero;
    }

    public static float ExpOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        return t == TotalTime ? To + Zero : To * (-Mathf.Pow(2, -10 * t / TotalTime) + 1) + Zero;
    }

    public static float ExpInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        if (t == 0.0f) return Zero;
        if (t == TotalTime) return To;
        To -= Zero;
        t /= TotalTime / 2;

        if (t < 1) return To / 2 * Mathf.Pow(2, 10 * (t - 1)) + Zero;

        t = t - 1;
        return To / 2 * (-Mathf.Pow(2, -10 * t) + 2) + Zero;

    }

    public static float CircIn(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime;
        return -To * (Mathf.Sqrt(1 - t * t) - 1) + Zero;
    }

    public static float CircOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t = t / TotalTime - 1;
        return To * Mathf.Sqrt(1 - t * t) + Zero;
    }

    public static float CircInOut(float t, float TotalTime = 1.0f, float Zero = 0.0f, float To = 1.0f)
    {
        To -= Zero;
        t /= TotalTime / 2;
        if (t < 1) return -To / 2 * (Mathf.Sqrt(1 - t * t) - 1) + Zero;

        t = t - 2;
        return To / 2 * (Mathf.Sqrt(1 - t * t) + 1) + Zero;
    }
}