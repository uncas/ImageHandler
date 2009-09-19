using System;
using System.Collections.Generic;
using System.Text;
using Uncas.Drawing.ImageResizing;
using System.IO;
using System.Threading;

namespace UncasImageHandler.Console
{
    class Program
    {
        /// <summary>
        /// Mains the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <example>
        ///     -i Photos -o Smaller -r -s 640
        ///     -i c:\Docs\Photos -o d:\Backup\Photos_small -r -s 640
        /// </example>
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.Run(args);
        }

        public Program()
        {
            _ri = new ResizeImages();
            _ri.ResizeProgressChanged
                += new EventHandler
                    <ResizeProgressEventArgs>
                    (_ri_ResizeProgressChanged);
            _ri.ResizeCompleted
                += new EventHandler
                    <ResizeCompletedEventArgs>
                    (_ri_ResizeCompleted);
            _ri.ResizeFailed
                += new EventHandler
                    <ResizeFailedEventArgs>
                    (_ri_ResizeFailed);
        }

        private void Run(string[] args)
        {
            string inputFolder
                = GetValueFromArgs
                (args, "-i", @"c:\temp\images");

            string outputFolder
                = GetValueFromArgs
                (args, "-o", @"c:\temp\small");

            int maxImageSizeDefault = 640;
            int maxImageSize = maxImageSizeDefault;
            string size
                = GetValueFromArgs
                (args, "-s", maxImageSize.ToString());
            if (!int.TryParse(size, out maxImageSize))
            {
                maxImageSize = maxImageSizeDefault;
            }

            bool includeSubfolders
                = ArgsContainCommand(args, "-r");

            string inputFolderPath
                = GetFolderPath(inputFolder);

            string outputFolderPath
                = GetFolderPath(outputFolder);

            System.Console.WriteLine
                 ("Input folder: {0}\nOutput folder: {1}\nMaxImageSize: {2}\nIncludeSubFolders: {3}"
                 , inputFolderPath
                 , outputFolderPath
                 , maxImageSize
                 , includeSubfolders);

            DoWork(inputFolderPath
                , includeSubfolders
                , outputFolderPath
                , maxImageSize);
        }

        private static string GetValueFromArgs
            (string[] args
            , string command
            , string defaultValue
            )
        {
            string value = defaultValue;
            for (int argIndex = 0
                ; argIndex < args.Length
                ; argIndex++)
            {
                string arg = args[argIndex];
                if (arg.Equals(command)
                    && argIndex + 1 < args.Length)
                {
                    value = args[argIndex + 1];
                }
            }
            return value;
        }

        private static bool ArgsContainCommand
           (string[] args
           , string command
           )
        {
            foreach (string arg in args)
            {
                if (arg.Equals(command))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetFolderPath(string folder)
        {
            string folderPath = string.Empty;
            if (folder.Contains(":")
                || folder.Contains("\\\\"))
            {
                folderPath = folder;
            }
            else
            {
                folderPath = Path.Combine
                    (Environment.CurrentDirectory
                    , folder);
            }
            return folderPath;
        }

        private ResizeImages _ri;

        private void DoWork
            (string baseInputFolder
            , bool includeSubfolders
            , string baseOutputFolder
            , int maxImageSize)
        {
            _ri.DoResizeWorkAsync
                (baseOutputFolder
                , maxImageSize
                , false
                , null
                , true
                , baseInputFolder
                , includeSubfolders);
            System.Console.ReadKey();
        }

        void _ri_ResizeProgressChanged
            (object sender
            , ResizeProgressEventArgs e)
        {
            System.Console.WriteLine
                ("Processed {0}% ({1} of {2} images)."
                , e.ProcessedImages.Percentage
                , e.ProcessedImages.ResizedNumberOfImages
                , e.ProcessedImages.TotalNumberOfImages);
        }

        void _ri_ResizeCompleted
            (object sender
            , ResizeCompletedEventArgs e)
        {
            Exit();
        }

        void _ri_ResizeFailed
            (object sender
            , ResizeFailedEventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            Environment.Exit(-1);
        }

    }
}
