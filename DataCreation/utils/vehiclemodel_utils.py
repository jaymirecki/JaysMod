
import typer
import pandas as pd
import os
import csv
import sys
import utils.xml_to_csv as to_csv
import utils.csv_to_xml as to_xml
import lxml.etree as ET

def xml_to_csv(root: ET.Element):
    model_hash = to_csv.element_text_or_default(root, "Hash")
    id = to_csv.element_text_or_default(root, "ID")
    vehicle_class = to_csv.element_text_or_default(root, "Class")
    subclass = to_csv.element_text_or_default(root, "Subclass")
    gta_make = to_csv.element_text_or_default(root, "GTAMake")
    gta_model = to_csv.element_text_or_default(root, "GTAModel")
    rw_make = to_csv.element_text_or_default(root, "RWMake")
    rw_model = to_csv.element_text_or_default(root, "RWModel")
    max_speed = to_csv.element_text_or_default(root, "MaxSpeed")
    max_knots = to_csv.element_text_or_default(root, "MaxKnots")
    number_of_seats = to_csv.element_text_or_default(root, "NumberOfSeats")

    id = to_csv.element_text_or_default(root, "ID")

    return [model_hash, id, vehicle_class, subclass, gta_make, gta_model, rw_make, rw_model, max_speed, max_knots, number_of_seats]

def csv_to_xml(row, destination_path):
    root = ET.Element("VehicleModel")
    model_hash = to_xml.element_with_text(root, "Hash", row["Hash"])
    id = to_xml.element_with_text(root, "ID", row["ID"])
    vehicle_class = to_xml.element_with_text(root, "Class", row["Class"])
    subclass = to_xml.element_with_text(root, "SubClass", row["SubClass"])
    gta_make = to_xml.element_with_text(root, "GTAMake", row["GTAMake"])
    gta_model = to_xml.element_with_text(root, "GTAModel", row["GTAModel"])
    rw_make = to_xml.element_with_text(root, "RWMake", row["RWMake"])
    rw_model = to_xml.element_with_text(root, "RWModel", row["RWModel"])
    max_speed = to_xml.element_with_float(root, "MaxSpeed", row["MaxSpeed"])
    max_knots = to_xml.element_with_float(root, "MaxKnots", row["MaxKnots"])
    number_of_seats = to_xml.element_with_int(root, "NumberOfSeats", row["NumberOfSeats"])
    
    tree = ET.ElementTree(root)
    text = ET.tostring(tree, encoding = "utf-8", xml_declaration = True, pretty_print = True).decode("utf-8")
    with open(os.path.join(destination_path, f"{row['ID']}.xml"), "w") as output:
        output.write(text)