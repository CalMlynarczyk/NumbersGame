$(document).ready(function () {
    $(".view-controls button.start").click(function () {
        goToStart();
        $('.move-count').text(currentMove);
    });

    $(".view-controls button.end").click(function () {
        goToEnd();
        $('.move-count').text(currentMove);
    });

    $(".view-controls button.back").click(function () {
        goBack();
        $('.move-count').text(currentMove);
    });

    $(".view-controls button.forward").click(function () {
        goForward();
        $('.move-count').text(currentMove);
    });

    goToStart();
    refreshGameBoard();
    $('.move-count').text(currentMove);
});


// Function Definitions

function goToStart() {
    gameBoard = copyArray(startingPosition);
    findZeroElement();
    currentMove = 0;
    refreshGameBoard();
}

function goToEnd() {
    gameBoard = [[1, 2, 3],
                 [4, 5, 6],
                 [7, 8, 0]];
    findZeroElement();
    currentMove = moves.length;
    refreshGameBoard();
}

function goForward() {
    if (currentMove < moves.length) {
        swapEmptyCell(moves[currentMove][0], moves[currentMove][1]);
        currentMove++;
        refreshGameBoard();
    }   
}

function goBack() {
    if (currentMove > 0) {
        currentMove--;
        swapEmptyCell(moves[currentMove][0], moves[currentMove][1]);
        refreshGameBoard();
    }
}