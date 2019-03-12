using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Playground.FunctionApp
{
    public class SomeService : ISomeService
    {
        public void DoSomething()
        {
            Thread.Sleep(5000);
        }
    }

    public interface ISomeService
    {
        void DoSomething();
    }
}
