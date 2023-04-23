from pickle import TRUE
import subprocess

def property(name: string):
    return f"""
        [XmlIgnore]
        public {name} {name};
        public ComponentKey {name}Key
        {{
            get {{ return new ComponentKey().FromComponent({name}); }}
            set
            {{
                {name} = Global.Presets.Male{name}s[value.ID];
                if ({name} == null)
                {{
                    {name} = new {name}(value.ID, value.CurrentColor);
                }}
                {name}.CurrentColor = value.CurrentColor;
            }}
        }}"""

def allproperties():
    props [
        "Mask",
        "Hands",
        "Lower",
        "Parachute",
        "Shoes",
        "Accessory",
        "Vest",
        "Neck",
        "ShirtOverlay",

        ]

def main():
    constructors()

if __name__ == "__main__":
    main();
