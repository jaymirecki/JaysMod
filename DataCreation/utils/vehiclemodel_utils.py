
import typer
import pandas as pd
import os
import csv
import sys
import utils.xml_to_csv as to_csv
import utils.csv_to_xml as to_xml
import lxml.etree as ET

def csv_to_xml(row, destination_path):
    root = ET.Element("VehicleModel")
    id = to_xml.element_with_text(root, "Hash", row["Hash"])
    id = to_xml.element_with_text(root, "ID", row["ID"])
    id = to_xml.element_with_text(root, "Subclass", row["Subclass"])
    id = to_xml.element_with_text(root, "GTAMake", row["Subclass"])
    id = to_xml.element_with_text(root, "GTAModel", row["GTAModel"])
    id = to_xml.element_with_text(root, "RWMake", row["RWMake"])
    id = to_xml.element_with_text(root, "RWModel", row["RWModel"])
    id = to_xml.element_with_text(root, "MaxSpeed", row["MaxSpeed"])
    id = to_xml.element_with_text(root, "MaxKnots", row["MaxKnots"])
    id = to_xml.element_with_text(root, "NumberOfSeats", row["NumberOfSeats"])
    
    tree = ET.ElementTree(root)
    text = ET.tostring(tree, encoding = "utf-8", xml_declaration = True, pretty_print = True).decode("utf-8")
    with open(os.path.join(destination_path, f"{row['ID']}.xml"), "w") as output:
        output.write(text)