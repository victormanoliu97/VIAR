using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour {
    public static bool Equals(float a,float b,float accuracy = 0)
    {
        Debug.Log("Eq(" + a + "," + b + ")");
        a = To360(a);
        b = To360(b);
        float dif1 = Mathf.Abs(a - b);
        float dif2 = Mathf.Abs(360 - dif1);
        float dif = Mathf.Min(dif1, dif2);
        Debug.Log(a + " " + b + " // " + dif1 + " " + dif2 + " // " + dif + " " + (dif <= accuracy));
        return (dif <= accuracy);
    }

    public static float To360(float x)
    {
        x %= 360f;
        if (x < 0f)
            x += 360f;
        x %= 360f;
        return x;
    }
    public static float toSigned(float x)
    {
        x %= 360;
        if (x > 180)
            x = -x + 180;
        return x;
    }
    public static float Add(float a,float b)
    {
        a = To360(a);
        b = To360(b);
        return ((a + b) % 360);
    }
}
