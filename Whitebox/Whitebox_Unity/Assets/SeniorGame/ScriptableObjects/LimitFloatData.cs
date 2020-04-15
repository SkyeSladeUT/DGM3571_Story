using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/float/limited float")]
public class LimitFloatData : FloatData
{
    public float MinValue, MaxValue;

    public override void SetFloat(float value)
    {
        base.SetFloat(value);
        CheckFloat();
    }

    public override void AddFloat(float value)
    {
        base.AddFloat(value);
        CheckFloat();
    }

    public override void SubFloat(float value)
    {
        base.SubFloat(value);
        CheckFloat();
    }


    private void CheckFloat()
    {
        if (this.value > MaxValue)
        {
            this.value = MaxValue;
        }
        else if (this.value < MinValue)
        {
            this.value = MinValue;
        }
    }
}
