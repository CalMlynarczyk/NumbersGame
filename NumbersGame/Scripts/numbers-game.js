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

    startingPosition = copyArray(gameBoard);
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



function checkRegexp(o, regexp, n) {
    if (!(regexp.test(o.val()))) {
        o.addClass("ui-state-error");
        $(".validateTips").text(n);
        return false;
    } else {
        return true;
    }
}
