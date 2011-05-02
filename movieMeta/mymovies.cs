using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;
//using System.Xml.Serialization;


namespace movieMeta
{

   public class mymovies
    {
       public string LocalTitle
       {
           get
           {
               return returnXMLValue("LocalTitle");
           }
           set
           {
               doc.GetElementsByTagName("LocalTitle")[0].InnerText = value;
           }
        }

        public string OriginalTitle
        {
           get
           {
               return returnXMLValue("OriginalTitle");
           }
           set
           {
               doc.GetElementsByTagName("OriginalTitle")[0].InnerText = value;
           }
        }


        public string SortTitle
        {
            get
            {
                return returnXMLValue("SortTitle");
            }
            set
            {
                doc.GetElementsByTagName("SortTitle")[0].InnerText = value;
            }
        }

        public string Added
        {
            get
            {
                return returnXMLValue("Added");
            }
            set
            {
                doc.GetElementsByTagName("Added")[0].InnerText = value;
            }
        }

        public string ProductionYear
        {
            get
            {
                return returnXMLValue("ProductionYear");
            }
            set
            {
                doc.GetElementsByTagName("ProductionYear")[0].InnerText = value;
            }
        }

        public string RunningTime
        {
            get
            {
                return returnXMLValue("RunningTime");
            }
            set
            {
                doc.GetElementsByTagName("RunningTime")[0].InnerText = value;
            }
        }

        public string IMDBrating
        {
            get
            {
                return returnXMLValue("IMDBrating");
            }
            set
            {
                doc.GetElementsByTagName("IMDBrating")[0].InnerText = value;
            }
        }

        public string MPAARating
        {
            get
            {
                return returnXMLValue("MPAARating");
            }
            set
            {
                doc.GetElementsByTagName("MPAARating")[0].InnerText = value;
            }
        }

        public string Description
        {
            get
            {
                return returnXMLValue("Description");
            }
            set
            {
                doc.GetElementsByTagName("Description")[0].InnerText = value;
            }
        }

        public string Type
        {
            get
            {
                return returnXMLValue("Type");
            }
            set
            {
                doc.GetElementsByTagName("Type")[0].InnerText = value;
            }
        }

        public string AspectRatio
        {
            get
            {
                return returnXMLValue("AspectRatio");
            }
            set
            {
                doc.GetElementsByTagName("AspectRatio")[0].InnerText = value;
            }
        }

        public string LockData
        {
            get
            {
                return returnXMLValue("LockData");
            }
            set
            {
                doc.GetElementsByTagName("LockData")[0].InnerText = value;
            }
        }

        public string IMDB
        {
            get
            {
                return returnXMLValue("IMDB");
            }
            set
            {
                doc.GetElementsByTagName("IMDB")[0].InnerText = value;
            }
        }

        public string TMDbId
        {
            get
            {
                return returnXMLValue("TMDbId");
            }
            set
            {
                doc.GetElementsByTagName("TMDbId")[0].InnerText = value;
            }
        }

        public bool XMLComplete
        {
            get{
                return LocalTitle != "" &&
                    OriginalTitle != "" &&
                    SortTitle != "" &&
                    Added != "" &&
                    ProductionYear != "" &&
                    RunningTime != "" &&
                    IMDBrating != "" &&
                    MPAARating != "" &&
                    Description != "" &&
                    Type != "" &&
                    AspectRatio != "" &&
                    IMDB != "" &&
                    TMDbId != "" &&
                    Genres.Genre.Count != 0 &&
                    Persons.Count != 0;
            }

        }

        public struct _Genres
        {
            public List<string> Genre
            {
                get 
                {
                    List<string> tmp = new List<string>();
                    if (doc.GetElementsByTagName("Genres")[0] != null)
                    {
                        foreach (XmlNode xmn in doc.GetElementsByTagName("Genre"))
                        {
                            tmp.Add(xmn.InnerText);
                        }
                    }
                    return tmp;
                }
                set 
                {
                    doc.GetElementsByTagName("Genres")[0].RemoveAll();
                    foreach (string gn in value)
                    {
                        XmlNode xmn = doc.CreateElement("Genre");
                        xmn.InnerText = gn;
                        doc.GetElementsByTagName("Genres")[0].AppendChild(xmn);
                    }
                }
            }

        }
        public _Genres Genres;

        public struct _Studios
        {
            public List<string> Studio
            {
                get 
                {
                    List<string> tmp = new List<string>();
                    if (doc.GetElementsByTagName("Studios")[0] != null)
                    {
                        foreach (XmlNode xmn in doc.GetElementsByTagName("Studio"))
                        {
                            tmp.Add(xmn.InnerText);
                        }
                    }
                    return tmp;

                }
                set 
                {
                    doc.GetElementsByTagName("Studios")[0].RemoveAll();
                    foreach (string gn in value)
                    {
                        XmlNode xmn = doc.CreateElement("Studio");
                        xmn.InnerText = gn;
                        doc.GetElementsByTagName("Studios")[0].AppendChild(xmn);
                    }
                }
            }

        }
        public _Studios Studios;

        public struct Person
        {
            public string Name;
            public string Type;
            public string Role;
        }

        public List<Person> Persons //= new List<Person>();
        {
            get 
            { 
                List<Person> tmp = new List<Person>();
                if (doc.GetElementsByTagName("Persons")[0] != null)
                {
                    foreach (XmlNode xmn in doc.GetElementsByTagName("Person"))
                    {
                        Person p = new Person();
                        foreach (XmlNode xmp in xmn.ChildNodes)
                        {
                            if (xmp.Name == "Name") { p.Name = xmp.InnerText; };
                            if (xmp.Name == "Type") { p.Type = xmp.InnerText; };
                            if (xmp.Name == "Role") { p.Role = xmp.InnerText; };

                        }
                        tmp.Add(p);
                    }
                }
                return tmp;
            }
            set 
            {
                if (doc.GetElementsByTagName("Persons")[0] != null)
                {
                    doc.GetElementsByTagName("Persons")[0].RemoveAll();
                    foreach (Person p in value)
                    {
                        XmlNode xmn = doc.CreateElement("Person");
                        xmn.InnerXml = "<Name /><Type /><Role />";
                        xmn["Name"].InnerText = p.Name;
                        xmn["Type"].InnerText = p.Type;
                        xmn["Role"].InnerText = p.Role;
                        doc.GetElementsByTagName("Persons")[0].AppendChild(xmn);
                    }
                }
            }
        }

        static XmlDocument doc;
        string gpath;

        private string returnXMLValue(string xmlvalue)
        {
            if (doc.GetElementsByTagName(xmlvalue)[0] != null)
            {
                return doc.GetElementsByTagName(xmlvalue)[0].InnerText;
            }
            else
            {
                return "";
            }
        }
       
       public void load(string path){
           gpath = path;
           doc = new XmlDocument();
           
           using (StreamReader xr = new StreamReader(path))
           {
                doc.Load(xr);
                xr.Close();
                xr.Dispose();
           }
         
        }

        ~mymovies()
        {
            
        }

       public mymovies(string path) : base()
       {
           gpath = path;
           doc = new XmlDocument();
           XmlNode xmn = doc.CreateElement("Title");
           xmn.InnerXml = @"<LocalTitle />
                            <OriginalTitle />
                            <SortTitle />
                            <Added />
                            <ProductionYear />
                            <RunningTime />
                            <IMDBrating />
                            <MPAARating />
                            <Description />
                            <Type />
                            <AspectRatio />
                            <LockData />
                            <IMDB />
                            <TMDbId />
                            <Genres />
                            <Studios />
                            <Persons />";

           doc.AppendChild(xmn);


       }

       public mymovies() : base()
       {

       }

        public void save()
        {
            using (FileStream fs = new FileStream(gpath, FileMode.OpenOrCreate))
            {
                doc.Save(fs);
            }
        }

    }


}