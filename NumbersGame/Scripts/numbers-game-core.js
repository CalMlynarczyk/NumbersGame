gameBoard = [];
startingPosition = [];
moves = [];
zeroRow = -1;
zeroCol = -1;

// Function Definitions

function refreshGameBoard() {
    $('.game-board-table td').text(function () {
        var value = gameBoard[$(this).parent('tr').get(0).rowIndex][this.cellIndex];

        return (value != 0 ? value : "");
    });

    $('.move-count').text(moves.length);
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
    return ((row === (zeroRow + 1) && col === zeroCol) ||
            (row === (zeroRow - 1) && col === zeroCol) ||
            (row === zeroRow && col === (zeroCol + 1)) ||
            (row === zeroRow && col === (zeroCol - 1)));
}

function swapEmptyCell(row, col) {
    var value = gameBoard[zeroRow][zeroCol];
    gameBoard[zeroRow][zeroCol] = gameBoard[row][col];
    gameBoard[row][col] = value;

    if (value === 0) {
        zeroRow = row;
        zeroCol = col;
    }

    refreshGameBoard();
}

function findZeroElement() {
    for (var row = 0; row < gameBoard.length; row++) {
        for (var col = 0; col < gameBoard[row].length; col++) {
            if (gameBoard[row][col] === 0) {
                zeroRow = row;
                zeroCol = col;
                return;
            }
        }
    }
}

function areArraysEqual(a, b) {
    for (var i = 0; i < a.length; i++) {
        for (var j = 0; j < a[i].length; j++) {
            if (a[i][j] !== b[i][j]) {
                return false;
            }
        }
    }

    return true;
}

function copyArray(arr) {
    var newArr = [[0, 0, 0],
                  [0, 0, 0],
                  [0, 0, 0]];

    for (var i = 0; i < arr.length; i++) {
        for (var j = 0; j < arr[i].length; j++) {
            newArr[i][j] = arr[i][j];
        }
    }

    return newArr;
}