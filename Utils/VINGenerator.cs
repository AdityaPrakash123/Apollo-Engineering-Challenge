using System.Text;

namespace ApolloEngineeringChallenge.Utils
{
    public class VINGenerator
    {
        public string createVIN()
        {
            // Generate a random 17-character alphanumeric string as a placeholder for VIN.
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 17; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }
            return sb.ToString();
        }
    }
}
