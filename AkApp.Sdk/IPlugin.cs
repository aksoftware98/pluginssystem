using System;
using System.Collections.Generic;
using System.Text;

namespace AkApp.Sdk
{
    public interface IPlugin
    {

        string Title { get; }
        string Description { get; }

        void DoSomething();

    }
}
