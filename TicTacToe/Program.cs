using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    //Player n: Please choose your field! - Active player will choose the field!
    //Please pick a new field - incorrect input! please use another field! - If player enters the already picked field!
    //Please enter a number! - incorrect input! please use another field! - If player enters any other value except the integers!
    //Player n has won! - If any player wins either horizontally, vertically or diagonally at first!
    // -- Press Y to reset the game or N to leave the game.
    class Program
    {
        //-------START--------

        static char[,] playField =
        {
            {'1','2','3'},
            {'4','5','6'},
            {'7','8','9'}
        };
        static char[,] playFieldInitial =
        {
            {'1','2','3'},
            {'4','5','6'},
            {'7','8','9'}
        };
        static int turns = 0;

        static void Main(string[] args)
        {
            
            bool gameActive = true;
            char inputField = '0';
            int player = 2;
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
                        player = CurrentActivePlayer(player); // reset the player to the current player
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
                            player = CurrentActivePlayer(player); // reset the player to the current player
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

                if (winnerChecker == true)
                {
                    DisplayPlayerField();
                    Console.WriteLine("Player {0} has won the game!", player);
                    gameActive = false;
                    Console.Write("Press 'y' to restart the game ");
                    gameActive = Restart(Convert.ToChar(Console.ReadLine()));
                    if (gameActive) { ResetPlayField(); player = CurrentActivePlayer(player);}                   
                }
                else if (turns >= 9)
                {
                    DisplayPlayerField();
                    Console.WriteLine("It's a tie!");
                    gameActive = false;
                    Console.Write("Press 'y' to restart the game ");
                    gameActive = Restart(Convert.ToChar(Console.ReadLine()));
                    if (gameActive) { ResetPlayField(); player = CurrentActivePlayer(player);}                    
                }
                
            } while (gameActive);

        }

        //Display the Player Field!
        static void DisplayPlayerField()
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

        static int CurrentActivePlayer(int player)
        {
            if (player == 2) //Player 1 starts as per the condition on first run
                player = 1;
            else if (player == 1)
                player = 2;
            return player;
        }


        static void UpdatePlayerField(char inputField, int player)
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

        static bool CheckPlayerField(char inputField)
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

        static bool WinnerChecker()
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

        static bool Restart(char input)
        { 
            bool gameActive = false;
            if (input == 'y' || input == 'Y')
                gameActive = true;
            else
                gameActive = false;
            return gameActive;
        }
        static void ResetPlayField()
        {
            Console.Clear();
            playField = playFieldInitial;
            turns = 0; 
        }

        //-------END--------
    }
}
