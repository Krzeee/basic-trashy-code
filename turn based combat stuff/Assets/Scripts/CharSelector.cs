using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharSelector : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    public GameObject charCellPrefab;

    void Start()
    {
        foreach(Character character in characters)
        {
            SpawnCharacterCell(character);
        }
    }

    private void SpawnCharacterCell(Character character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);

        Image artwork = charCell.transform.Find("artwork").GetComponent<Image>();
        TextMeshProUGUI name = charCell.transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();

        artwork.sprite = character.charSprite;
        name.text = character.charName;

        artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;
    }
}
