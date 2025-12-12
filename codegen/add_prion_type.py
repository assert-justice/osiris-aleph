import sys, pathlib

def gen_template(class_name: str):
    return f"""using System.Text.Json.Nodes;

namespace Prion
{{
    public class Prion{class_name} : PrionNode
    {{
        public Prion{class_name}() : base(PrionType.{class_name}){{}}
        public override JsonNode ToJson()
        {{
            throw new System.NotImplementedException();
        }}
        public override string ToString()
        {{
            throw new System.NotImplementedException();
        }}
    }}
}}
"""
    
def main():
    print(sys.argv)
    if len(sys.argv) < 2:
        print("a script to add new types to ./scripts/prion")
        return
    class_name = sys.argv[1]
    template = gen_template(class_name)
    path = pathlib.Path(f"./scripts/prion/Prion{class_name}.cs")
    if path.exists():
        print(f"A file already exists at {path}")
        return
    with open(path, "w", encoding="utf-8") as f:
        f.write(template)

if __name__ == "__main__":
    main()
