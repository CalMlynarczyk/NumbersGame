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

                if (checkRegexp($("#name"), /^[0-9A-Za-z_]+$/i, "Name may consist of letters and numbers.")) {
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

    startingPosition = gameBoard.slice(0);
    moves = [];
    //moves = [[1, 0]];
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

        array[Math.floor(i / 3)][i % 3] = value;
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
        swapEmptyCell(row, col);
        moves.push([row, col]);
        refreshGameBoard();
    }

    if (isGameFinished()) {
        // TODO: Display high score form
        alert("Winner!");
        console.log(moves);
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
// TODO: Do not use exceptions
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
                return;
            }
        }
    }
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
