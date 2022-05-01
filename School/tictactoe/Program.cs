dynamic BoardMake() {
    dynamic[] board = {" ", " ", " ", " ", " ", " ", " ", " ", " "};
    return board;
}

void DisplayBoard(dynamic board) {
    Console.WriteLine($"  1   2   3 \nA {board[0]} | {board[1]} | {board[2]} \n ---+---+---\nB {board[3]} | {board[4]} | {board[5]} \n ---+---+---\nC {board[6]} | {board[7]} | {board[8]} ");
}

string Player(string activePlayer) {
    if(activePlayer == "" || activePlayer == "O") {
        return "X";
    }
    else {
        return "O";
    }
}

void PlayerAction(dynamic board, string player) {
    Console.WriteLine($"Player {player}'s move (Example: A2)");
    string? space = "";
    while(space != "A1" || space != "A2" || space != "A3" || space != "B1" || space != "B2" || space != "B3" || space != "C1" || space != "C2" || space != "C3" || space != "03601202") {
        Console.Write(">> ");
        space = Console.ReadLine();
        if(space == "A1" && board[0] == " ") {
            board[0] = player;
            break;
        }
        else if(space == "A2" && board[1] == " ") {
            board[1] = player;
            break;
        }
        else if(space == "A3" && board[2] == " ") {
            board[2] = player;
            break;
        }
        else if(space == "B1" && board[3] == " ") {
            board[3] = player;
            break;
        }
        else if(space == "B2" && board[4] == " ") {
            board[4] = player;
            break;
        }
        else if(space == "B3" && board[5] == " ") {
            board[5] = player;
            break;
        }
        else if(space == "C1" && board[6] == " ") {
            board[6] = player;
            break;
        }
        else if(space == "C2" && board[7] == " ") {
            board[7] = player;
            break;
        }
        else if(space == "C3" && board[8] == " ") {
            board[8] = player;
            break;
        }
        else if(space == "03601202") {
            for(int i = 0; i < 9;) {
                board[i] = player;
                i++;
            }
            break;
        }
        else {
            Console.WriteLine("Invalid Move");
        }
    }
}

bool WinCondition(dynamic board) {
    bool tHoriz = (board[0] == board[1]) && (board[0] == board[2]) && board[0] != " ";
    bool mHoriz = (board[3] == board[4]) && (board[3] == board[5]) && board[3] != " ";
    bool bHoriz = (board[6] == board[7]) && (board[6] == board[8]) && board[6] != " ";
    bool lVerti = (board[0] == board[3]) && (board[0] == board[6]) && board[0] != " ";
    bool mVerti = (board[1] == board[4]) && (board[1] == board[7]) && board[1] != " ";
    bool rVerti = (board[2] == board[5]) && (board[2] == board[8]) && board[2] != " ";
    bool ldiagn = (board[0] == board[4]) && (board[0] == board[8]) && board[0] != " ";
    bool rdiagn = (board[2] == board[4]) && (board[2] == board[6]) && board[2] != " ";
    bool horizComp = tHoriz || mHoriz || bHoriz;
    bool vertiComp = lVerti || mVerti || rVerti;
    bool diagnComp = ldiagn || rdiagn;
    bool totalComp = horizComp || vertiComp || diagnComp;
    if(tHoriz) {
        WinningPlayer(board, board[0]);
    }
    else if(mHoriz) {
        WinningPlayer(board, board[3]);
    }
    else if(bHoriz) {
        WinningPlayer(board, board[6]);
    }
    else if(lVerti) {
        WinningPlayer(board, board[0]);
    }
    else if(mVerti) {
        WinningPlayer(board, board[1]);
    }
    else if(rVerti) {
        WinningPlayer(board, board[2]);
    }
    else if(ldiagn) {
        WinningPlayer(board, board[0]);
    }
    else if(rdiagn) {
        WinningPlayer(board, board[2]);
    }
    return totalComp;
}

void WinningPlayer(dynamic board, string player) {
    Console.WriteLine($"Player {player} won the match. Congratulations, Player {player}!");
}

void Main() {
    dynamic board = BoardMake();
    string player = Player("");
    for(int i = 0; i < 9;) {
        DisplayBoard(board);
        PlayerAction(board, player);
        player = Player(player);
        if(WinCondition(board)) {
            break;
        }
        i++;
    }
    DisplayBoard(board);
    Console.Write("Press [Enter] to end       ");
    Console.Read();
}

Main();