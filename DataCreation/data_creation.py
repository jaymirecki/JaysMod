
import typer
import pandas as pd
import os
import xml.etree.ElementTree as ET
import csv
import utils.ipl_utils as ipl

app = typer.Typer()

@app.command()
def convert_ipl_to_xml(
    source: str = "./ipl.csv",
    destination: str = "../JaysModFramework/JMFBin/Data/IPL"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)
    df = pd.read_csv(source_path)
    for idx, row in df.iterrows():
        ipl.csv_to_xml(row, destination_path)
    
@app.command()
def convert_ipl_to_csv(
    source: str = "../JaysModFramework/JMFBin/Data/IPL",
    destination: str = "./ipl.csv"
    ):
    source_path= os.path.abspath(source)
    destination_path = os.path.abspath(destination)

    with open(destination_path, "w", newline="") as output:
        csv_writer = csv.writer(output)
        csv_writer.writerow(["ID", "DLC", "InteriorID","Position","IPLNames","EntitySets", "Themes", "DefaultEntitySets","Portals"])
        for file in os.listdir(source_path):
            filepath = os.path.join(source_path, file)
            tree = ET.parse(filepath)
            root = tree.getroot()
            csv_writer.writerow(ipl.xml_to_csv(root))


if __name__ == "__main__":
    app()