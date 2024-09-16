# Smiley-Puzzle
Smiley-Puzzle

Sure, I’ll break down the solution line by line and explain the logic in simple terms so you can understand exactly what’s happening.

### Code Breakdown

```csharp
void squareclick(int n)
{
    // Map the clicked number n (1 to 9) to its 2D position in the `pieces` array.
    int clickedRow = (n - 1) / 3; // This gives the row (i)
    int clickedCol = (n - 1) % 3; // This gives the column (j)
```

1. **Purpose**: The `squareclick(int n)` method is called when a user clicks on a piece. `n` represents which piece was clicked (1 to 9).
   
2. **Mapping `n` to a 2D array**:
   - The `pieces` array is a 3x3 grid that stores the puzzle pieces.
   - The clicked piece, represented by `n`, needs to be mapped to its correct position in the grid. 
   - The formula `(n - 1) / 3` calculates the row (`i`) in which the clicked piece is located.
   - The formula `(n - 1) % 3` calculates the column (`j`) in which the clicked piece is located.
   - For example, if `n = 5`, then `clickedRow = 1` and `clickedCol = 1`, which corresponds to the center tile.

---

```csharp
    // Find the position of the empty piece (which is 0).
    int emptyRow = -1;
    int emptyCol = -1;
    for (int i = 0; i < pieces.GetLength(0); i++)
    {
        for (int j = 0; j < pieces.GetLength(1); j++)
        {
            if (pieces[i, j] == 0)
            {
                emptyRow = i;
                emptyCol = j;
                break;
            }
        }
        if (emptyRow != -1) break; // If found, break out of the loop.
    }
```

3. **Find the empty tile**:
   - The empty piece is represented by `0` in the `pieces` array.
   - `emptyRow` and `emptyCol` are initialized as `-1`, which means the empty tile hasn't been found yet.
   - We use a nested loop to scan the `pieces` array. The outer loop iterates through the rows (`i`), and the inner loop iterates through the columns (`j`).
   - As soon as we find the empty tile (i.e., `pieces[i, j] == 0`), we store its position in `emptyRow` and `emptyCol` and exit the loop using `break`.
   - For example, if the empty tile is at the bottom-right corner, `emptyRow = 2` and `emptyCol = 2`.

---

```csharp
    // Check if the clicked tile is adjacent to the empty tile.
    bool isAdjacent = (Math.Abs(clickedRow - emptyRow) == 1 && clickedCol == emptyCol) ||
                      (Math.Abs(clickedCol - emptyCol) == 1 && clickedRow == emptyRow);
```

4. **Check adjacency**:
   - A valid move in the puzzle only allows you to swap a piece with the empty tile if they are directly next to each other, either vertically or horizontally.
   - The `isAdjacent` variable checks if the clicked tile and the empty tile are adjacent.
   - **Explanation of the condition**:
     - `Math.Abs(clickedRow - emptyRow) == 1 && clickedCol == emptyCol`: This checks if the tiles are in the same column but in adjacent rows (above or below each other).
     - `Math.Abs(clickedCol - emptyCol) == 1 && clickedRow == emptyRow`: This checks if the tiles are in the same row but in adjacent columns (left or right of each other).
   - If either of these conditions is true, then the tiles are adjacent, and we can swap them.

---

```csharp
    // If adjacent, swap the clicked tile with the empty tile.
    if (isAdjacent)
    {
        pieceswap(clickedRow, clickedCol, emptyRow, emptyCol);
        l("Swapped piece " + n + " with the empty piece.\n");
    }
    else
    {
        l("Piece " + n + " is not adjacent to the empty spot, can't swap.\n");
    }
}
```

5. **Swapping the tiles**:
   - If `isAdjacent` is true (i.e., the clicked tile is next to the empty tile), the `pieceswap` function is called to swap their positions in the `pieces` array.
   - `pieceswap(clickedRow, clickedCol, emptyRow, emptyCol)` swaps the clicked tile at `(clickedRow, clickedCol)` with the empty tile at `(emptyRow, emptyCol)`.
   - After the swap, we log a message saying that the swap was successful.
   
6. **Handling invalid moves**:
   - If the clicked tile is not adjacent to the empty tile (i.e., `isAdjacent` is false), we log a message saying the move can't be performed.

### Summary of the Process:
1. **User clicks on a tile**: The clicked tile's number (`n`) is passed to the `squareclick` method.
2. **Find the position of the clicked tile**: The number `n` is converted to the corresponding row and column in the 3x3 grid.
3. **Find the empty tile**: We scan the puzzle to locate the empty tile (represented by `0`).
4. **Check adjacency**: We check if the clicked tile is next to the empty tile.
5. **Swap if adjacent**: If they are adjacent, the tiles are swapped, and the board is updated. If not, the move is rejected.

This logic ensures that only valid moves are allowed in the puzzle, maintaining the rules of how sliding puzzles work.


![image](https://github.com/user-attachments/assets/bc5bd8c3-194b-4a3b-a753-245c31453890)

