using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.HLO
{
    public class HelloSolution
    {
        public string Hello(string? friendName)
        {
            if (string.IsNullOrEmpty(friendName))
            {
                return "Hello!";
            }
            return $"Hello, {friendName}!";
        }
    }
}
