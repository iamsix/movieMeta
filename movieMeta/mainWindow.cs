using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class mainWindow : Form
    {
        mymovies mmdata;
        DataGridViewRow currentrow;

        public mainWindow()
        {
            InitializeComponent();
            MovieList.DataSource = MovieData.Tables[0].DefaultView;

        }


        private void button1_Click(object sender, EventArgs e)
        {
           

            string path = "E:\\movies";

            findMoviesInDirectory(path);

            path = "G:\\HDMovies";

            findMoviesInDirectory(path);

            refreshmovielist();
           
           // MovieList.Sort(MovieList.Columns[4], ListSortDirection.Ascending);

        }

        /// <summary>
        /// This looks for any movie file within subdirectories of a directory, indlucing nested subdirectories, and adds it to the MovieList.
        /// </summary>
        /// <param name="path">The path containing movie folders</param>
        private void findMoviesInDirectory(string path)
        {

            /*TODO:
             * I need to change this code so it looks for movie files instead of mymovies.xml files
             * I also need to add some directory tripping code for subdirectories
             * and make it not add a folder that only exists to hold subdirectories
             * 
             * Also I need to make it properly add directories WITHOUT the mymovies and 'red' them
             */

            string[] directories = Directory.GetDirectories(path);

            // MovieList.DataSource = MovieData.Tables[0];
            foreach (string di in directories)
            {
                FileInfo fi = new FileInfo(di + "\\mymovies.dna");
                //listView1.Items.Add(di);
                string movieTitle = "";
                string sortTitle = "";
                string genreList = "";
                bool xmlComplete = false;

                if (fi.Exists)
                {
                }
                else
                {
                    fi = new FileInfo(di + "\\mymovies.xml");
                    if (fi.Exists)
                    {
                        //ok We're going to just flatten the entire thing, no subfolders.
                        //no code for subfolders yet...

                        mmdata = new mymovies();
                        mmdata.load(di + "\\mymovies.xml");

                        movieTitle = mmdata.LocalTitle;
                        sortTitle = mmdata.SortTitle;

                        xmlComplete = mmdata.XMLComplete;
                        genreList = String.Join(";", mmdata.Genres.Genre.ToArray());

                    }

                    fi = new FileInfo(di + "\\folder.jpg");
                    bool posterexists = fi.Exists;
                    fi = new FileInfo(di + "\\backdrop.jpg");
                    bool backdropexists = fi.Exists;
                    //MovieList.Rows.Add(movieTitle, posterexists, backdropexists, di, sortTitle, "", "", "", genreList);

                    MovieData.Tables[0].Rows.Add(movieTitle, posterexists, backdropexists, di, sortTitle, "", "", "", genreList, xmlComplete);
                    //this tooltip needs to be more extensive
                    MovieList.Rows[MovieList.Rows.Count - 1].Cells[0].ToolTipText = di;
                    if (xmlComplete) { MovieList.Rows[MovieList.Rows.Count - 1].Cells[0].Style.ForeColor = Color.Green; }
                }

                //mymovies mmdata = new mymovies(di + "\\mymovies.xml");

            }
         //   MovieData.Tables[0].DefaultView.Sort = "sort ASC";
           // MovieList.EndEdit();            
        }


        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentrow = MovieList.Rows[e.RowIndex];
            //clear everything before repopulating. 
            //We do this heere instead of on cellleave because the cell is 'left' when it's deselected
            //this way this happens only when we want to load something else.

            if (Poster.Image != null) {Poster.Image.Dispose();}
            if (BackdropPic.Image != null) { BackdropPic.Image.Dispose(); }
            Genres.Rows.Clear();
            Directors.Rows.Clear();
            Studios.Rows.Clear();
            Actors.Rows.Clear();

            string di = MovieList[4, e.RowIndex].Value.ToString();
            FileInfo fi = new FileInfo(di + "\\mymovies.xml");
            mmdata = new mymovies();
            
            if (fi.Exists)
            {
                mmdata.load(di + "\\mymovies.xml");
                LocalTitle.Text = mmdata.LocalTitle;
                OriginalTitle.Text = mmdata.OriginalTitle;
                SortTitle.Text = mmdata.SortTitle;
                ProductionYear.Text = mmdata.ProductionYear;
                Runtime.Text = mmdata.RunningTime;
                MPAARating.Text = mmdata.MPAARating;
                IMDBRating.Text = mmdata.IMDBrating;
                AspectRatio.Text = mmdata.AspectRatio;
                MovieType.Text = mmdata.Type;
                DateAdded.Value = DateTime.Parse(mmdata.Added);
                IMDB.Text = mmdata.IMDB;
                toolTip1.SetToolTip(imdbgo, "http://www.imdb.com/title/" + mmdata.IMDB + "/");
                TMDBID.Text = mmdata.TMDbId;
                toolTip1.SetToolTip(tmdbgo, "http://www.themoviedb.org/movie/" + mmdata.TMDbId);
                Description.Text = mmdata.Description;
                foreach (string g in mmdata.Genres.Genre)
                {
                    Genres.Rows.Add(g);

                }
                foreach (string s in mmdata.Studios.Studio)
                {
                    Studios.Rows.Add(s);
                }
                foreach (mymovies.Person p in mmdata.Persons)
                {
                    if (p.Type.ToLower() == "director") { Directors.Rows.Add(p.Name); }
                    if (p.Type.ToLower() == "actor")
                    {
                        System.Drawing.Image img;

                        string imgbyname = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ImagesByName\\" + p.Name + "\\folder.jpg";
                        fi = new FileInfo(imgbyname);
                        if (fi.Exists)
                        {
                            img = new Bitmap(imgbyname);

                        }
                        else
                        {
                            img = new Bitmap(1, 1);
                        }

                        Actors.Rows.Add(img, p.Name, "..as ", p.Role, " ");
                    }
                }
            }
            //else 
            //{ 
            //}
            

            fi = new FileInfo(di + "\\folder.jpg");
            if (fi.Exists)
            {
                Poster.Image = Image.FromFile(di + "\\folder.jpg");
            }
            else
            {
                Poster.Image = imageList1.Images[1];
            }
            fi = new FileInfo(di + "\\backdrop.jpg");
            if (fi.Exists)
            {
                BackdropPic.Image = Image.FromFile(di + "\\backdrop.jpg");
            }
            else
            {
                fi = new FileInfo(di + "\\backdrop.png");
                if (fi.Exists)
                {
                    BackdropPic.Image = Image.FromFile(di + "\\backdrop.png");
                }
                else
                {
                    BackdropPic.Image = imageList1.Images[1];
                }
            }
            
           
          
           /* */
            

        }

        private void imdbgo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(toolTip1.GetToolTip(imdbgo));
        }

        private void tmdbgo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(toolTip1.GetToolTip(tmdbgo));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            savedata();

        }
        private void savedata()
        {
            //if (mmdata == null)
            //{
            //    mmdata = new mymovies();
            //    //string di = MovieList[3, e.RowIndex].Value.ToString();
            //}
            mmdata.LocalTitle = LocalTitle.Text;
            mmdata.OriginalTitle = OriginalTitle.Text;
            mmdata.SortTitle = SortTitle.Text;
            mmdata.ProductionYear = ProductionYear.Text;
            mmdata.RunningTime = Runtime.Text;
            mmdata.MPAARating = MPAARating.Text;
            mmdata.IMDBrating = IMDBRating.Text;
            mmdata.AspectRatio = AspectRatio.Text;
            mmdata.Type = MovieType.Text;
            mmdata.Added = DateAdded.Value.ToString("yyy-MM-dd h:mm:ss tt");
            mmdata.IMDB = IMDB.Text;
            mmdata.TMDbId = TMDBID.Text;
            mmdata.Description = Description.Text;

            mmdata.save();
            currentrow.Cells[3].Value = mmdata.XMLComplete;
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            savedata();
            refreshmovielist();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MovieData.Tables[0].DefaultView.RowFilter = "genres LIKE '*Sci*'";
            refreshmovielist();

            
        }

        private void refreshmovielist()
        {
            MovieData.Tables[0].DefaultView.Sort = "sort ASC";
            //  MovieList.DataSource = MovieData.Tables[0];
            button2.Text = MovieList.Rows.Count.ToString();


            foreach (DataGridViewRow dr in MovieList.Rows)
            {
                dr.Cells[0].ToolTipText = dr.Cells[4].Value.ToString();
                switch ((string)dr.Cells[3].Value)
                {
                    case "True":
                        dr.Cells[0].Style.ForeColor = Color.Green;
                        break;
                    case "False":
                        dr.Cells[0].Style.ForeColor = Color.Black;
                        break;
                    case "":
                        dr.Cells[0].Style.ForeColor = Color.Red;
                        break;
                }
            }
            MovieList.EndEdit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MovieData.Tables[0].DefaultView.RowFilter = "";
            }
            else
            {
                MovieData.Tables[0].DefaultView.RowFilter = "MovieName LIKE '*" + textBox1.Text + "*'";
            }
            refreshmovielist();
        }

        private void MovieList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
