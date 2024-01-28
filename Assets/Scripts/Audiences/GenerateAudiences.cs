using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerateAudiences : MonoBehaviour
{
    public GameObject audience;
    public int rowCount;
    public float rowSpacing;
    public int columnCount;
    public float columnSpacing;
    private Vector3 position = new Vector3(0, 0, 0);

    public Transform playerTrans;
    public int playerRow = 2;
    public int playerColumn = 3;

    public void OnEnable()
    {
        ReGenerate();
    }

    private void ReGenerate()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                position.x = (i + 1) * columnSpacing + transform.position.x;
                position.y = (j + 1) * rowSpacing + transform.position.y;
                if(i == playerColumn && j == playerRow)
                {
                    playerTrans.position = position;
                    playerTrans.Find("Spine").GetComponent<SortingGroup>().sortingOrder = (rowCount - j)*2;
                    playerTrans.Find("Chair").GetComponent<SpriteRenderer>().sortingOrder = (rowCount - j)*2 - 1;
                    continue;
                }
                GameObject instantiatedPrefab = Instantiate(audience, position, Quaternion.identity);
                instantiatedPrefab.transform.Find("Spine").GetComponent<SortingGroup>().sortingOrder = (rowCount - j)*2;
                instantiatedPrefab.transform.Find("Chair").GetComponent<SpriteRenderer>().sortingOrder = (rowCount - j)*2 - 1;
                instantiatedPrefab.transform.SetParent(this.transform);
            }
        }
    }

}
