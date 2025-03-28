using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    const string ColorProperty = "_Color";

    [SerializeField] private Color _defaultColor = Color.red;

    private Color NewColor;

    public void SetRandomColor(Cube cube)
    {
        NewColor = Random.ColorHSV();
        cube.GetRenderer().material.SetColor(ColorProperty, NewColor);
    }


    public void SetDefaultColor(Cube cube)
    {
        NewColor = _defaultColor;
        cube.GetRenderer().material.SetColor(ColorProperty, NewColor);
    }
}