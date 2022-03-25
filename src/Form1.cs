using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCrawl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                label1.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text != "" && (bfsButton.Checked || dfsButton.Checked) && fileName.Text != "Insert File Name")
            {
                Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
                List<string> visitedDirs1 = new List<string>();
                FolderCrawler.makeGraph(ref graph, ref visitedDirs1, label1.Text);

                HashSet<string> itemCovered = new HashSet<string>();
                List<string> queue = new List<string>();
                queue.Add(graph.ElementAt(0).Key);

                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                Microsoft.Msagl.Drawing.Graph graphDraw = new Microsoft.Msagl.Drawing.Graph("graph");

                var watch = new System.Diagnostics.Stopwatch();

                
                
                if (searchAll.Checked)
                {
                    List<string> targetDirs = new List<string>();
                    if (bfsButton.Checked)
                    {
                        watch.Start();
                        FolderCrawler.BFSSearchAll(graph, fileName.Text, ref targetDirs, ref queue, ref itemCovered, ref graphDraw);
                        watch.Stop();
                        Label executionTime = new Label();
                        executionTime.Location = new Point(20, 400);
                        executionTime.Size = new Size(50, 15);
                        executionTime.AutoSize = true;
                        executionTime.Text = "Total Execution Time: " + watch.ElapsedMilliseconds.ToString() + "ms";
                        this.label1.AutoSize = true;
                        this.Controls.Add(executionTime);
                    }
                    else
                    {
                        watch.Start();
                        FolderCrawler.DFSSearchAll(graph, fileName.Text, ref targetDirs, ref queue, ref itemCovered, ref graphDraw);
                        watch.Stop();
                        Label executionTime = new Label();
                        executionTime.Location = new Point(20, 400);
                        executionTime.Size = new Size(50, 15);
                        executionTime.AutoSize = true;
                        executionTime.Text = "Total Execution Time: " + watch.ElapsedMilliseconds.ToString() + "ms";
                        this.label1.AutoSize = true;
                        this.Controls.Add(executionTime);
                    }
                    LinkLabel[] paths = new LinkLabel[targetDirs.Count];
                    int linkLabelCounter = 0;
                    int yLoc = 10;
                    int ySize = 15;
                    foreach (string dir in targetDirs)
                    {
                        paths[linkLabelCounter] = new LinkLabel();
                        paths[linkLabelCounter].Text = dir;
                        paths[linkLabelCounter].Tag = dir;
                        paths[linkLabelCounter].Location = new Point(390, yLoc);
                        paths[linkLabelCounter].Size = new Size(200, 15);
                        paths[linkLabelCounter].AutoSize = true;
                        paths[linkLabelCounter].LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Clicked);
                        this.Controls.Add(paths[linkLabelCounter]);
                        linkLabelCounter++;
                        yLoc += ySize * 2;
                        string[] dirSplit = dir.Split(Path.DirectorySeparatorChar);
                        for (int i = 0; i < dirSplit.Length - 1; i++)
                        {
                            int existingEdge = 0;
                            foreach (var edge in graphDraw.Edges)
                            {
                                if (edge.Source == dirSplit[i] && edge.Target == dirSplit[i + 1])
                                {
                                    edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                    graphDraw.FindNode(dirSplit[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                    graphDraw.FindNode(dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                    existingEdge++;
                                }
                            }
                            if (existingEdge == 0)
                            {
                                graphDraw.AddEdge(dirSplit[i], dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                graphDraw.FindNode(dirSplit[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                graphDraw.FindNode(dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                            }
                                            
                        }
                    }

                }
                else
                {
                    bool found = false;
                    string targetDir = "Not Found";
                    if (bfsButton.Checked)
                    {
                        watch.Start();
                        FolderCrawler.BFSSearchOne(graph, fileName.Text, ref targetDir, ref queue, ref found, ref itemCovered, ref graphDraw);
                        watch.Stop();
                        Label executionTime = new Label();
                        executionTime.Location = new Point(20, 400);
                        executionTime.Size = new Size(50, 15);
                        executionTime.AutoSize = true;
                        executionTime.Text = "Total Execution Time: " + watch.ElapsedMilliseconds.ToString() + "ms";
                        this.label1.AutoSize = true;
                        this.Controls.Add(executionTime);
                    }
                    else
                    {
                        watch.Start();
                        FolderCrawler.DFSSearchOne(graph, fileName.Text, ref targetDir, ref queue, ref found, ref itemCovered, ref graphDraw);
                        watch.Stop();
                        Label executionTime = new Label();
                        executionTime.Location = new Point(20, 400);
                        executionTime.Size = new Size(50, 15);
                        executionTime.AutoSize = true;
                        executionTime.Text = "Total Execution Time: " + watch.ElapsedMilliseconds.ToString() + "ms";
                        this.label1.AutoSize = true;
                        this.Controls.Add(executionTime);
                    }
                    LinkLabel finalPath = new LinkLabel();
                    finalPath.Text = targetDir;
                    finalPath.Tag = targetDir;
                    finalPath.Location = new System.Drawing.Point(390, 10);
                    finalPath.Size = new System.Drawing.Size(200, 15);
                    finalPath.AutoSize = true;
                    finalPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Clicked);
                    this.Controls.Add(finalPath);
                    string[] dirSplit = targetDir.Split(Path.DirectorySeparatorChar);
                    for (int i = 0; i < dirSplit.Length - 1; i++)
                    {
                        int existingEdge = 0;
                        foreach (var edge in graphDraw.Edges)
                        {
                            if (edge.Source == dirSplit[i] && edge.Target == dirSplit[i + 1])
                            {
                                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                graphDraw.FindNode(dirSplit[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                graphDraw.FindNode(dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                                existingEdge++;
                            }
                        }
                        if (existingEdge == 0)
                        {
                            graphDraw.AddEdge(dirSplit[i], dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                            graphDraw.FindNode(dirSplit[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                            graphDraw.FindNode(dirSplit[i + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                        }
                    }
                }
                

            Form form = new Form();

                FolderCrawler.drawGraph(graph, ref graphDraw);
                //bind the graph to the viewer 
                viewer.Graph = graphDraw;
                //associate the viewer with the form 
                form.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                form.Controls.Add(viewer);
                form.ResumeLayout();
                //show the form 
                form.ShowDialog();
            }
        }

        private void linkLabel_Clicked (object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filepath = ((LinkLabel)sender).Tag.ToString();
            string[] filepathSplit = filepath.Split(Path.DirectorySeparatorChar);
            string[] pathWithoutFile = new string[filepathSplit.Length - 1];
            for (int i = 0; i < filepathSplit.Length - 1; i++)
            {
                pathWithoutFile[i] = filepathSplit[i];
            }
            string folderPath = Path.Combine(pathWithoutFile);
            ProcessStartInfo sInfo = new ProcessStartInfo(folderPath);
            Process.Start(sInfo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
