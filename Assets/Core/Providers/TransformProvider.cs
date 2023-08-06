using Core.Components;
using Voody.UniLeo.Lite;
using UnityEngine;

[DisallowMultipleComponent]
public class TransformProvider : MonoProvider<TransformComponent>
{
    private void OnValidate()
    {
        value.transform = value.transform ? value.transform : transform;
        value.convertToEntity = GetComponent<ConvertToEntity>();
    }
}