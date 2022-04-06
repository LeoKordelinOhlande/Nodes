

using Nodes;
using System;
using System.Threading;

static class Program
{
    public static void Main()
    {
        Console.WriteLine("Loading...");
        Scene scene;
        Scene? newScene;
        scene = new NodeScene();
        while(true)
        {
            newScene = scene.Run(0);
            scene.Dispose();
            if (newScene != null)
            {
                scene = newScene;   
            }
            else
            {

                break;
            }
        } 
    }
}
