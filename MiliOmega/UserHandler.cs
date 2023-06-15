using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace MiliOmega
{
    public class UserHandler
    {
        public string logFilePath = "log.txt";
        private string connectionString = "server=localhost;user=root;database=database_name;password=your_password";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="database"></param>
        /// <param name="password"></param>
        public UserHandler(string server,string user,string database, string password)
        {
            connectionString = "server="+server+";user="+user+";database="+database+";password="+password;
            Prihlaseni(connectionString, logFilePath);
        }
        public enum LoginStage
        {
           Guest,
           Succesful,
           Failed,
           None
        }
        static void Prihlaseni(string connectionString, string logFilePath)
        {
            string oddelovac = "\n----------------------------------------------------------------------------------\n";
            LoginStage stage = LoginStage.None;
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                logWriter.WriteLine($"Spusteni programu - {DateTime.Now}");
                bool mainRun = true;
                while(mainRun)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            Console.WriteLine();
                            Console.WriteLine("Přihlášení/Registrace");

                            string username = string.Empty;
                            string password = string.Empty;
                            int input = 0;
                            int strike = 1;
                            int maxStrike = 3;
                            bool run = true;
                            while (run)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Přihlašovací Menu\n1 - Přihlásit se\n2 - Registrovat se\n3 - Pokračovat jako host\n4 - Ukončit program");
                                Console.Write("Vyberte možnost: ");
                                try
                                {
                                    input = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    //throw new Exception("Zadaná Hodnota musí být integer!");
                                    Console.WriteLine("Zadaná Hodnota musí být integer!");
                                    input = 0;
                                }
                                Console.WriteLine();
                                switch (input)
                                {
                                    case 1:
                                        Console.Write("Zadejte uživatelské jméno: ");
                                        username = Console.ReadLine().ToLower();

                                        Console.Write("Zadejte heslo: ");
                                        password = Console.ReadLine();

                                        if (Login(connection, username, password))
                                        {
                                            Console.WriteLine("Přihlášení úspěšné!");
                                            logWriter.WriteLine($"Přihlášení uživatele {username} - {DateTime.Now}");
                                            run = false;
                                            stage = LoginStage.Succesful;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Přihlášení selhalo.");
                                            logWriter.WriteLine($"Přihlášení selhalo pro uživatele {username} - {DateTime.Now}");
                                            if (strike < maxStrike)
                                            {
                                                Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků přihlášení se automaticky ukončí.");
                                                Console.WriteLine("Vyberte celé číslo z nabídky!");
                                                strike++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Dosáhli jste " + strike + " striků. Hra se teď automaticky ukončí.");
                                                run = false;
                                                stage = LoginStage.Failed;
                                            }
                                        }
                                        break;
                                    case 2:
                                        Console.Write("Zadejte uživatelské jméno: ");
                                        username = Console.ReadLine().ToLower();

                                        Console.Write("Zadejte heslo: ");
                                        password = Console.ReadLine();

                                        string hashedPassword = GetSHA1Hash(password);

                                        if (Register(connection, username, hashedPassword))
                                        {
                                            Console.WriteLine("Registrace úspěšná!");
                                            logWriter.WriteLine($"Registrace uživatele {username} - {DateTime.Now}");
                                            run = false;
                                            stage = LoginStage.Succesful;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Registrace selhala.");
                                            logWriter.WriteLine($"Registrace selhala pro uživatele {username} - {DateTime.Now}");
                                            if (strike < maxStrike)
                                            {
                                                Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků přihlášení se automaticky ukončí.");
                                                Console.WriteLine("Vyberte celé číslo z nabídky!");
                                                strike++;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Dosáhli jste " + strike + " striků. Hra se teď automaticky ukončí.");
                                                run = false;
                                                stage = LoginStage.Failed;
                                            }
                                        }
                                        break;
                                    case 3:
                                        username = "Neznámý uživatel";
                                        Console.WriteLine("Přihlášení úspěšné!");
                                        logWriter.WriteLine($"Přihlášení neznámého uživatele - {DateTime.Now}");
                                        run = false;
                                        stage = LoginStage.Guest;
                                        break;
                                    case 4:
                                        run = false;
                                        stage = LoginStage.None;
                                        break;
                                    default:
                                        if (strike < maxStrike)
                                        {
                                            Console.WriteLine("Máte " + strike + " striků, jestli dosáhnete " + maxStrike + " striků přihlášení se automaticky ukončí.");
                                            Console.WriteLine("Vyberte možnost z nabídky!");
                                            Console.WriteLine(oddelovac);
                                            strike++;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Dosáhli jste " + strike + " striků. Program se teď automaticky ukončí.");
                                            run = false;
                                            stage = LoginStage.Failed;
                                        }
                                        break;
                                }

                                connection.Close();
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        logWriter.WriteLine($"Nepovedené připojení do databáze - {DateTime.Now}");
                        Console.WriteLine("Zkontrolujte připojení do databáze! A pak zapněte program znovu.");
                    }
                    // Ukončit nebo pokračovat
                    string exit = "0";
                    if (stage == LoginStage.Succesful || stage == LoginStage.Guest)
                    {
                        MainMenu.Start();
                        Console.WriteLine();
                        Console.WriteLine("1 - Přihlašovací menu\nEnter - Ukončení programu");
                        Console.Write("Vyberte možnost: ");
                        exit = Console.ReadLine();
                        if (exit != "1")
                        {
                            mainRun = false;
                            logWriter.WriteLine($"Program byl ukončen - {DateTime.Now}");
                        }
                    }
                    if (stage == LoginStage.None)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1 - Opakovat připojení\nEnter - Ukončení programu");
                        Console.Write("Vyberte možnost: ");
                        exit = Console.ReadLine();
                        if (exit != "1")
                        {
                            mainRun = false;
                            logWriter.WriteLine($"Program byl ukončen - {DateTime.Now}");
                        }
                        Console.WriteLine(oddelovac);
                    }
                    if (stage == LoginStage.Failed)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Z důvodu neuspěšného přihlášení/registrace by program ukončen.");
                        mainRun = false;
                        logWriter.WriteLine($"Program byl ukončen - {DateTime.Now}");
                    }
                    Console.WriteLine();
                }
                logWriter.WriteLine(oddelovac.ToString());
            }
                
        }

        static bool Login(MySqlConnection connection, string username, string password)
        {
            string query = "SELECT COUNT(*) FROM user WHERE username = @username AND password = @password";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", GetSHA1Hash(password));
            int count = 0;
            try
            {
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return count > 0;
        }

        static bool Register(MySqlConnection connection, string username, string password)
        {
            string query = "INSERT INTO user (username, password) VALUES (@username, @password)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            int rowsAffected = 0;
            try
            {
                rowsAffected = command.ExecuteNonQuery();

            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rowsAffected > 0;
        }

        public static string GetSHA1Hash(string input)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                StringBuilder builder = new StringBuilder();
                try
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = sha1.ComputeHash(inputBytes);


                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return builder.ToString();
            }
        }
    }
    
}
