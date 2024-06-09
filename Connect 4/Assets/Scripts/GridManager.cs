using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoonActive.Connect4;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Disk[] disks;
    [SerializeField] private WinPopup winPopup;

    private int numOfPlayers = 2;
    private int numOfComps = 2;
    private bool[] isPlayerComp;
    private bool canPlay = false;
    private int winPlayer = -9;
    private IGrid gameGrid;
    private int[,] slots;
    private int playerInt = 0;
    private List<int> availableColumns;

    [SerializeField] private AudioSource DropDiskSound;

    void Start()
    {
        gameGrid = GetComponent<IGrid>();
        gameGrid.ColumnClicked += CanDropDiskCheck;

        slots = new int[6, 7];

        availableColumns = new List<int>(); // a List of columns which are not full.
        for (int i = 0; i < slots.GetLength(1); i++)
            availableColumns.Add(i);


        isPlayerComp = new bool[numOfPlayers]; // an array presenting which players are controlled by the computer.
        for (int i = 0; i < isPlayerComp.Length; i++)
            isPlayerComp[i] = false;

        int index = 0;
        while (numOfComps > 0) // I made it like this so the computers will always play after humans.
        {
            isPlayerComp[isPlayerComp.Length-1 + index] = true;
            index--;
            numOfComps--;
        }

        ResetBoard();
    }
    private void CanDropDiskCheck(int desiredColumn)
    {
        if (slots[5, desiredColumn] != -9 || !canPlay) return; // can not play if the column is full or if the previous disk is still falling.

        DropNewDisk(desiredColumn);
    }
    private void DropNewDisk(int chosenColumn)
    {
        DropDiskSound.Play();
        canPlay = false;

        Disk diskPrefab = disks[playerInt];
        diskPrefab.GetComponent<AssignGridManager>().gridManager = this;

        int i = 0;

        while (slots[i, chosenColumn] != -9)
            i++;

        slots[i, chosenColumn] = playerInt;

        if (i == 5)
        {
            int index = 0;
            while (availableColumns[index] != chosenColumn)
                index++;

            availableColumns.RemoveAt(index);

            if (availableColumns.Count == 0)
                winPlayer = -10; // -10 signals a draw, because the board is full.
        }

        if (CheckForWin(i, chosenColumn))
            winPlayer = playerInt;

        gameGrid.Spawn(diskPrefab, chosenColumn, i);
    }
    private bool CheckForWin(int x, int y)
    {
        int counter = 1; int i = 1;
        while (x - i >= 0 && slots[x - i, y] == playerInt) { counter++; i++; } // Check Down
        if (counter == 4) return true;

        counter = 1; i = 1;
        while (y - i >= 0 && slots[x, y - i] == playerInt) { counter++; i++; } // Check Left
        i = 1;
        while (y + i < slots.GetLength(1) && slots[x, y + i] == playerInt) { counter++; i++; } // Check Right
        if (counter >= 4) return true;

        counter = 1; i = 1;
        while (y - i >= 0 && x + i < slots.GetLength(0) && slots[x + i, y - i] == playerInt) { counter++; i++; } // Left and Up
        i = 1;
        while (y + i < slots.GetLength(1) && x - i >= 0 && slots[x - i, y + i] == playerInt) { counter++; i++; } // Right and Down
        if (counter >= 4) return true;

        counter = 1; i = 1;
        while (y + i < slots.GetLength(1) && x + i < slots.GetLength(0) && slots[x + i, y + i] == playerInt) { counter++; i++; } // Right and Up
        i = 1;
        while (x - i >= 0 && y - i >= 0 && slots[x - i, y - i] == playerInt) { counter++; i++; } // Left and Down
        if (counter >= 4) return true;

        return false;
        //try
    }
    public void EndTurn()
    {
        if (winPlayer != -9)
        {
            // show a win announcement popup screen.

            if (winPlayer == -10) // a draw.
                winPopup.Initialize(winPlayer, null);
            else
                winPopup.Initialize(winPlayer, disks[winPlayer].GetComponent<Image>().sprite);

            return;
        }

        if (playerInt == numOfPlayers - 1)
            playerInt = 0;
        else
            playerInt++;

        NextTurn();
    }
    private void NextTurn()
    {
        if (isPlayerComp[playerInt] == true)
        {
            int col = availableColumns[UnityEngine.Random.Range(0, availableColumns.Count)];
            DropNewDisk(col);
        }
        else
            canPlay = true;
    }

    public void ResetBoard()
    {
        for (int x = 0; x < slots.GetLength(0); x++)
        {
            for (int y = 0; y < slots.GetLength(1); y++)
            {
                slots[x, y] = -9;
            }
        }
        playerInt = 0;
        winPlayer = -9;
        NextTurn();
    }
    public void SetGameParameters(int AllPlayers, int Comps) 
    {
        // this will determine how many players will play,
        // and how many of this players will be controlled by the computer.
        // the system supports up to 4 players, but it can easily be extend for more.

        numOfPlayers = AllPlayers;
        numOfComps = Comps;
    }
}
