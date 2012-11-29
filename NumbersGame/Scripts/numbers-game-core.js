gameBoard = []
moves = [];


// Function Definitions

function refreshGameBoard() {
    $('.game-board-table td').text(function () {
        var value = gameBoard[$(this).parent('tr').get(0).rowIndex][this.cellIndex];

        return (value != 0 ? value : "");
    });

    $('.move-count').text(moveCount);
}

function isGameStarted() {
    return (moves.length > 0);
}

function isGameFinished() {
    var goalArray = [[1, 2, 3],
                     [4, 5, 6],
                     [7, 8, 0]];

    return areArraysEqual(gameBoard, goalArray);
}

function areArraysEqual(a, b) {
    // Assume the arrays are of equal dimensions
    for (var i = 0; i < a.length; i++) {
        for (var j = 0; j < a[i].length; j++) {
            if (a[i][j] !== b[i][j]) {
                return false;
            }
        }
    }

    return true;
}

function checkAdjacentCells(row, col) {
    try {
        return (gameBoard[row - 1][col] === 0) ||
               (gameBoard[row + 1][col] === 0) ||
               (gameBoard[row][col - 1] === 0) ||
               (gameBoard[row][col + 1] === 0);
    } catch (ex) {
        try {
            return (gameBoard[row - 1][col] === 0) ||
                   (gameBoard[row][col - 1] === 0) ||
                   (gameBoard[row][col + 1] === 0);
        } catch (ex) {
            return (gameBoard[row + 1][col] === 0) ||
                   (gameBoard[row][col - 1] === 0) ||
                   (gameBoard[row][col + 1] === 0);
        }
    }
}

function swapEmptyCell(row, col) {
    for (var i = 0; i < gameBoard.length; i++) {
        for (var j = 0; j < gameBoard[i].length; j++) {
            if (gameBoard[i][j] === 0) {
                gameBoard[i][j] = gameBoard[row][col];
                gameBoard[row][col] = 0;
                refreshGameBoard();
                return;
            }
        }
    }

    
}