using System;
using Unify.PetStore.Test.Services.Contract;

namespace Unify.PetStore.Test.Services
{
    public class ConsoleLineWriterService : ILineWriterService
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}