using System;
using System.IO;
using Uncas.Drawing.ImageResizing;

namespace UncasImageHandler.Console
{
    class Program : IDisposable
    {
        /// <summary>
        /// Starts the main program.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <example>
        ///     -i Photos -o Smaller -r -s 640
        ///     -i c:\Docs\Photos -o d:\Backup\Photos_small -r -s 640
        /// </example>
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.Run(args);
        }

        #region Constructor

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

        #endregion

        #region Private fields

        private IResizeImages _ri;

        #endregion

        #region Private methods

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

        private void _ri_ResizeProgressChanged
            (object sender
            , ResizeProgressEventArgs e)
        {
            System.Console.WriteLine
                ("Processed {0}% ({1} of {2} images)."
                , e.ProcessedImages.Percentage
                , e.ProcessedImages.ResizedNumberOfImages
                , e.ProcessedImages.TotalNumberOfImages);
        }

        private void _ri_ResizeCompleted
            (object sender
            , ResizeCompletedEventArgs e)
        {
            Exit();
        }

        private void _ri_ResizeFailed
           (object sender
           , ResizeFailedEventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            Environment.Exit(-1);
        }

        #endregion

        #region Private static methods

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

        private static string GetFolderPath(string folder)
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

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var ri = _ri as ResizeImages;
                if (ri != null)
                {
                    ri.Dispose();
                }
            }
        }
    }
}