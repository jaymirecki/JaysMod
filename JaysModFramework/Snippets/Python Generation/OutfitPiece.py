from pickle import TRUE
import subprocess

def properties(component: bool = TRUE):
    base = f"""
            Name = name;
            Index = index;
            Colors = colors;
            CurrentColor = currentColor;"""
    if component:
        base = base + "\n            internal OutfitComponents ComponentSlot = OutfitComponents.Mask;"

def constructors(name: str):
    return f"""
        public bool HideHair = false;
        public {name}() : this(defaultName, defaultID, defaultColors, defaultCurrentColor){{ }}
        public {name}(int index) : this(index, defaultCurrentColor) {{ }}
        public {name}(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) {{ }}
        public {name}(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) {{ }}
        public {name}(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {{
            ComponentSlot = OutfitComponents.{name};
        }}"""

def main():
    constructors()

if __name__ == "__main__":
    main();
