
import typer
import pandas as pd
import os
import csv
import lxml.etree as ET
import math

def element_text_or_default(root: ET.Element, tag: str, default: str = ""):
    return root.find(tag).text if not root.find(tag) is None else default

def element_children_to_list(root: ET.Element, tag: str):
    elem = root.find(tag)
    content = ""
    if not elem is None:
        ray = elem
        return ray
    return []

def element_list(root: ET.Element, tag: str):
    children = element_children_to_list(root, tag)
    content = ""
    for elem in children:
        content += f",{elem.text}"
    return content[1:]

def multipart_list(root: ET.Element, tag: str, children):
    elems = element_children_to_list(root, tag)
    content = ""
    for elem in elems:
        content += ","
        child_content = ""
        for child in children:
            child_elem = elem.find(child)
            if not child_elem is None and "X" in child_elem.attrib:
                child_content += f"/{vector(child_elem)}"
            else:
                child_content += f"/{element_text_or_default(elem, child)}"
        content += child_content[1:]
    return content[1:]

def vector(vector):
    return f"{vector.attrib['X']}:{vector.attrib['Y']}:{vector.attrib['Z']}" if not vector is None else ""
