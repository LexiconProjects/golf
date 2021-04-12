using System;


namespace Golf
{
    class Game
    {
        const int MIN_CUP_DISTANCE = 100;
        const int MAX_CUP_DISTANCE = 2000;
        const int MAX_NUMBER_OF_SWINGS = 10;
       
        const double GRAVITY = 9.8d;
        private double swingAngle, swingForce;

        private double ballPosition = 0;
        private int cupPosition;
        private int par;

        private int numberOfSwings=1;
        private bool swingForward = true;
        
        private string gameLog = "";
        private string[] swingInfo = new string[MAX_NUMBER_OF_SWINGS];
        Random rand = new Random();

        public Game()
        {
            cupPosition = rand.Next(MIN_CUP_DISTANCE, MAX_CUP_DISTANCE);
            DeterminePar(cupPosition);
            Console.WriteLine();
            Console.WriteLine($"The par of the cup is: {par}");
            Console.WriteLine($"The maximum number of swings allowed is {MAX_NUMBER_OF_SWINGS}");
            PrintCourse(cupPosition,ballPosition);

            try
            {
                Swing();
            }
            catch (OutOfBoundaryException e)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ball is too far away from cup. {(int)ballPosition}m > {(int)cupPosition}m" + "\n" + e);
                Console.ResetColor();

            }catch (MaximumNumberOfSwingsException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("You reached the maximum number of swings" + "\n" + ex);
                Console.ResetColor();
            }
            
            
        }

        private void DeterminePar(int cupPosition)
        {
            if (cupPosition < 241)
                par = 3;
            else
                if (cupPosition > 240 && cupPosition < 451)
                par = 4;
            else
                if (cupPosition > 450 && cupPosition < 651)
                par = 5;
            else
                if (cupPosition > 650 && cupPosition < 1001)
                par = 6;
            else
                if (cupPosition > 1000 && cupPosition < 2001)
                par = 8;

        }
         
        private void PrintCourse(int cupPosition, double ballPosition)
        {
            Console.WriteLine();
            Console.WriteLine("                                  /|");
            Console.WriteLine("                                 /_|");
            Console.WriteLine("                                   |");
            Console.WriteLine("o__________________________________|_\n");
            Console.WriteLine($"^Ball position is: {(int)ballPosition}m.");
            Console.WriteLine($"Total distance to cup is:         {(int)(cupPosition -ballPosition)}m.");
            Console.WriteLine("______________________________________\n");
        }

        private void Swing()
        {
            bool swingIsValid = true;
            bool won = false;

            while (swingIsValid && numberOfSwings < MAX_NUMBER_OF_SWINGS+1)
            {

                if (ballPosition > cupPosition + 1000)
                {
                    throw new OutOfBoundaryException();

                }
                else if ((cupPosition - ballPosition)<1 && (cupPosition - ballPosition)>-1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your ball is in the cup!\n");
                    Console.ResetColor();

                    swingIsValid = false;
                    won = true;

                } else
                {
                    Console.WriteLine($"Current swing: {numberOfSwings}");

                    if (won != true)
                    {
                        if ((cupPosition - ballPosition) > -999 && (cupPosition - ballPosition) < -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ball is past the cup. Now you swing in the opposite direction.");
                            Console.ResetColor();
                            swingForward = false;
                        }
                        else
                            swingForward = true;

                        SetSwingInfo();
                        double prevBallPosition = ballPosition;
                        UpdateBallPosition(swingForward);
                        UpdateSwingInfo(numberOfSwings - 1, prevBallPosition);
                        numberOfSwings++;
                        PrintCourse(cupPosition, ballPosition);
                    }
                    
                }

            }

            if ((cupPosition - ballPosition) < 1 && (cupPosition - ballPosition) > -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your ball is in the cup!\n");
                Console.ResetColor();
                won = true;
            }

            if (won)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                UpdateGameLog();
                Console.WriteLine(gameLog);
                PrintSwings();
                Console.ResetColor();

            }else if (numberOfSwings > MAX_NUMBER_OF_SWINGS - 1 )
            {
                throw new MaximumNumberOfSwingsException();
            }

        }

        private void SetSwingInfo()
        {
            
            bool angleIsValid;
            do
            {
                Console.Write("Enter desired angle in degrees(0-45): ");
                try
                {
                    swingAngle = Convert.ToDouble(Console.ReadLine());
                    angleIsValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid angle!");
                    angleIsValid = false;
                }
                
                if (swingAngle > 45)
                {
                    Console.WriteLine("Angle exceeded the limit!");
                    angleIsValid = false;

                } else if (swingAngle < 0)
                {
                    Console.WriteLine("Angle under the limit!");
                    angleIsValid = false;
                }

            } while (!angleIsValid);


            bool forceIsValid;

            do {
                Console.Write("Enter desired force of the swing(max 100m/s): ");
                try
                {
                    swingForce = Convert.ToDouble(Console.ReadLine());
                    forceIsValid = true;

                    if (swingForce > 100)
                    {
                        Console.WriteLine("Swing force exceeded the limit!");
                        forceIsValid = false;

                    } else if (swingForce < 0)
                    {
                        Console.WriteLine("Swing force under the limit!");
                        forceIsValid = false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid swing force!");
                    forceIsValid = false;
                }
            } while (!forceIsValid);
            
            
            Console.WriteLine();

        }

        private void UpdateBallPosition(bool goForward)
        {
            if (goForward)
            {
                ballPosition += Math.Pow(swingForce, 2) / GRAVITY * Math.Sin(2 * AngleInRad());
            } else
                ballPosition -= Math.Pow(swingForce, 2) / GRAVITY * Math.Sin(2 * AngleInRad());
;
        }

        private void UpdateSwingInfo(int swing, double prevBallPosition)
        {
            swingInfo[swing] = $"At swing number {swing+1}, with an angle of {swingAngle} degrees " +
                            $"and a speed of {swingForce} m/s, the ball travelled {Math.Abs(ballPosition - prevBallPosition)}m.";

        }

       
        private double AngleInRad()
        {
            return swingAngle * (Math.PI / 180);
        }

        private void UpdateGameLog()
        {
            gameLog += $"Distance to cup: {cupPosition}m\n" +
                $"Par of cup: {par}\n" +
                $"Number of swings: {numberOfSwings-1}\n";
            
        }

        private void PrintSwings()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Game details:");
            for (int swing = 0; swing < swingInfo.Length; swing++)
                Console.WriteLine(swingInfo[swing]);

        }


    }

    
}



