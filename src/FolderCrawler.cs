using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


public class FolderCrawler
{
    /*static void Main()
    {
        Console.WriteLine("Choose Starting Directory:");
        string dirPath = Console.ReadLine();
        Console.WriteLine("Input File Name:");
        string targetFile = Console.ReadLine();
        Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        List<string> visitedDirs1 = new List<string>();
        makeGraph(ref graph, ref visitedDirs1, dirPath);

        HashSet<string> itemCovered = new HashSet<string>();
        List<string> queue = new List<string>();
        queue.Add(graph.ElementAt(0).Key);

        bool found = false;
        string targetDir = "Not found";
        List<string> targetDirs = new List<string>();
        
        //BFSSearchOne(graph, targetFile, ref targetDir, ref queue, ref found, ref itemCovered);
        //DFSSearchOne(graph, targetFile, ref targetDir, ref queue, ref found, ref itemCovered);
        //BFSSearchAll(graph, targetFile, ref targetDirs, ref queue, ref itemCovered);
        DFSSearchAll(graph, targetFile, ref targetDirs, ref queue, ref itemCovered);

        //Console.WriteLine(targetDir);
        foreach(string tarDir in targetDirs)
        {
            Console.WriteLine(tarDir);
        }
        Console.ReadKey();
    }*/

    public static void makeGraph(ref Dictionary<string, List<string>> graph, ref List<string> visitedDirs1, string dirPath)
    {
        System.IO.Directory.SetCurrentDirectory(@dirPath);
        var directories = CustomSearcher.GetDirectories(@dirPath);
        visitedDirs1.Add(dirPath);

        List<string> subDirs = new List<string>();
        foreach (string dir in directories)
        {
            subDirs.Add(dir);
        }
        graph.Add(dirPath, subDirs);

        foreach(string subdi in subDirs)
        {
            bool isChecked = false;
            string nextPath = Path.Combine(dirPath, subdi);
            if (visitedDirs1.Contains(nextPath)) 
            {
                isChecked = true;
            }
            if (isChecked == false)
            {
                makeGraph(ref graph, ref visitedDirs1, nextPath);
            }
        }
    }

    public static void BFSSearchOne(Dictionary<string, List<string>> graph, string targetFile, ref string targetDir, ref List<string> queue, ref bool found, ref HashSet<string> itemCovered, ref Microsoft.Msagl.Drawing.Graph graphDraw)
    {   
        while(queue.Count > 0)
        {    
            if (found == false)
            {
                var element = queue.ElementAt(0);
                queue.Remove(element);

                if (itemCovered.Contains(element))
                    continue;
                else
                    itemCovered.Add(element);
                string[] elementSplit = element.Split(Path.DirectorySeparatorChar);
                for (int el = 0; el < elementSplit.Length - 1; el++)
                {
                    List<Microsoft.Msagl.Drawing.Edge> removeEdgeList = new List<Microsoft.Msagl.Drawing.Edge>();
                    foreach (var edge in graphDraw.Edges)
                    {
                        if (edge.Source == elementSplit[el] && edge.Target == elementSplit[el + 1])
                        {
                            removeEdgeList.Add(edge);
                        }

                    }
                    foreach (var ed in removeEdgeList)
                    {
                        graphDraw.RemoveEdge(ed);
                    }
                    graphDraw.AddEdge(elementSplit[el], elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graphDraw.FindNode(elementSplit[el]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graphDraw.FindNode(elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;

                }

                string[] files;
                try
                {
                    files = Directory.GetFiles(@element);
                }
                catch (UnauthorizedAccessException)
                {
                    files = new string[] { };
                }

                foreach(var file in files)
                {
                    if (file == Path.Combine(element, targetFile))
                    {
                        found = true;
                        targetDir = targetDir.Replace(targetDir, Path.Combine(element, targetFile));
                        break;
                    }
                }
                if (found == false)
                {
                    List<string> neighbours;
                    try
                    {
                        neighbours = graph[element];
                    } 
                    catch(KeyNotFoundException)
                    {
                        neighbours = new List<string>();
                    }

                    if (neighbours.Count > 0)
                    {
                        foreach (var item1 in neighbours)
                        {
                            queue.Add(item1);
                        }
                    }
                }
                else break;

            }
        }
    }

    public static void BFSSearchAll(Dictionary<string, List<string>> graph, string targetFile, ref List<string> targetDirs, ref List<string> queue, ref HashSet<string> itemCovered, ref Microsoft.Msagl.Drawing.Graph graphDraw)
    {   
        while(queue.Count > 0)
        {    
            var element = queue.ElementAt(0);
            queue.Remove(element);

            if (itemCovered.Contains(element))
                continue;
            else
                itemCovered.Add(element);

            string[] elementSplit = element.Split(Path.DirectorySeparatorChar);
            for (int el = 0; el < elementSplit.Length - 1; el++)
            {
                List<Microsoft.Msagl.Drawing.Edge> removeEdgeList = new List<Microsoft.Msagl.Drawing.Edge>();
                foreach (var edge in graphDraw.Edges)
                {
                    if (edge.Source == elementSplit[el] && edge.Target == elementSplit[el + 1])
                    {
                        removeEdgeList.Add(edge);
                    }

                }

                foreach (var ed in removeEdgeList)
                {
                    graphDraw.RemoveEdge(ed);
                }
                graphDraw.AddEdge(elementSplit[el], elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graphDraw.FindNode(elementSplit[el]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graphDraw.FindNode(elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            }
            string[] files;
            try
            {
               files = Directory.GetFiles(@element);
            }
            catch (UnauthorizedAccessException)
            {
                files = new string[]{};
            }
            

            foreach(var file in files)
            {
                Console.WriteLine(file);
                if (file == Path.Combine(element, targetFile))
                {
                    targetDirs.Add(file);
                }
            }
            List<string> neighbours;
            try
            {
                neighbours = graph[element];
            }
            catch (KeyNotFoundException)
            {
                neighbours = new List<string>();
            }

            if (neighbours.Count > 0)
            {
                foreach (var item1 in neighbours)
                {
                    queue.Add(item1);
                }
            }
        }
    }


    public static void DFSSearchOne(Dictionary<string, List<string>> graph, string targetFile, ref string targetDir, ref List<string> astack, ref bool found, ref HashSet<string> itemCovered, ref Microsoft.Msagl.Drawing.Graph graphDraw)
    {   
        while(astack.Count > 0)
        {    
            if (found == false)
            {
                var element = astack.ElementAt(0);
                astack.RemoveAt(0);
                if (itemCovered.Contains(element))
                    continue;
                else
                    itemCovered.Add(element);

                string[] elementSplit = element.Split(Path.DirectorySeparatorChar);
                for (int el = 0; el < elementSplit.Length - 1; el++)
                {
                    List<Microsoft.Msagl.Drawing.Edge> removeEdgeList = new List<Microsoft.Msagl.Drawing.Edge>();
                    foreach (var edge in graphDraw.Edges)
                    {
                        if (edge.Source == elementSplit[el] && edge.Target == elementSplit[el + 1])
                        {
                            removeEdgeList.Add(edge);
                        }

                    }

                    foreach (var ed in removeEdgeList)
                    {
                        graphDraw.RemoveEdge(ed);
                    }
                    graphDraw.AddEdge(elementSplit[el], elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graphDraw.FindNode(elementSplit[el]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    graphDraw.FindNode(elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                }
                string[] files;
                try
                {
                   files = Directory.GetFiles(@element);
                }
                catch (UnauthorizedAccessException)
                {
                    files = new string[]{};
                }

                foreach(var file in files)
                {
                    Console.WriteLine(file);
                    if (file == Path.Combine(element, targetFile))
                    {
                        found = true;
                        targetDir = targetDir.Replace(targetDir, Path.Combine(element, targetFile));
                        break;
                    }
                }
                if (found == false)
                {
                    List<string> neighbours;
                    try
                    {
                        neighbours = graph[element];
                    }
                    catch (KeyNotFoundException)
                    {
                        neighbours = new List<string>();
                    }

                    if (neighbours.Count > 0)
                    {
                        for (int i = 0; i < neighbours.Count/2; i++)
                        {
                            string temp = neighbours[i];
                            neighbours[i] = neighbours[i].Replace(neighbours[i], neighbours[neighbours.Count - i - 1]);
                            neighbours[neighbours.Count - i - 1] = neighbours[neighbours.Count - i - 1].Replace(neighbours[neighbours.Count - i - 1], temp);
                        }
                        foreach (var item1 in neighbours)
                        {
                            astack.Insert(0, item1);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static void DFSSearchAll(Dictionary<string, List<string>> graph, string targetFile, ref List<string> targetDirs, ref List<string> astack, ref HashSet<string> itemCovered, ref Microsoft.Msagl.Drawing.Graph graphDraw)
    {   
        while(astack.Count > 0)
        {    
            var element = astack.ElementAt(0);
            astack.RemoveAt(0);

            if (itemCovered.Contains(element))
                continue;
            else
                itemCovered.Add(element);

            string[] elementSplit = element.Split(Path.DirectorySeparatorChar);
            for (int el = 0; el < elementSplit.Length - 1; el++)
            {
                List<Microsoft.Msagl.Drawing.Edge> removeEdgeList = new List<Microsoft.Msagl.Drawing.Edge>();
                foreach (var edge in graphDraw.Edges)
                {
                    if (edge.Source == elementSplit[el] && edge.Target == elementSplit[el + 1])
                    {
                        removeEdgeList.Add(edge);
                    }

                }

                foreach (var ed in removeEdgeList)
                {
                    graphDraw.RemoveEdge(ed);
                }
                graphDraw.AddEdge(elementSplit[el], elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graphDraw.FindNode(elementSplit[el]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                graphDraw.FindNode(elementSplit[el + 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            }

            string[] files;
            try
            {
               files = Directory.GetFiles(@element);
            }
            catch (UnauthorizedAccessException)
            {
                files = new string[]{};
            }

            foreach(var file in files)
            {
                Console.WriteLine(file);
                if (file == Path.Combine(element, targetFile))
                {
                    targetDirs.Add(file);
                }
            }
            List<string> neighbours;
            try
            {
                neighbours = graph[element];
            }
            catch (KeyNotFoundException)
            {
                neighbours = new List<string>();
            }

            if (neighbours.Count > 0)
            {
                for (int i = 0; i < neighbours.Count/2; i++)
                {
                    string temp = neighbours[i];
                    neighbours[i] = neighbours[i].Replace(neighbours[i], neighbours[neighbours.Count - i - 1]);
                    neighbours[neighbours.Count - i - 1] = neighbours[neighbours.Count - i - 1].Replace(neighbours[neighbours.Count - i - 1], temp);
                }
                foreach (var item1 in neighbours)
                {
                    astack.Insert(0, item1);
                }
            }
        }
    }

    public static void drawGraph(Dictionary<string, List<string>> graph, ref Microsoft.Msagl.Drawing.Graph graphDraw)
    {

        string[] dirPathSplit = graph.ElementAt(0).Key.Split(Path.DirectorySeparatorChar);

        for (int baseNode = 0; baseNode < dirPathSplit.Length - 1; baseNode++)
        {
            int existingEdges = 0;
            foreach (Microsoft.Msagl.Drawing.Edge edge in graphDraw.Edges)
            {
                if (edge.Source == dirPathSplit[baseNode] && edge.Target == dirPathSplit[baseNode + 1])
                {
                    existingEdges++;
                }
            }
            if (existingEdges == 0)
                graphDraw.AddEdge(dirPathSplit[baseNode], dirPathSplit[baseNode + 1]);
        }

        foreach (var initNode in graph.ElementAt(0).Value)
        {
            string[] nodeValue = initNode.Split(Path.DirectorySeparatorChar);
            int existingEdges = 0;
            foreach (Microsoft.Msagl.Drawing.Edge edge in graphDraw.Edges)
            {
                if (edge.Source == dirPathSplit[dirPathSplit.Length - 1] && edge.Target == nodeValue[nodeValue.Length - 1])
                {
                    existingEdges++;
                }
            }
            if (existingEdges == 0)
                graphDraw.AddEdge(dirPathSplit[dirPathSplit.Length - 1], nodeValue[nodeValue.Length - 1]);
        }
        graph.Remove(graph.ElementAt(0).Key);

        foreach(var graphNode in graph)
        {
            if (graphNode.Value.Count > 0)
            {
                foreach(var neighbor in graphNode.Value)
                {
                    string[] nodeKey = graphNode.Key.Split(Path.DirectorySeparatorChar);
                    string[] nodeValue = neighbor.Split(Path.DirectorySeparatorChar);

                    int existingEdges = 0;
                    foreach (Microsoft.Msagl.Drawing.Edge edge in graphDraw.Edges)
                    {
                        if (edge.Source == nodeKey[nodeKey.Length - 1] && edge.Target == nodeValue[nodeValue.Length - 1])
                        {
                            existingEdges++;
                        }
                    }
                    if (existingEdges == 0)
                        graphDraw.AddEdge(nodeKey[nodeKey.Length - 1], nodeValue[nodeValue.Length - 1]);
                }
            }

        }
    
    }
}

public class CustomSearcher
{ 
    public static List<string> GetDirectories(string path, string searchPattern = "*",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (searchOption == SearchOption.TopDirectoryOnly)
        {    
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
        var directories = new List<string>(GetDirectories(path, searchPattern));

        for (var i = 0; i < directories.Count; i++)
            directories.AddRange(GetDirectories(directories[i], searchPattern));

        return directories;
    }


    /*private static List<string> GetDirectories(string path, string searchPattern)
    {
        try
        {
            return Directory.GetDirectories(path, searchPattern).ToList();
        }
        catch (UnauthorizedAccessException)
        {
            return new List<string>();
        }
    }*/
}