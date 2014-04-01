using System;
using System.Xml.Serialization;

namespace TeamMentor.Test
{
    public class Git_Clone_Data
    {
        [XmlAttribute] public string   When          { get; set; }
        [XmlAttribute] public double   Clone_Seconds { get; set; }
        [XmlAttribute] public string   Clone_Type    { get; set; }
        [XmlAttribute] public string   Repo_Name     { get; set; }
        [XmlAttribute] public string   Repo_Source   { get; set; }
        [XmlAttribute] public string   Repo_Path     { get; set; }
        [XmlAttribute] public int      Repo_Files    { get; set; }
    }
}