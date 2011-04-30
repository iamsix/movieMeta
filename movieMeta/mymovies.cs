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
        public string LocalTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string SortTitle { get; set; }
        public string Added { get; set; }
        public string ProductionYear { get; set; }
        public string RunningTime { get; set; }
        public string IMDBrating { get; set; }
        public string MPAARating { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string AspectRatio { get; set; }
        public string LockData { get; set; }
        public string IMDB { get; set; }
        public string TMDbId { get; set; }
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
            public List<string> Genre;
            
        }
        public _Genres Genres;

        public struct _Studios
        {
            public List<string> Studio;

        }
        public _Studios Studios;

        public struct Person
        {
            public string Name;
            public string Type;
            public string Role;
        }
        public List<Person> Persons = new List<Person>();

        XmlDocument doc;
        string gpath;
       
       public void load(string path){
            gpath = path;
            doc = new XmlDocument();
           
           // using (XmlReader xr = XmlReader.Create(path))
           using (StreamReader xr = new StreamReader(path))
            {
                doc.Load(xr);
                xr.Close();
                xr.Dispose();
            }



            LocalTitle = doc.GetElementsByTagName("LocalTitle")[0].InnerText;
            OriginalTitle = doc.GetElementsByTagName("OriginalTitle")[0].InnerText;
            SortTitle = doc.GetElementsByTagName("SortTitle")[0].InnerText;
            if (doc.GetElementsByTagName("Added")[0] != null)
            {
                Added = doc.GetElementsByTagName("Added")[0].InnerText;
            }
            if (doc.GetElementsByTagName("ProductionYear")[0] != null)
            {
                ProductionYear = doc.GetElementsByTagName("ProductionYear")[0].InnerText;
            }
            if (doc.GetElementsByTagName("RunningTime")[0] != null)
            {
                RunningTime = doc.GetElementsByTagName("RunningTime")[0].InnerText;
            }
            if (doc.GetElementsByTagName("IMDBrating")[0] != null)
            {
                IMDBrating = doc.GetElementsByTagName("IMDBrating")[0].InnerText;
            }
            if (doc.GetElementsByTagName("MPAARating")[0] != null)
            {
                MPAARating = doc.GetElementsByTagName("MPAARating")[0].InnerText;
            }
            if (doc.GetElementsByTagName("Description")[0] != null)
            {
                Description = doc.GetElementsByTagName("Description")[0].InnerText;
            }
            if (doc.GetElementsByTagName("Type")[0] != null)
            {
                Type = doc.GetElementsByTagName("Type")[0].InnerText;
            }
            if (doc.GetElementsByTagName("AspectRatio")[0] != null)
            {
                AspectRatio = doc.GetElementsByTagName("AspectRatio")[0].InnerText;
            }
            if (doc.GetElementsByTagName("LockData")[0] != null)
            {
                LockData = doc.GetElementsByTagName("LockData")[0].InnerText;
            }
            if (doc.GetElementsByTagName("IMDB")[0] != null)
            {
                IMDB = doc.GetElementsByTagName("IMDB")[0].InnerText;
            }
            if (doc.GetElementsByTagName("TMDbId")[0] != null)
            {
                TMDbId = doc.GetElementsByTagName("TMDbId")[0].InnerText;
            }

            Genres.Genre = new List<string>();
            if (doc.GetElementsByTagName("Genres")[0] != null)
            {
                foreach (XmlNode xmn in doc.GetElementsByTagName("Genre"))
                {
                    Genres.Genre.Add(xmn.InnerText);  
                }
            }

            Studios.Studio = new List<string>();
            if (doc.GetElementsByTagName("Studios")[0] != null)
            {
                foreach (XmlNode xmn in doc.GetElementsByTagName("Studio"))
                {
                    Studios.Studio.Add(xmn.InnerText);
                }
            }



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
                    Persons.Add(p);
                }
            }
         
        }

        ~mymovies()
        {
            
        }

        public void save()
        {
            doc.GetElementsByTagName("LocalTitle")[0].InnerText = LocalTitle;
            doc.GetElementsByTagName("OriginalTitle")[0].InnerText = OriginalTitle;
            doc.GetElementsByTagName("SortTitle")[0].InnerText = SortTitle;
            if (doc.GetElementsByTagName("Added")[0] != null)
            {
                doc.GetElementsByTagName("Added")[0].InnerText = Added;
            }
            if (doc.GetElementsByTagName("ProductionYear")[0] != null)
            {
                 doc.GetElementsByTagName("ProductionYear")[0].InnerText = ProductionYear;
            }
            if (doc.GetElementsByTagName("RunningTime")[0] != null)
            {
                doc.GetElementsByTagName("RunningTime")[0].InnerText = RunningTime;
            }
            if (doc.GetElementsByTagName("IMDBrating")[0] != null)
            {
                doc.GetElementsByTagName("IMDBrating")[0].InnerText = IMDBrating;
            }
            if (doc.GetElementsByTagName("MPAARating")[0] != null)
            {
                doc.GetElementsByTagName("MPAARating")[0].InnerText = MPAARating;
            }
            if (doc.GetElementsByTagName("Description")[0] != null)
            {
                doc.GetElementsByTagName("Description")[0].InnerText = Description;
            }
            if (doc.GetElementsByTagName("Type")[0] != null)
            {
                doc.GetElementsByTagName("Type")[0].InnerText = Type;
            }
            if (doc.GetElementsByTagName("AspectRatio")[0] != null)
            {
                doc.GetElementsByTagName("AspectRatio")[0].InnerText = AspectRatio;
            }
            if (doc.GetElementsByTagName("LockData")[0] != null)
            {
                doc.GetElementsByTagName("LockData")[0].InnerText = LockData;
            }
            if (doc.GetElementsByTagName("IMDB")[0] != null)
            {
                doc.GetElementsByTagName("IMDB")[0].InnerText = IMDB;
            }
            if (doc.GetElementsByTagName("TMDbId")[0] != null)
            {
                doc.GetElementsByTagName("TMDbId")[0].InnerText = TMDbId;
            }
          
            
            if (doc.GetElementsByTagName("Genres")[0] != null)
            {
                doc.GetElementsByTagName("Genres")[0].RemoveAll();
                foreach (string gn in Genres.Genre)
                {
                    XmlNode xmn = doc.CreateElement("Genre");
                    xmn.InnerText = gn;
                    doc.GetElementsByTagName("Genres")[0].AppendChild(xmn);
                }
            }

            if (doc.GetElementsByTagName("Studios")[0] != null)
            {
                doc.GetElementsByTagName("Studios")[0].RemoveAll();
                foreach (string st in Studios.Studio)
                {
                    XmlNode xmn = doc.CreateElement("Studio");
                    xmn.InnerText = st;
                    doc.GetElementsByTagName("Studios")[0].AppendChild(xmn);
                }
            }

            if (doc.GetElementsByTagName("Persons")[0] != null)
            {
                doc.GetElementsByTagName("Persons")[0].RemoveAll();
                foreach (Person p in Persons)
                {
                    XmlNode xmn = doc.CreateElement("Person");
                    xmn.InnerXml = "<Name /><Type /><Role />";
                    xmn["Name"].InnerText = p.Name;
                    xmn["Type"].InnerText = p.Type;
                    xmn["Role"].InnerText = p.Role;
                    doc.GetElementsByTagName("Persons")[0].AppendChild(xmn);
                }
            }
            
            using (FileStream fs = new FileStream(gpath, FileMode.Truncate))
            {
                
                doc.Save(fs);
            }
        }

    }


}