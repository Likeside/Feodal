using System;

public class Vector2SliderAttribute : Attribute
{
    public float minValue;
    public float maxValue;

    public Vector2SliderAttribute(float min, float max)
    {
        minValue = min;
        maxValue = max;
    }
}