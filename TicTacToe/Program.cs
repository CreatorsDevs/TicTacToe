using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    // BASIC MECHANICS OF TIC TAC TOE GAME - (No restart functionality.)
    class Program
    {
        //-------START--------
        static char[,] playField =
        {
            {'1','2','3'},
            {'4','5','6'},
            {'7','8','9'}
        };
        static int turns = 0;
        static int player = 2;
        static void Main(string[] args)
        {
            
            bool gameActive = true;
            char inputField = '0'; 
            bool isFieldAvailable = true;
            bool winnerChecker = false;
            
            do
            {
                player = CurrentActivePlayer(player); //Player 1 starts
                DisplayPlayerField();
                Console.Write("Player {0}: Please Choose Your Field! ", player);                
                try
                {
                    inputField = Convert.ToChar(Console.ReadLine());

                    if (inputField == '0' || char.IsDigit(inputField) == false)
                    {
                        Console.WriteLine("Please enter a valid input field from 1 to 9!");
                        player = CurrentActivePlayer(player); //Reset the player to the current player
                        Console.ReadKey();
                    }
                    else
                    {
                        isFieldAvailable = CheckPlayerField(inputField);
                        if(isFieldAvailable == true)
                        {
                            UpdatePlayerField(inputField, player);
                        }
                        else
                        {
                            player = CurrentActivePlayer(player); //Reset the player to the current player
                            Console.WriteLine("Selected field has already been taken - Please use another field!");        
                            Console.ReadKey(); 
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect input - Please enter a valid input field!");
                    player = CurrentActivePlayer (player);
                    Console.ReadKey();
                }

                winnerChecker = WinnerChecker(); 

                if (winnerChecker == true) //If any player wins.
                {
                    DisplayPlayerField();
                    Console.WriteLine("Player {0} has won the game!", player);
                    Console.ReadKey();
                    gameActive = false;                
                }
                else if (turns >= 9) //If it's a tie game.
                {
                    DisplayPlayerField();
                    Console.WriteLine("It's a tie!");
                    Console.ReadKey();
                    gameActive = false;
                }               
            } while (gameActive);
        }
 
        static void DisplayPlayerField() //Display the Player Field!
        {
            Console.Clear();
            Console.WriteLine("TIC TAC TOE\n");
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    if(j == playField.GetLength(1)-1)
                        Console.Write(" " + playField[i,j] + " ");
                    else
                        Console.Write(" " + playField[i, j] + " |");
                }
                if (i == playField.GetLength(0) - 1)
                    Console.WriteLine("\n");
                else
                    Console.WriteLine("\n-----------");
            }
        }

        static int CurrentActivePlayer(int player) //Set the current active player as per the resepective player's turn.
        {
            if (player == 2) //Player 1 starts as per the condition on first run.
                player = 1;
            else if (player == 1)
                player = 2;
            return player;
        }


        static void UpdatePlayerField(char inputField, int player) //Update the playerfield to display the game progress.
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(0); j++)
                {
                    if(inputField == playField[i, j] && player == 1)
                    {
                            playField[i, j] = 'X'; //Player 1 sign is 'X'
                    }
                    else if(inputField == playField[i, j] && player == 2)
                    {
                            playField[i, j] = 'O'; //Player 2 sign is 'O'
                    }                   
                }
            }
            turns++;
        }

        static bool CheckPlayerField(char inputField) //Check the available player field for players.
        {
            bool isFieldAvailable = true;

            if ((inputField == '1') && (playField[0, 0] == '1'))
                isFieldAvailable = true;
            else if ((inputField == '2') && (playField[0, 1] == '2'))
                isFieldAvailable = true;
            else if ((inputField == '3') && (playField[0, 2] == '3'))
                isFieldAvailable = true;
            else if ((inputField == '4') && (playField[1, 0] == '4'))
                isFieldAvailable = true;
            else if ((inputField == '5') && (playField[1, 1] == '5'))
                isFieldAvailable = true;
            else if ((inputField == '6') && (playField[1, 2] == '6'))
                isFieldAvailable = true;
            else if ((inputField == '7') && (playField[2, 0] == '7'))
                isFieldAvailable = true;
            else if ((inputField == '8') && (playField[2, 1] == '8'))
                isFieldAvailable = true;
            else if ((inputField == '9') && (playField[2, 2] == '9'))
                isFieldAvailable = true;
            else
                isFieldAvailable = false;

            return isFieldAvailable;
        }

        static bool WinnerChecker() //Check the winner of the game.
        {
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                if (playField[i, 0] == playField[i, 1] && playField[i, 1] == playField[i, 2]) // Checks for horizontal cases
                    return true;
                if (playField[0, i] == playField[1, i] && playField[1, i] == playField[2, i]) // Checks for vertical cases
                    return true;
            }
            if (playField[0, 0] == playField[1, 1] && playField[1, 1] == playField[2, 2]) // Checks for diagonal cases
                return true;
            if (playField[0, 2] == playField[1, 1] && playField[1, 1] == playField[2, 0])
                return true;
            return false;
        }
        //-------END--------
    }
}
