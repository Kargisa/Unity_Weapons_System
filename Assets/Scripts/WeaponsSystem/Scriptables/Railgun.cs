using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName="WeaponsSystem/Type/Railgun")]
public class Railgun : ScriptableObject, IWeaponType
{
    public Gradient color;
    [Min(0f)]
    public float startWidth;
    [Min(0f)]
    public float endWidth;

    public Material lineMaterial;

    [HideInInspector] public GameObject railgunLine;

    private LineRenderer _line;
    
    public void Initialize(Transform parent)
    {
        if (railgunLine == null)
            railgunLine = new GameObject("Railgun_Line");
        Instantiate(railgunLine);

        _line = railgunLine.AddComponent<LineRenderer>();
        _line.useWorldSpace = true;
        _line.colorGradient = color;
        _line.startWidth = startWidth;
        _line.endWidth = endWidth;
        _line.positionCount = 2;

        if (lineMaterial == null)
        {
            Debug.LogWarning($"{nameof(lineMaterial)} set to null, using default material");
            _line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
        }
        else
            _line.material = lineMaterial;

        _line.enabled = false;

        railgunLine.transform.parent = parent;
        railgunLine.transform.localPosition = Vector3.zero;
            
    }

    public IEnumerator Animate(Transform attackPoint, object data)
    {
        Vector3 hitPoint = (Vector3)data;

        _line.enabled = true;
        _line.SetPosition(0, attackPoint.position);
        _line.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(0.1f);
        _line.enabled = false;
    }

    public void Destroy()
    {
        Destroy(railgunLine);
    }
}
