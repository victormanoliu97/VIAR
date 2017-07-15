using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour {
    public static float Distance(float From, float To)
    {
        From = To360(From);
        To = To360(To);
        float dif1 = Mathf.Abs(From - To);
        float dif2 = 360 - dif1;
        return Mathf.Min(dif1, dif2);
    }
    public static bool Equals(float a,float b,float accuracy = 1f)
    {
        return (Distance(a,b) <= accuracy);
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
            x = x - 360;
        else if (x < -180)
            x = x + 360;
        return x;
    }
    public static float Add(float a,float b)
    {
        a = To360(a);
        b = To360(b);
        return ((a + b) % 360);
    }

    public static bool Contains(float x,float left,float right)
    {
        left = To360(left);
        right = To360(right);
        if (left < right)
            return (left <= x && x <= right);
        else if (left > right)
            return (left <= x || x <= right);
        else
            return (x == left);

    }
    public static int CloserDirection(float From,float To)
    {
        From = To360(From);
        To = To360(To);
        if (Equals(From, To))
            return 0;
        else if (Contains(To, From, Add(From, 180)))
            return 1;
        else
            return -1;
    }
    public static float RotationTo(float From,float To,float by)
    {
        From = To360(From);
        To = To360(To);
        Debug.Log("RotationTo(" + From + "," + To + "," + by + ")");
        if (Equals(From,To))
            return From;
        else
        {
            int direction = CloserDirection(From,To);
            if (Distance(From, To) <= by)
            {
                Debug.Log("last step " + From + " " + To);
                return To;
            }   
            else
            {
                float rot = To360(From + by * direction);
                Debug.Log("rot = " + rot);
                return rot;
            }
        }
    }

    float rotat = 0, tar = 270;
    float sped = 20f;
    private void Update()
    {
        //Debug.Log(rotat);
        //Debug.Log(Distance(180, 0));
        //rotat = RotationTo(rotat, tar, sped * Time.deltaTime);
    }

}
