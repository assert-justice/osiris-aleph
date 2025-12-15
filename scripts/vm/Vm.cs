using System;
using Jint;

namespace Osiris.Vm
{
    public class Vm
    {
        public static void JintExample()
        {
            var engine = new Engine()
                .SetValue("log", new Action<string>(OsirisSystem.Log));
                
            engine.Execute(@"
                function hello() { 
                    log('Hello World');
                };
            
                hello();
            ");
        }
    }
}
