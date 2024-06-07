
import typer
import pandas as pd
import os
import csv
import sys
import utils.xml_to_csv as to_csv
import utils.csv_to_xml as to_xml
import lxml.etree as ET

def csv_to_xml(row, destination_path):
    root = ET.Element("Worldspace")
    id = to_xml.element_with_text(root, "ID", row["ID"])
    maps = to_xml.element_list(root, "MapIDs", row["MapIDs"])
    travel_points = to_xml.multipart_list(root, "TravelPoints", row["TravelPoints"], "TravelPoint", ["ID", "Position", "ConnectedWorldspaceID", "ConnectedTravelPointID"])
    weather_types = to_xml.element_list(root, "WeatherTypes", row["WeatherTypes"], "WeatherType")
    
    tree = ET.ElementTree(root)
    text = ET.tostring(tree, encoding = "utf-8", xml_declaration = True, pretty_print = True).decode("utf-8")
    with open(os.path.join(destination_path, f"{row['ID']}.xml"), "w") as output:
        output.write(text)