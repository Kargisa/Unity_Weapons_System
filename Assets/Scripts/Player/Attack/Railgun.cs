using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(menuName="Player/Weapon/Type/Railgun")]
public class Railgun : ScriptableObject, IWeaponType
{
    public Gradient color;
    [Min(0f)]
    public float startWidth;
    [Min(0f)]
    public float endWidth;

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
        _line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
        _line.enabled = false;

        railgunLine.transform.parent = parent;
        railgunLine.transform.localPosition = Vector3.zero;
            
    }

    public IEnumerator Animate(Transform attackPoint, Vector3 hitPoint)
    {
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
