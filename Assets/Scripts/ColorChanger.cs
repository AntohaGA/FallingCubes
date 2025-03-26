using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    const string ColorProperty = "_Color";
    Color NewColor;

    public void SetDefaultColor(Cube cube, Color color)
    {
        cube.GetRenderer().material.SetColor(ColorProperty, color);
    }

    public void ChangeColor(Cube cube)
    {
        NewColor = Random.ColorHSV();
        cube.GetRenderer().material.SetColor(ColorProperty, NewColor);
    }
}