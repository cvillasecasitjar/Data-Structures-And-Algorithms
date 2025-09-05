using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

// Models to match API response
public class Root
{
    public List<User> Results { get; set; }
}

public class User
{
    public Name Name { get; set; }
    public string Email { get; set; }
    public Login Login { get; set; }
}

public class Name
{
    public string First { get; set; }
    public string Last { get; set; }
}

public class Login
{
    public string Username { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();

        try
        {
            // Request 10 random users
            var data = await client.GetFromJsonAsync<Root>("https://randomuser.me/api/?results=10");

            if (data?.Results != null)
            {
                List<User> users = data.Results;

                // Sort users alphabetically by username
                users.Sort((u1, u2) => string.Compare(u1.Login.Username, u2.Login.Username, StringComparison.Ordinal));

                Console.WriteLine("Sorted Usernames:\n");
                foreach (var user in users)
                {
                    Console.WriteLine(user.Login.Username);
                }

                // Ask the user to input a username to search
                Console.WriteLine("\nEnter a username to search:");
                string searchUsername = Console.ReadLine();

                User foundUser = BinarySearch(users, searchUsername);

                if (foundUser != null)
                {
                    Console.WriteLine($"\nUser found:");
                    Console.WriteLine($"Name: {foundUser.Name.First} {foundUser.Name.Last}");
                    Console.WriteLine($"Email: {foundUser.Email}");
                    Console.WriteLine($"Username: {foundUser.Login.Username}");
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
            }
            else
            {
                Console.WriteLine("No users found in API response.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching users: {ex.Message}");
        }
    }

    // Binary search method
    static User BinarySearch(List<User> users, string username)
    {
        int left = 0;
        int right = users.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = string.Compare(users[mid].Login.Username, username, StringComparison.Ordinal);

            if (comparison == 0)
            {
                return users[mid]; // Found
            }
            else if (comparison < 0)
            {
                left = mid + 1; // Search right half
            }
            else
            {
                right = mid - 1; // Search left half
            }
        }

        return null; // Not found
    }
}
