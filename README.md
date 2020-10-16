# Write a SOA application that implements the TIC-TAC-toe game. 
#### Players – the two users who connected first, the other users are observers.
#### The WCF service must be created as a class library with a duplex contract. Service operation:
1. connecting a player;
2. disconnection of the player;
3. attempted move (returns false if the move is not possible);
4. sending a move.
#### The callback contract must contain an operation to display the client's progress.
#### To host the created service create a Windows service.
#### Implement the client application as a Windows Forms or WPF project.
#### Provide for the continuation of the game if one of the players is disabled.
