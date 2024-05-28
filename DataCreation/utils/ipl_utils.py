
import typer
import pandas as pd
import os
import csv
import sys
import utils.xml_to_csv as to_csv
import utils.csv_to_xml as to_xml
import lxml.etree as ET

def xml_to_csv(root: ET.Element):
    id = to_csv.element_text_or_default(root, "ID")
    dlc = to_csv.element_text_or_default(root, "DLC")
    interior_id = to_csv.element_text_or_default(root, "InteriorID")
    is_overworld = to_csv.element_bool_or_default(root, "IsOverworld")
    position = to_csv.vector(root.find("Position"))
    ipl_names = to_csv.element_list(root, "IPLNames")
    entity_sets = to_csv.multipart_list(root, "EntitySets", ["HumanName", "GameName"])
    themes = to_csv.multipart_list(root, "Themes", ["Name", "Index"])
    default_entity_sets = to_csv.element_list(root, "DefaultEntitySets")
    portals = to_csv.multipart_list(root, "Portals", ["ID", "InPosition", "OutPosition", "OutHeading", "PortalType"])

    return [id, dlc, interior_id, is_overworld, position,ipl_names, entity_sets, themes, default_entity_sets, portals]

def csv_to_xml(row, destination_path):
    root = ET.Element("IPL")
    id = to_xml.element_with_text(root, "ID", row["ID"])
    dlc = to_xml.element_with_text(root, "DLC", row["DLC"])
    interior_id = to_xml.element_with_int(root, "InteriorID", row["InteriorID"])
    is_overworld = to_xml.element_with_bool(root, "IsOverworld", row["IsOverworld"])
    position = to_xml.vector(root, "Position", row["Position"])
    ipl_names = to_xml.element_list(root, "IPLNames", row["IPLNames"])
    entity_sets = to_xml.multipart_list(root, "EntitySets", row["EntitySets"], "IPLEntitySet", ["HumanName", "GameName"])
    themes = to_xml.multipart_list(root, "Themes", row["Themes"], "IPLTheme", ["Name", "Index"])
    default_entity_sets = to_xml.element_list(root, "DefaultEntitySets", row["DefaultEntitySets"])
    portals = to_xml.multipart_list(root, "Portals", row["Portals"], "RoomPortal", ["ID", "InPosition", "OutPosition","OutHeading", "PortalType"])
    
    tree = ET.ElementTree(root)
    text = ET.tostring(tree, encoding = "utf-8", xml_declaration = True, pretty_print = True).decode("utf-8")
    with open(os.path.join(destination_path, f"{row['ID']}.xml"), "w") as output:
        output.write(text)