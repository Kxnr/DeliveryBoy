using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelList", menuName = "ModelList", order = 1)]

public class ModelList : ScriptableObject
{
    public List<GameObject> models;
}
