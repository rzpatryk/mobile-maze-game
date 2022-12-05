using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFixGridCellSize : MonoBehaviour
{
    private GridLayoutGroup grid;
    private RectTransform rect;
    private int rows = 1;
    private int columns = 1;
    [SerializeField]
    private int elements;
    [SerializeField]
    private float spacingX;
    [SerializeField]
    private float spacingY;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        grid.spacing = new Vector2(spacingX, spacingY);
        rect = GetComponent<RectTransform>();
        if (grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            rows = grid.constraintCount;
            columns = elements / rows;
            float cellSizeX = ((rect.rect.width * 0.8f) / columns) - spacingX;
            float cellSizeY = ((rect.rect.height * 0.8f) / rows) - spacingY;
            grid.cellSize = new Vector2(cellSizeX, cellSizeY);

        }
        else if(grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            columns = grid.constraintCount;
            rows = elements / columns;
            float cellSize = Mathf.Round((rect.rect.height - (grid.padding.left + grid.padding.right) - (columns - 1)) / rows);
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
        
        

        Debug.Log(grid.constraint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
