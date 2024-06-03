
import typer
import pandas as pd
import os
import csv
import lxml.etree as ET
import math

def element_with_text(root, tag, text):
    if type(text) is str:
        elem = ET.SubElement(root, tag)
        elem.text = text
        return elem
    return None

def element_with_int(root, tag, text):
    try:
        return element_with_text(root, tag, str(int(float(text))))
    finally:
        return None
def element_with_bool(root, tag, text):
    if type(text) is bool:
        return element_with_text(root, tag, str(text).lower())
    return None

def vector(root, tag, vector):
    try:
        text = vector.split(":")
        elem = ET.SubElement(root, tag,{"X": text[0], "Y": text[1], "Z": text[2]})
        return elem
    finally:
        return None

def element_if_content(root, tag, content):
    if type(content) is str:
        return ET.SubElement(root, tag)
    return None

def element_list(root, tag, content, list_tag = "string"):
    elem = element_if_content(root, tag, content)
    try:
        for c in content.split(","):
            element_with_text(elem, list_tag, c)
        return elem
    except AttributeError:
        x = 1
    return None

def multipart_list(root, tag, content, item_tag, children):
    elem = element_if_content(root, tag, content)
    try:
        for e in content.split(","):
            item = ET.SubElement(elem, item_tag)
            for i, c in enumerate(children):
                if i >= len(e.split("/")):
                    return elem
                if  not e.split("/")[i]:
                    continue
                if ":" in e.split("/")[i]:
                    vector(item, c, e.split("/")[i])
                else:
                    element_with_text(item, c, e.split("/")[i])
        return elem
    except AttributeError:
        x = 1
    return elem
