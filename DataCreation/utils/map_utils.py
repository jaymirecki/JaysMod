
import typer
import pandas as pd
import os
import csv
import sys
import utils.xml_to_csv as to_csv
import utils.csv_to_xml as to_xml
import lxml.etree as ET

def csv_to_xml(row, destination_path):
    root = ET.Element("Map")
    id = to_xml.element_with_text(root, "ID", row["ID"])
    is_overworld = to_xml.element_with_bool(root, "IsOverworld", row["IsOverworld"])
    portals = to_xml.multipart_list(root, "IPLs", row["IPLs"], "IPLSettings", ["ID", "Theme"])
    portals = to_xml.multipart_list(root, "Portals", row["Portals"], "Portal", ["ID", "IPL", "RoomPortalID", "Position", "Heading", "ConnectedPortalMap", "ConnectedPortalID"])
    
    tree = ET.ElementTree(root)
    text = ET.tostring(tree, encoding = "utf-8", xml_declaration = True, pretty_print = True).decode("utf-8")
    with open(os.path.join(destination_path, f"{row['ID']}.xml"), "w") as output:
        output.write(text)