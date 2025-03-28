// Placeholder for Azure Function
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

public static class Function1
{
    [FunctionName("Function1")]
    public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    }
    
    public static void GetUser(string userInput, ILogger log)
    {
        string connectionString = "Server=tcp:wedkjnwerfihnj.database.windows.net,1433;Initial Catalog=wedkjnwerfihnj;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
        string query = $"SELECT * FROM Users WHERE Username = '{userInput}'";

        try
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            log.LogInformation($"User: {reader["Username"]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log.LogError($"An error occurred: {ex.Message}");
        }
    }
}