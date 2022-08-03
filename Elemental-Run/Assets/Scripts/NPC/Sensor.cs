using UnityEngine;

[System.Serializable]
public class Sensor: MonoBehaviour
{
    public bool isSameColor;
    public bool hasDetected;

    private void Start()
    {
        hasDetected = false;
        isSameColor = false;
    }

    public bool DetectPlayer(Vector3 checkPos,float radius,LayerMask layer,BodyTypeManager goal,ElementName colorName)
    {
        if (!Physics.CheckSphere(checkPos, radius, layer))
        {
            return false;
        }

        if (goal.GetBodyColor() == colorName)
        {
            isSameColor = true;
            return false;
        }

        return true;

    }
}