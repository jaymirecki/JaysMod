
import typer
import pandas as pd
import os
import xml.etree.ElementTree as ET
import csv
import utils.ipl_utils as ipl
import utils.map_utils as maps
import utils.worldspace_utils as worldspace
import utils.vehiclemodel_utils as vehiclemodel

app = typer.Typer()

@app.command()
def convert_ipl_to_xml(
    source: str = "./Data - IPLs.csv",
    destination: str = "../JaysModFramework/JMFBin/Data/IPLs"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)
    df = pd.read_csv(source_path)
    for idx, row in df.iterrows():
        ipl.csv_to_xml(row, destination_path)
    
@app.command()
def convert_ipl_to_csv(
    source: str = "../JaysModFramework/JMFBin/Data/IPLs",
    destination: str = "./Data - IPLs.csv"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)

    with open(destination_path, "w", newline="") as output:
        csv_writer = csv.writer(output)
        csv_writer.writerow(["ID", "DLC", "InteriorID", "IsOverworld", "Position","IPLNames","EntitySets", "Themes", "DefaultEntitySets","Portals"])
        for file in os.listdir(source_path):
            filepath = os.path.join(source_path, file)
            tree = ET.parse(filepath)
            root = tree.getroot()
            csv_writer.writerow(ipl.xml_to_csv(root))
            
@app.command()
def convert_worldspace_to_xml(
    source: str = "./Data - Worldspaces.csv",
    destination: str = "../JaysModFramework/JMFBin/Data/Worldspaces"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)
    df = pd.read_csv(source_path)
    for idx, row in df.iterrows():
        worldspace.csv_to_xml(row, destination_path)
            
@app.command()
def convert_map_to_xml(
    source: str = "../JaysModFramework/JMFBin/Data/Maps",
    destination: str = "./Data - Maps.csv"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)
    df = pd.read_csv(source_path)
    for idx, row in df.iterrows():
        maps.csv_to_xml(row, destination_path)
            
@app.command()
def convert_vehiclemodel_to_xml(
    source: str = "./Data - VehicleModels.csv",
    destination: str = "../JaysModFramework/JMFBin/Data/VehicleModels"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)
    df = pd.read_csv(source_path)
    for idx, row in df.iterrows():
        vehiclemodel.csv_to_xml(row, destination_path)

@app.command()
def convert_vehiclemodel_to_csv(
    source: str = "../JaysModFramework/JMFBin/Data/VehicleModels",
    destination: str = "./Data - VehicleModels.csv"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)

    with open(destination_path, "w", newline="") as output:
        csv_writer = csv.writer(output)
        csv_writer.writerow(["Hash", "ID", "Class", "SubClass", "GTAMake", "GTAModel", "RWMake", "RWModel", "MaxSpeed", "MaxKnots", "NumberOfSeats"])
        for file in os.listdir(source_path):
            filepath = os.path.join(source_path, file)
            tree = ET.parse(filepath)
            root = tree.getroot()
            csv_writer.writerow(vehiclemodel.xml_to_csv(root))

if __name__ == "__main__":
    app()