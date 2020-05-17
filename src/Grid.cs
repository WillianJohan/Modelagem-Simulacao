using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int x_Length;
    public int y_Length;
    
    
    private Cell[,] celulas;
    private GameObject cellPrefab;
    private const int cellSize = 1;

    public void generateNewGrid(int x, int y, GameObject cellPrefab)
    {
        this.x_Length = x;
        this.y_Length = y;
        this.cellPrefab = cellPrefab;
        gerarGrid();
    }

    private void gerarGrid()
    {
        //inicializa a variável célula
        celulas = new Cell[x_Length, y_Length];
        
        //Gera o grid com o centro sendo o vetor zero do mundo.
        Vector3 initalPosition = new Vector3(x_Length - 1, y_Length - 1, 0) * cellSize * .5f * -1;

        for (int x = 0; x < x_Length; x++)
        {
            for (int y = 0; y < y_Length; y++)
            {
                GameObject cell = Instantiate(cellPrefab, initalPosition + new Vector3(x, y, 0), Quaternion.identity, null);
                celulas[x,y] = cell.GetComponent<Cell>();
            }
        }
    }
    public void initializeRandomPos()
    {
        int x = Random.Range(0, celulas.GetLength(0));
        int y = Random.Range(0, celulas.GetLength(1));
        celulas[x, y].updateStatus(1);
    }
    
    public void runNextGeneration()
    {
        for (int x = 0; x < celulas.GetLength(0); x++) {
            for (int y = 0; y < celulas.GetLength(1); y++)
            {
                if (celulas[x, y].estado == Cell.Estado.morta) continue;
                int count = getCountOfAlives(getVizinhos(new Vector2Int(x, y)));
                if (count != 0) celulas[x, y].updateStatus(count);
            }
        }
    }

    private int getCountOfAlives(List<Cell> candidates)
    {
        int count = 0;
        foreach (Cell cell in candidates) 
            if (cell.estado.Equals(Cell.Estado.viva)) 
                count++;
        return count;
    }

    private List<Cell> getVizinhos(Vector2Int index)
    {
        List<Cell> vizinhos = new List<Cell>();
        
        if(index.x - 1 >= 0) //Todos vizinhos da esquerda
        {
            vizinhos.Add(celulas[index.x - 1, index.y]);
            if (index.y + 1 < celulas.GetLength(1)) vizinhos.Add(celulas[index.x - 1, index.y + 1]);
            if (index.y - 1 >= 0) vizinhos.Add(celulas[index.x - 1, index.y - 1]);
        }
        if (index.x + 1 < celulas.GetLength(0)) //Todos vizinhos da direita
        {
            vizinhos.Add(celulas[index.x + 1, index.y]);
            if (index.y + 1 < celulas.GetLength(1)) vizinhos.Add(celulas[index.x + 1, index.y + 1]);
            if (index.y - 1 >= 0) vizinhos.Add(celulas[index.x + 1, index.y - 1]);
        }
        if (index.y + 1 < celulas.GetLength(1)) vizinhos.Add(celulas[index.x, index.y + 1]); //Vizinho de cima
        if (index.y - 1 >= 0) vizinhos.Add(celulas[index.x, index.y - 1]); //Vizinho de baixo

        return vizinhos;
    }

    public void destroyCells()
    {
        GameObject[] allCells = GameObject.FindGameObjectsWithTag("Cell");
        foreach (GameObject obj in allCells) Destroy(obj);
    }
}
