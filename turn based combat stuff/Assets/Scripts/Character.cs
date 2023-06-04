using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Characer", menuName = "Character")]
public class Character : ScriptableObject
{
    public string charName;
    public string charDMG;
    public string charHP;
    public string charLVL;
    public Sprite charSprite;
    public float zoom = 1;
}
