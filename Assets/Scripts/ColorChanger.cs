using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    const string ColorProperty = "_Color";

    [SerializeField] private Color _defaultColor = Color.red;

    public void SetRandomColor(Cube cube)
    {
        cube.Renderer.material.SetColor(ColorProperty, Random.ColorHSV());
    }


    public void SetDefaultColor(Cube cube)
    {
        cube.Renderer.material.SetColor(ColorProperty, _defaultColor);
    }
}