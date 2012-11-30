$(document).ready(function () {
    $("#dialog-form").dialog({
        autoOpen: false,
        height: 200,
        width: 350,
        modal: true,
        resizable: false,
        draggable: false,
        buttons: {
            "Submit": function () {
                $("#name").removeClass("ui-state-error");

                if (checkRegexp($("#name"), /^[0-9A-Za-z_]{1,20}$/, "Name may consist of letters, numbers, and underscores.")) {
                    //submitScore($("#name").val());
                    document.score.startingPosition.value = startingPosition;
                    document.score.moves.value = moves;
                    document.score.submit();
                    $(this).dialog("close");
                }
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $("#name").val("").removeClass("ui-state-error");
            newGame();
        }
    });

    $("td").click(function () {
        var row = $(this).parent("tr").get(0).rowIndex;
        var col = this.cellIndex;

        makeMove(row, col);
    });

    newGame();
});


// Function Definitions

function newGame() {
    do { // Do not create a winning configuration right away
        gameBoard = generateStartingPositions();
    } while (isGameFinished());

    findZeroElement();

    startingPosition = gameBoard.slice(0);
    moves = [];
    refreshGameBoard();
}

function generateStartingPositions() {
    var array = [[0, 0, 0],
                 [0, 0, 0],
                 [0, 0, 0]];
    var values = [0, 1, 2, 3, 4, 5, 6, 7, 8];
    var length = values.length;

    for (var i = 0; i < length; i++) {
        var index = Math.floor(Math.random() * values.length);
        var value = values[index];

        var row = Math.floor(i / 3);
        var col = i % 3;

        array[row][col] = value;
        values.splice(index, 1);
    }

    return array;
}

function refreshGameBoard() {
    $('.game-board-table td').text(function () {
        var value = gameBoard[$(this).parent('tr').get(0).rowIndex][this.cellIndex];

        return (value != 0 ? value : "");
    });

    $('.move-count').text(moves.length);
}

function makeMove(row, col) {
    if (gameBoard[row][col] === 0) {
        return;
    }

    if (checkAdjacentCells(row, col)) {
        moves.push([row, col]);
        swapEmptyCell(row, col);
    }

    if (isGameFinished()) {
        alert("Winner!");
        $('#dialog-form').dialog('open');
    }
}

function isGameFinished() {
    var goalArray = [[1, 2, 3],
                     [4, 5, 6],
                     [7, 8, 0]];

    return areArraysEqual(gameBoard, goalArray);
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

function checkAdjacentCells(row, col) {
    return ((row === (zeroRow + 1) && col === zeroCol) ||
            (row === (zeroRow - 1) && col === zeroCol) ||
            (row === zeroRow && col === (zeroCol + 1)) ||
            (row === zeroRow && col === (zeroCol - 1)));
}

function swapEmptyCell(row, col) {
    gameBoard[zeroRow][zeroCol] = gameBoard[row][col];
    gameBoard[row][col] = 0;
    zeroRow = row;
    zeroCol = col;

    refreshGameBoard();
}

function checkRegexp(o, regexp, n) {
    if (!(regexp.test(o.val()))) {
        o.addClass("ui-state-error");
        $(".validateTips").text(n);
        return false;
    } else {
        return true;
    }
}

function submitScore(name) {
    $.post("/Home/SubmitScore",
           { "name": name,
             "startingPosition": $.param(startingPosition),
             "moves": moves
           });
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
