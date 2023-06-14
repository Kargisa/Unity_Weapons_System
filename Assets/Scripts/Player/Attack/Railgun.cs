using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(menuName="Player/Weapon/Type/Railgun")]
public class Railgun : ScriptableObject, IWeapon
{
    public Gradient color;
    [Min(0f)]
    public float startWidth;
    [Min(0f)]
    public float endWidth;

    [HideInInspector] public GameObject railgunLine;

    private LineRenderer _line;
    private AttackSettings.Range settings;

    public void Initialize(Transform transform, AttackSettings attackSettings)
    {
        if (railgunLine == null)
            railgunLine = new GameObject("Railgun_Line");
        Instantiate(railgunLine);
        settings = (AttackSettings.Range)attackSettings;

        _line = railgunLine.AddComponent<LineRenderer>();
        _line.useWorldSpace = true;
        _line.colorGradient = color;
        _line.startWidth = startWidth;
        _line.endWidth = endWidth;
        _line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
        _line.enabled = false;

        railgunLine.transform.parent = transform;
        railgunLine.transform.localPosition = Vector3.zero;

    }

    public IEnumerator Animate(Transform shootpoint, Vector3 hitPoint)
    {
        Vector3 endpoint;
        if (hitPoint == Vector3.zero)
            endpoint = shootpoint.position + shootpoint.forward * settings.range;
        else
            endpoint = hitPoint;
        
        _line.enabled = true;
        _line.SetPosition(0, shootpoint.position);
        _line.SetPosition(1, endpoint);
        yield return new WaitForSeconds(0.1f);
        _line.enabled = false;
    }

    public void Destroy()
    {
        Destroy(railgunLine);
    }

}
