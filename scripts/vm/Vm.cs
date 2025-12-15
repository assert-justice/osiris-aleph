namespace Osiris.Vm
{
    public class Vm
    {
        public static void Example()
        {
            var eng = IronPython.Hosting.Python.CreateEngine();
            var scope = eng.CreateScope();
            string src = OsirisSystem.ReadFile("scripts/vm/python_scripts/example.py");
            eng.Execute(src, scope);
            dynamic greetings = scope.GetVariable("greetings");
            OsirisSystem.Log(greetings("world"));
        }
    }
}
